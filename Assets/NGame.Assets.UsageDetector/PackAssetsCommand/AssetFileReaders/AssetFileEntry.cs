using NGame.Cli.PackAssetsCommand.Paths;

namespace NGame.Cli.PackAssetsCommand.AssetFileReaders;



public record AssetFileEntry(PackageName PackageName, NormalizedPath FilePath);

public record PackageName(string Value);
