using Singulink.IO;

namespace NGame.Assets.UsageDetector.Commands;



public class ValidatedCommand(
	IAbsoluteDirectoryPath solutionDirectory,
	IAbsoluteFilePath appSettings,
	IAbsoluteFilePath output
)
{
	public IAbsoluteDirectoryPath SolutionDirectory { get; } = solutionDirectory;
	public IAbsoluteFilePath AppSettings { get; } = appSettings;
	public IAbsoluteFilePath Output { get; } = output;
}
