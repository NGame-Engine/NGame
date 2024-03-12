using Singulink.IO;

namespace NGame.Assets.UsageFinder.Commands;



public class ValidatedCommand(
	List<IAbsoluteFilePath> assetListPaths,
	IAbsoluteFilePath appSettingsPath
)
{
	public List<IAbsoluteFilePath> AssetListPaths { get; } = assetListPaths;
	public IAbsoluteFilePath AppSettingsPath { get; } = appSettingsPath;
}
