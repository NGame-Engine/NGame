using NGame.Cli.PackAssets.AssetFileReaders;

namespace NGame.Cli.PackAssets;



public static class Conventions
{
	public static readonly string AssetPackFileEnding = "ngpack";
	public static readonly string DefaultAssetPackName = "Default";


	public static string CreateAssetPackName(PackageName packageName) =>
		$"{packageName.Value}.{AssetPackFileEnding}";
}
