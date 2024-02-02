using Microsoft.Extensions.Options;
using NGame.Cli.Abstractions.Paths;

namespace NGame.Cli.PackAssets.CommandValidators;



public interface ICommandValidator
{
	ValidatedCommand ValidateCommand();
}



public class CommandValidator(
	IOptions<CommandArguments> commandArguments
) : ICommandValidator
{
	private readonly CommandArguments _commandArguments = commandArguments.Value;

	[Obsolete]
	public ValidatedCommand ValidateCommand()
	{
		var assetListParameter =
			_commandArguments.AssetList ??
			throw new InvalidOperationException("No asset list provided");

		var assetListPath = AbsoluteNormalizedPath.Create(assetListParameter);
		if (!File.Exists(assetListPath.Value))
		{
			throw new InvalidOperationException($"Invalid asset list path '{assetListPath}'");
		}


		var projectParameter =
			_commandArguments.Project ??
			throw new InvalidOperationException("No project folder provided");

		var projectFolder = AbsoluteNormalizedPath.Create(projectParameter);
		if (!Directory.Exists(projectFolder.Value))
		{
			throw new InvalidOperationException($"Invalid project folder '{projectFolder}'");
		}


		var targetParameter =
			_commandArguments.Target ??
			throw new InvalidOperationException("No target folder provided");

		var targetFolder = AbsoluteNormalizedPath.Create(targetParameter);


		return new ValidatedCommand(assetListPath, projectFolder, targetFolder);
	}
}
