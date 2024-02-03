using System.Collections;
using System.Reflection;
using NGame.Assets;
using NGame.SceneAssets;

namespace NGame.Implementations.SceneAssets;



public interface IAssetReferenceReplacer
{
	void ReplaceAssetReferences(object input);
}



public class AssetReferenceReplacer : IAssetReferenceReplacer
{
	private readonly IAssetFromPackReader _assetFromPackReader;


	public AssetReferenceReplacer(IAssetFromPackReader assetFromPackReader)
	{
		_assetFromPackReader = assetFromPackReader;
	}


	private static IEnumerable<PropertyInfo> GetNonIndexPropertyInfos(Type type) =>
		type
			.GetProperties()
			.Where(p => p.GetIndexParameters().Length == 0);


	public void ReplaceAssetReferences(object input)
	{
		var type = input.GetType();
		var propertyInfos = GetNonIndexPropertyInfos(type);

		foreach (var propertyInfo in propertyInfos)
		{
			CheckProperty(input, propertyInfo);
		}
	}


	private void CheckProperty(object obj, PropertyInfo propertyInfo)
	{
		var value = propertyInfo.GetValue(obj);
		if (value == null) return;

		var type = propertyInfo.PropertyType;


		if (
			propertyInfo.CanWrite &&
			type.IsAssignableTo(typeof(Asset)) &&
			!type.IsAssignableTo(typeof(SceneAsset))
		)
		{
			value = GetRealAssetValue(type, value);
			propertyInfo.SetValue(obj, value);
		}

		var propertyInfos = GetNonIndexPropertyInfos(type);

		foreach (var childPropertyInfo in propertyInfos)
		{
			CheckProperty(value, childPropertyInfo);
		}

		if (value is not IEnumerable enumerable) return;
		foreach (var enumeratedObject in enumerable)
		{
			if (enumeratedObject == null) continue;

			var enumeratedType = enumeratedObject.GetType();
			var enumeratedPropertyInfos = GetNonIndexPropertyInfos(enumeratedType);

			foreach (var enumeratedPropertyInfo in enumeratedPropertyInfos)
			{
				CheckProperty(enumeratedObject, enumeratedPropertyInfo);
			}
		}
	}


	private Asset GetRealAssetValue(Type type, object value)
	{
		var propertyInfoSelector = GetNonIndexPropertyInfos(type);
		var idProperty =
			propertyInfoSelector
				.First(x => x.Name == AssetConventions.AssetIdPropertyName);

		var id = idProperty.GetValue(value);
		if (id == null)
		{
			throw new InvalidOperationException($"ID of {type} not set");
		}

		var assetId = (AssetId)id;
		return _assetFromPackReader.ReadFromAssetPack(assetId);
	}
}
