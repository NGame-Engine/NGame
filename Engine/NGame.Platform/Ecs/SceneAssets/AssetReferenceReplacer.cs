using System.Collections;
using System.Reflection;
using NGame.Assets;
using NGame.Assets.Common.Ecs;
using NGame.Platform.Assets;

namespace NGame.Platform.Ecs.SceneAssets;



public interface IAssetReferenceReplacer
{
	void ReplaceAssetReferences(object input);
}



public class AssetReferenceReplacer(
	IAssetAccessor assetAccessor
) : IAssetReferenceReplacer
{
	private static IEnumerable<PropertyInfo> GetNonIndexPropertyInfos(Type type) =>
		type
			.GetProperties()
			.Where(p => p.GetIndexParameters().Length == 0);


	public void ReplaceAssetReferences(object input)
	{
		var type = input.GetType();
		var propertyInfos = GetNonIndexPropertyInfos(type);

		var alreadyImported = new Dictionary<Guid, Asset>();

		foreach (var propertyInfo in propertyInfos)
		{
			CheckProperty(input, propertyInfo, alreadyImported);
		}
	}


	private void CheckProperty(
		object obj,
		PropertyInfo propertyInfo,
		Dictionary<Guid, Asset> alreadyImported
	)
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
			var assetReference = (Asset)value;
			var assetId = assetReference.Id;
			if (alreadyImported.TryGetValue(assetId, out var asset))
			{
				propertyInfo.SetValue(obj, asset);
				return;
			}

			var newAsset = assetAccessor.ReadFromAssetPack(assetId);
			alreadyImported.Add(assetId, newAsset);
			propertyInfo.SetValue(obj, newAsset);
		}

		var propertyInfos = GetNonIndexPropertyInfos(type);

		foreach (var childPropertyInfo in propertyInfos)
		{
			CheckProperty(value, childPropertyInfo, alreadyImported);
		}

		if (value is not IEnumerable enumerable) return;
		foreach (var enumeratedObject in enumerable)
		{
			if (enumeratedObject == null) continue;

			var enumeratedType = enumeratedObject.GetType();
			var enumeratedPropertyInfos = GetNonIndexPropertyInfos(enumeratedType);

			foreach (var enumeratedPropertyInfo in enumeratedPropertyInfos)
			{
				CheckProperty(enumeratedObject, enumeratedPropertyInfo, alreadyImported);
			}
		}
	}
}
