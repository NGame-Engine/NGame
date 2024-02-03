using Singulink.IO;

namespace NGame.Cli.FindUsedAssetsCommand.AssetOverviews;



public record AssetEntry(
	Guid Id,
	IAbsoluteFilePath FilePath,
	string PackageName,
	IAbsoluteFilePath? CompanionFile
);
