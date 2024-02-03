using Singulink.IO;

namespace NGame.Assets.UsageDetector.Commands;



public class ValidatedCommand(
	IAbsoluteDirectoryPath solutionDirectory,
	IAbsoluteFilePath appSettings,
	IAbsoluteDirectoryPath outputDirectory
)
{
	public IAbsoluteDirectoryPath SolutionDirectory { get; } = solutionDirectory;
	public IAbsoluteFilePath AppSettings { get; } = appSettings;
	public IAbsoluteDirectoryPath OutputDirectory { get; } = outputDirectory;
}
