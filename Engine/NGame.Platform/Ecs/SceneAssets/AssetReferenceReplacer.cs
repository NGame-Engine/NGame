using System.Collections;
using System.Reflection;
using NGame.Assets;
using NGame.Assets.Common.Ecs;
using NGame.Platform.Assets;

namespace NGame.Platform.Ecs.SceneAssets;



public record AssetReference(Asset Asset, int ReferenceLevel);



public interface IAssetReferenceReplacer
{
	List<AssetReference> ReplaceAssetReferences(object input);
}



public class AssetReferenceReplacer(
	IAssetAccessor assetAccessor
) : IAssetReferenceReplacer
{
	private static IEnumerable<PropertyInfo> GetNonIndexPropertyInfos(Type type) =>
		type
			.GetProperties()
			.Where(p => p.GetIndexParameters().Length == 0);


	public List<AssetReference> ReplaceAssetReferences(object input)
	{
		var type = input.GetType();
		var propertyInfos = GetNonIndexPropertyInfos(type);

		var referenceLevel = 1;
		var assetReferences = new List<AssetReference>();

		foreach (var propertyInfo in propertyInfos)
		{
			CheckProperty(input, propertyInfo, referenceLevel, assetReferences);
		}

		return assetReferences;
	}


	private void CheckProperty(
		object obj,
		PropertyInfo propertyInfo,
		int referenceLevel,
		List<AssetReference> assetReferences
	)
	{
		var value = propertyInfo.GetValue(obj);
		if (value == null) return;

		referenceLevel++;

		var type = propertyInfo.PropertyType;


		if (propertyInfo.CanWrite &&
		    type.IsAssignableTo(typeof(Asset)) &&
		    type.IsAssignableTo(typeof(SceneAsset)) == false)
		{
			var reference = (Asset)value;

			var hasTraversedAssetAlready =
				assetReferences.Any(x => x.Asset.Id == reference.Id);

			var asset = assetAccessor.ReadFromAssetPack(reference.Id);

			propertyInfo.SetValue(obj, asset);

			var assetReference = new AssetReference(asset, referenceLevel);
			assetReferences.Add(assetReference);

			if (hasTraversedAssetAlready) return;
			value = asset;
		}


		var propertyInfos = GetNonIndexPropertyInfos(type);

		foreach (var childPropertyInfo in propertyInfos)
		{
			CheckProperty(value, childPropertyInfo, referenceLevel, assetReferences);
		}


		if (value is not IEnumerable enumerable) return;

		foreach (var enumeratedObject in enumerable)
		{
			if (enumeratedObject == null) continue;

			var enumeratedType = enumeratedObject.GetType();
			var enumeratedPropertyInfos = GetNonIndexPropertyInfos(enumeratedType);

			foreach (var enumeratedPropertyInfo in enumeratedPropertyInfos)
			{
				CheckProperty(enumeratedObject, enumeratedPropertyInfo, referenceLevel,
					assetReferences);
			}
		}
	}
}
