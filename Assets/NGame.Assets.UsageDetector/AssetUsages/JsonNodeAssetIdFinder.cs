using System.Text.Json.Nodes;
using NGame.Assets.Common.Assets;

namespace NGame.Assets.UsageDetector.AssetUsages;



public interface IJsonNodeAssetIdFinder
{
	IEnumerable<Guid> FindReferencedIdsRecursively(JsonNode? jsonNode);
}



public class JsonNodeAssetIdFinder : IJsonNodeAssetIdFinder
{
	public IEnumerable<Guid> FindReferencedIdsRecursively(JsonNode? jsonNode)
	{
		if (jsonNode == null) yield break;

		var ids = jsonNode switch
		{
			JsonArray jsonArray => jsonArray.SelectMany(FindReferencedIdsRecursively),
			JsonObject jsonObject => GetChildren(jsonObject),
			_ => Enumerable.Empty<Guid>()
		};

		foreach (var id in ids)
		{
			yield return id;
		}
	}


	private IEnumerable<Guid> GetChildren(JsonObject jsonObject)
	{
		if (jsonObject.ContainsKey(AssetConventions.TypeDiscriminatorPropertyName))
		{
			var idNode = jsonObject[AssetConventions.AssetIdPropertyName]!;
			var id = idNode.GetValue<Guid>();
			yield return id;
		}

		var assetIdsInChildren =
			jsonObject
				.ToDictionary()
				.Values
				.SelectMany(FindReferencedIdsRecursively);

		foreach (var id in assetIdsInChildren)
		{
			yield return id;
		}
	}
}
