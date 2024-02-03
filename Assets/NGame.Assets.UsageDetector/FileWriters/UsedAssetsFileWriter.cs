using Microsoft.Extensions.Logging;
using NGame.Assets.UsageDetector.AssetUsages;
using Singulink.IO;

namespace NGame.Assets.UsageDetector.FileWriters;



internal interface IUsedAssetsFileWriter
{
	void WriteToFile(
		AssetUsageOverview assetUsageOverview,
		IAbsoluteFilePath appSettingsPath,
		IAbsoluteDirectoryPath outputPath
	);
}



internal class UsedAssetsFileWriter(
	ILogger<UsedAssetsFileWriter> logger,
	IAssetFileSpecificationFactory assetFileSpecificationFactory,
	IAssetPackFactory assetPackFactory,
	ITableOfContentsGenerator tableOfContentsGenerator,
	ITableOfContentsWriter tableOfContentsWriter
) : IUsedAssetsFileWriter
{
	public void WriteToFile(
		AssetUsageOverview assetUsageOverview,
		IAbsoluteFilePath appSettingsPath,
		IAbsoluteDirectoryPath outputPath
	)
	{
		var projectFolder = appSettingsPath.ParentDirectory;

		var assetFileSpecifications = assetUsageOverview
			.UsedAssetEntries
			.Select(x => assetFileSpecificationFactory.Create(x, projectFolder))
			.ToList();


		var createdPackNames = assetFileSpecifications
			.GroupBy(x => x.PackageName)
			.Select(x =>
				assetPackFactory.Create(x.Key, x, outputPath)
			);

		var packNamesString = string.Join(", ", createdPackNames);
		logger.LogInformation("Created asset packs {AssetPacks}", packNamesString);


		var tableOfContents = tableOfContentsGenerator.CreateTableOfContents(assetFileSpecifications);
		tableOfContentsWriter.Write(tableOfContents, outputPath);
	}
}
