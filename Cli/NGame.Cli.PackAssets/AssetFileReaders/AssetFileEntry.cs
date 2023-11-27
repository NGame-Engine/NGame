using NGame.Cli.PackAssets.Paths;

namespace NGame.Cli.PackAssets.AssetFileReaders;



public record AssetFileEntry(PackageName PackageName, NormalizedPath FilePath);

public record PackageName(string Value);
