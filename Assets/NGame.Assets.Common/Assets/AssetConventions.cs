namespace NGame.Assets.Common.Assets;



public static class AssetConventions
{
	public const string TypeDiscriminatorPropertyName = "$asset";
	public const string AssetIdPropertyName = "Id";
	public const string AssetFileEnding = ".ngasset";

	[Obsolete("Clean up when unused")] public const string ListFileName = "NGAssets.g.txt"; // TODO clean up when unused
	[Obsolete("Clean up when unused")] public const string PackSeparator = "//"; // TODO clean up when unused

	public const string AssetPackSubFolder = ".assets";
	public const string TableOfContentsFileName = "toc.json";
	public const string PackFileEnding = ".ngpack";
}
