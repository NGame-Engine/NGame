using NGame.Assets.UsageDetector.AssetOverviews;
using NGame.Assets.UsageDetector.AssetUsages;
using Singulink.IO;

namespace NGame.Assets.UsageDetector.FileWriters;



internal interface IUsedAssetsFileWriter
{
	void WriteToFile(
		AssetUsageOverview assetUsageOverview,
		IAbsoluteFilePath outputPath
	);
}



internal class UsedAssetsFileWriter : IUsedAssetsFileWriter
{
	public void WriteToFile(
		AssetUsageOverview assetUsageOverview,
		IAbsoluteFilePath outputPath
	) =>
		File.WriteAllLines(
			outputPath.PathDisplay,
			assetUsageOverview
				.UsedAssetEntries
				.Select(CreateTextLine)
		);


	private static string CreateTextLine(AssetEntry x)
	{
		var packageName = string.IsNullOrWhiteSpace(x.PackageName)
			? "Default"
			: x.PackageName;

		var filePath = x.FilePath.PathDisplay;
		var companionFile = x.CompanionFile?.PathDisplay;

		return $"{x.Id}//{packageName}//{filePath}//{companionFile}";
	}
}
