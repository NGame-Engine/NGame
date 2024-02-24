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
		var unpackedAssetsParameter =
			_commandArguments.UnpackedAssets ??
			throw new InvalidOperationException("No unpackedassets provided");

		// When compiling on Windows, msbuild is to provide paths with a
		// backslash at the end.  This leads to the command line parameter
		// ending in \"
		// That means that that double quote is passed as part of the string parameter
		// instead of ending it. In order to prevent that, we put a space before
		// the closing double quote. That's why we need to trim it here.
		unpackedAssetsParameter = unpackedAssetsParameter.Trim();

		var unpackedAssetsDirectory = DirectoryPath.ParseAbsolute(unpackedAssetsParameter);
		if (unpackedAssetsDirectory.Exists == false)
		{
			throw new InvalidOperationException($"Invalid unpackedassets '{unpackedAssetsDirectory}'");
		}


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

		var outputFilePath = DirectoryPath.ParseAbsolute(outputParameter);

		logger.LogInformation(
			"Input validated, unpacked assets directory: {UnpackedAssetsDirectory}" +
			", appSettings file: {AppSettings}" +
			", minifyJson: {MinifyJson}" +
			", output file: {Output}",
			unpackedAssetsDirectory.PathDisplay,
			appSettings.PathDisplay,
			minifyJson,
			outputFilePath.PathDisplay
		);

		return new ValidatedCommand(
			unpackedAssetsDirectory,
			appSettings,
			minifyJson,
			outputFilePath
		);
	}
}
