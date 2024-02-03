using NGame.Cli.PackAssetsCommand.Paths;

namespace NGame.Cli.PackAssetsCommand.CommandValidators;



public class ValidatedCommand
{
	[Obsolete]
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

	[Obsolete]
	public AbsoluteNormalizedPath AssetList { get; }
	[Obsolete]
	public AbsoluteNormalizedPath ProjectFolder { get; }
	[Obsolete]
	public AbsoluteNormalizedPath TargetFolder { get; }
}
