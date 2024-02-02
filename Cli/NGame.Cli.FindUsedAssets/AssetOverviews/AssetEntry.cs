using Singulink.IO;

namespace NGame.Cli.FindUsedAssets.AssetOverviews;



public record AssetEntry(
	Guid Id,
	IAbsoluteFilePath FilePath,
	string PackageName,
	IAbsoluteFilePath? CompanionFile
);
