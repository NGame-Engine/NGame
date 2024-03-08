using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;
using NGame.Assets.Common.Ecs;
using NGame.Assets.UsageFinder.AssetOverviews;
using Singulink.IO;

namespace NGame.Assets.UsageFinder.AssetUsages;



internal interface IAssetUsageOverviewFactory
{
	AssetUsageOverview Create(IEnumerable<IAbsoluteFilePath> appSettingsFiles, AssetOverview assetOverview);
}



internal class AssetUsageOverviewFactory(
	IJsonNodeAssetIdFinder jsonNodeAssetIdFinder
) : IAssetUsageOverviewFactory
{
	public AssetUsageOverview Create(IEnumerable<IAbsoluteFilePath> appSettingsFiles, AssetOverview assetOverview)
	{
		var sceneIds = GetSceneIds(appSettingsFiles);

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


	private static HashSet<Guid> GetSceneIds(IEnumerable<IAbsoluteFilePath> appSettingsFiles)
	{
		var configurationManager = new ConfigurationManager();

		var orderedAppSettings =
			appSettingsFiles.OrderBy(x => x.Length);

		foreach (var appSettingsFile in orderedAppSettings)
		{
			configurationManager.AddJsonFile(appSettingsFile.PathDisplay);
		}


		var sceneAssetsConfiguration =
			configurationManager
				.GetSection(SceneAssetsConfiguration.JsonElementName)
				.Get<SceneAssetsConfiguration>()!;

		return sceneAssetsConfiguration.Scenes;
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
		var fileContent = File.ReadAllText(mainPathInfo.SourcePath.PathDisplay);
		var jsonNode = JsonNode.Parse(fileContent)!;
		var referencedAssetIds = jsonNodeAssetIdFinder.FindReferencedIdsRecursively(jsonNode);

		foreach (var referencedAssetId in referencedAssetIds)
		{
			FindUsedAssetsRecursive(referencedAssetId, usedAssetIds, assetEntries);
		}
	}
}
