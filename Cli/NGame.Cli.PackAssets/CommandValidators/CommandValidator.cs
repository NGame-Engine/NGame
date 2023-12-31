using Microsoft.Extensions.Options;
using NGame.Cli.PackAssets.Paths;

namespace NGame.Cli.PackAssets.CommandValidators;



public interface ICommandValidator
{
	ValidatedCommand ValidateCommand();
}



public class CommandValidator : ICommandValidator
{
	private readonly CommandArguments _commandArguments;


	public CommandValidator(IOptions<CommandArguments> commandArguments)
	{
		_commandArguments = commandArguments.Value;
	}


	public ValidatedCommand ValidateCommand()
	{
		var assetListParameter =
			_commandArguments.AssetList ??
			throw new InvalidOperationException("No asset list provided");

		var assetListPath = AbsoluteNormalizedPath.Create(assetListParameter);
		if (!File.Exists(assetListPath.Value))
		{
			throw new InvalidOperationException($"Invalid asset list path '{assetListPath.Value}'");
		}


		var projectParameter =
			_commandArguments.Project ??
			throw new InvalidOperationException("No project folder provided");

		var projectFolder = AbsoluteNormalizedPath.Create(projectParameter);
		if (!Directory.Exists(projectFolder.Value))
		{
			throw new InvalidOperationException($"Invalid project folder '{projectFolder.Value}'");
		}


		var targetParameter =
			_commandArguments.Target ??
			throw new InvalidOperationException("No target folder provided");

		var targetFolder = AbsoluteNormalizedPath.Create(targetParameter);


		return new ValidatedCommand(assetListPath, projectFolder, targetFolder);
	}
}
