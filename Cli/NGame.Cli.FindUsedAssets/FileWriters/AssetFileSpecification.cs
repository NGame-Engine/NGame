using Singulink.IO;

namespace NGame.Cli.FindUsedAssets.FileWriters;



public record FileReference(IAbsoluteFilePath AbsolutePath, IRelativeFilePath RelativePath);



public record AssetFileSpecification(
	Guid Id,
	string PackageName,
	FileReference AssetFile,
	FileReference? CompanionFile
);
