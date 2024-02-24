using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;
using NGame.Assets.Common.Ecs;
using NGame.Assets.Packer.AssetOverviews;
using Singulink.IO;

namespace NGame.Assets.Packer.AssetUsages;



internal interface IAssetUsageFinder
{
	AssetUsageOverview Create(IAbsoluteFilePath appSettingsPath, AssetOverview assetOverview);
}



internal class AssetUsageFinder(
	IJsonNodeAssetIdFinder jsonNodeAssetIdFinder
) : IAssetUsageFinder
{
	public AssetUsageOverview Create(IAbsoluteFilePath appSettingsPath, AssetOverview assetOverview)
	{
		var sceneIds = GetSceneIds(appSettingsPath);

		var assetEntries =
			assetOverview
				.AssetEntries
				.ToDictionary(x => x.Id);

		var usedAssetIds = new HashSet<Guid>();
		foreach (var sceneId in sceneIds)
		{
			FindUsedAssetsRecursive(sceneId, usedAssetIds, assetEntries);
		}

		var usedAssetEntries = usedAssetIds.Select(x => assetEntries[x]);
		return new AssetUsageOverview(usedAssetEntries);
	}


	private static HashSet<Guid> GetSceneIds(IAbsoluteFilePath appSettingsPath)
	{
		var configurationManager = new ConfigurationManager();
		configurationManager.AddJsonFile(appSettingsPath.PathDisplay);

		var sceneAssetsConfiguration =
			configurationManager
				.GetSection(SceneAssetsConfiguration.JsonElementName)
				.Get<SceneAssetsConfiguration>()!;

		var sceneIds = sceneAssetsConfiguration.Scenes;
		return sceneIds;
	}


	private void FindUsedAssetsRecursive(
		Guid assetId,
		ISet<Guid> usedAssetIds,
		IReadOnlyDictionary<Guid, AssetEntry> assetEntries
	)
	{
		if (usedAssetIds.Add(assetId) == false) return;

		var assetEntry = assetEntries[assetId];
		var mainPathInfo = assetEntry.MainPathInfo;
		var fileContent = File.ReadAllText(mainPathInfo.AbsolutePath.PathDisplay);
		var jsonNode = JsonNode.Parse(fileContent)!;
		var referencedAssetIds = jsonNodeAssetIdFinder.FindReferencedIdsRecursively(jsonNode);

		foreach (var referencedAssetId in referencedAssetIds)
		{
			FindUsedAssetsRecursive(referencedAssetId, usedAssetIds, assetEntries);
		}
	}
}
