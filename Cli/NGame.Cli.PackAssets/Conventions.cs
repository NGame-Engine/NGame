using NGame.Cli.PackAssets.AssetFileReaders;

namespace NGame.Cli.PackAssets;



public static class Conventions
{
	public const string AssetPackFileEnding = "ngpack";
	public const string DefaultAssetPackName = "Default";


	public static string CreateAssetPackName(PackageName packageName) =>
		$"{packageName.Value}.{AssetPackFileEnding}";
}
