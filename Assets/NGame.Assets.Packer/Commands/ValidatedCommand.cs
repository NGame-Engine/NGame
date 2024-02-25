using Singulink.IO;

namespace NGame.Assets.Packer.Commands;



public class ValidatedCommand(
	string[] assetListPaths,
	IAbsoluteFilePath appSettings,
	bool minifyJson,
	IAbsoluteDirectoryPath output
)
{
	public string[] AssetListPaths { get; } = assetListPaths;
	public IAbsoluteFilePath AppSettings { get; } = appSettings;
	public bool MinifyJson { get; } = minifyJson;
	public IAbsoluteDirectoryPath Output { get; } = output;
}
