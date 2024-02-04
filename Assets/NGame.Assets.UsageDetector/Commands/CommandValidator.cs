using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Singulink.IO;

namespace NGame.Assets.UsageDetector.Commands;



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
		var solutionDirParameter =
			_commandArguments.SolutionDir ??
			throw new InvalidOperationException("No solutiondir provided");

		var solutionDirectory = DirectoryPath.ParseAbsolute(solutionDirParameter);
		if (solutionDirectory.Exists == false)
		{
			throw new InvalidOperationException($"Invalid solutiondir '{solutionDirectory}'");
		}


		var appSettingsParameter =
			_commandArguments.AppSettings ??
			throw new InvalidOperationException("No appsettings provided");

		var appSettings = FilePath.ParseAbsolute(appSettingsParameter);
		if (appSettings.Exists == false)
		{
			throw new InvalidOperationException($"Invalid appsettings '{appSettings}'");
		}


		var outputParameter =
			_commandArguments.Output ??
			throw new InvalidOperationException("No output folder provided");

		var outputFilePath = FilePath.ParseAbsolute(outputParameter);

		logger.LogInformation(
			"Input validated, solution directory: {Solution}" +
			", appSettings file: {AppSettings}" +
			", output file: {Output}",
			solutionDirectory.PathDisplay,
			appSettings.PathDisplay,
			outputFilePath.PathDisplay
		);

		return new ValidatedCommand(solutionDirectory, appSettings, outputFilePath);
	}
}
