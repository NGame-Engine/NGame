using Singulink.IO;

namespace NGame.Assets.UsageDetector.AssetOverviews;



public record AssetEntry(
	Guid Id,
	IAbsoluteFilePath FilePath,
	string PackageName,
	IAbsoluteFilePath? CompanionFile
);
