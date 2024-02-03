using NGame.Cli.PackAssetsCommand.AssetFileReaders;

namespace NGame.Cli.PackAssetsCommand;



public static class Conventions
{
	public const string AssetPackFileEnding = "ngpack";
	public const string DefaultAssetPackName = "Default";


	public static string CreateAssetPackName(PackageName packageName) =>
		$"{packageName.Value}.{AssetPackFileEnding}";
}
