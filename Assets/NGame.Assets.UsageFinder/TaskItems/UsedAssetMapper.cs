using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NGame.Assets.UsageFinder.AssetOverviews;
using Singulink.IO;

namespace NGame.Assets.UsageFinder.TaskItems;



public static class UsedAssetMapper
{
	public static ITaskItem Map(AssetEntry assetEntry)
	{
		var taskItem = new TaskItem(assetEntry.MainPathInfo.SourcePath.PathDisplay);
		taskItem.SetMetadata("AssetId", assetEntry.Id.ToString());
		taskItem.SetMetadata("ProjectDirectory", $"{assetEntry.ProjectDirectory.PathDisplay}/");
		taskItem.SetMetadata("Package", assetEntry.PackageName);
		taskItem.SetMetadata("JsonFilePath", assetEntry.MainPathInfo.TargetPath.PathDisplay);
		taskItem.SetMetadata("DataFilePath", assetEntry.SatellitePathInfo?.TargetPath.PathDisplay);
		return taskItem;
	}


	public static ITaskItem Map(UsedAsset usedAsset)
	{
		var taskItem = new TaskItem(usedAsset.SourcePath.PathDisplay);
		taskItem.SetMetadata("AssetId", usedAsset.AssetId.ToString());
		taskItem.SetMetadata("ProjectDirectory", $"{usedAsset.ProjectDirectory.PathDisplay}/");
		taskItem.SetMetadata("Package", usedAsset.Package);
		taskItem.SetMetadata("JsonFilePath", usedAsset.JsonFilePath.PathDisplay);

		if (usedAsset.DataFilePath == null) return taskItem;

		taskItem.SetMetadata("DataFilePath", usedAsset.DataFilePath.PathDisplay);
		return taskItem;
	}


	public static UsedAsset Map(ITaskItem taskItem) =>
		new(
			FilePath.ParseAbsolute(taskItem.ItemSpec),
			Guid.Parse(taskItem.GetMetadata("AssetId")),
			taskItem.GetMetadata("Package"),
			DirectoryPath.ParseAbsolute(taskItem.GetMetadata("ProjectDirectory")),
			FilePath.ParseRelative(taskItem.GetMetadata("JsonFilePath")),
			GetDataFilePath(taskItem)
		);


	private static IRelativeFilePath? GetDataFilePath(ITaskItem taskItem)
	{
		var dataFilePath = taskItem.GetMetadata("DataFilePath");

		return
			string.IsNullOrWhiteSpace(dataFilePath)
				? null
				: FilePath.ParseRelative(dataFilePath);
	}
}
