using NGame.Cli.Abstractions.Paths;

namespace NGame.Cli.PackAssets.CommandValidators;



public class ValidatedCommand
{
	public ValidatedCommand(
		AbsoluteNormalizedPath assetList,
		AbsoluteNormalizedPath projectFolder,
		AbsoluteNormalizedPath targetFolder
	)
	{
		AssetList = assetList;
		TargetFolder = targetFolder;
		ProjectFolder = projectFolder;
	}


	public AbsoluteNormalizedPath AssetList { get; }
	public AbsoluteNormalizedPath ProjectFolder { get; }
	public AbsoluteNormalizedPath TargetFolder { get; }
}
