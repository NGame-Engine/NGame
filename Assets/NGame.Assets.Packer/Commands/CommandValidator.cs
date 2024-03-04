using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Singulink.IO;

namespace NGame.Assets.Packer.Commands;



public interface ICommandValidator
{
	ValidatedCommand ValidateCommand();
}



public class CommandValidator(
	ILogger<CommandValidator> logger,
	IOptions<CommandArguments> commandArguments
) : ICommandValidator
{
	private readonly CommandArguments _commandArguments = commandArguments.Value;


	public ValidatedCommand ValidateCommand()
	{
		var assetListsParameter =
			_commandArguments.AssetLists ??
			throw new InvalidOperationException("No assetlists provided");

		var assetListPaths = assetListsParameter.Split(';');


		var appSettingsParameter =
			_commandArguments.AppSettings ??
			throw new InvalidOperationException("No appsettings provided");

		var appSettings = FilePath.ParseAbsolute(appSettingsParameter);
		if (appSettings.Exists == false)
		{
			throw new InvalidOperationException($"Invalid appsettings '{appSettings}'");
		}

		var minifyJson = _commandArguments.MinifyJson ?? true;


		var outputParameter =
			_commandArguments.Output ??
			throw new InvalidOperationException("No output folder provided");

		// msbuild convention is to provide a backslash at the end of a path.
		// However, a backslash before a double quote means it will be included
		// in the parameter instead of ending it. In msbuild targets we leave a space,
		// here we trim it again.
		outputParameter = outputParameter.Trim();

		var outputFilePath = DirectoryPath.ParseAbsolute(outputParameter);

		logger.LogInformation(
			"Input validated, unpacked assets directory: {AssetLists}" +
			", appSettings file: {AppSettings}" +
			", minifyJson: {MinifyJson}" +
			", output file: {Output}",
			assetListsParameter,
			appSettings.PathDisplay,
			minifyJson,
			outputFilePath.PathDisplay
		);

		return new ValidatedCommand(
			assetListPaths,
			appSettings,
			minifyJson,
			outputFilePath
		);
	}
}
