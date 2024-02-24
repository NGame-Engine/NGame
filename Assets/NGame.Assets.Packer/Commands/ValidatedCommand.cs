using Singulink.IO;

namespace NGame.Assets.Packer.Commands;



public class ValidatedCommand(
	IAbsoluteDirectoryPath unpackedAssetsDirectory,
	IAbsoluteFilePath appSettings,
	bool minifyJson,
	IAbsoluteDirectoryPath output
)
{
	public IAbsoluteDirectoryPath UnpackedAssetsDirectory { get; } = unpackedAssetsDirectory;
	public IAbsoluteFilePath AppSettings { get; } = appSettings;
	public bool MinifyJson { get; } = minifyJson;
	public IAbsoluteDirectoryPath Output { get; } = output;
}
