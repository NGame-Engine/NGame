using System.Text.Json;
using NGameEditor.Results;
using Singulink.IO;

namespace NGameEditor.Bridge.Setup;



public interface ISolutionConfigurationReader
{
	Result<SolutionConfigurationJsonModel> Read(IAbsoluteFilePath solutionFilePath);
}



public class SolutionConfigurationReader : ISolutionConfigurationReader
{
	public Result<SolutionConfigurationJsonModel> Read(IAbsoluteFilePath solutionFilePath)
	{
		var solutionConfigurationPath = $"{solutionFilePath.PathDisplay}{BridgeConventions.SolutionConfigurationSuffix}";
		if (File.Exists(solutionConfigurationPath) == false)
		{
			return Result.Error(
				$"Solution configuration file at '{solutionConfigurationPath}' does not exist"
			);
		}

		var allText = File.ReadAllText(solutionConfigurationPath);
		var jsonModel = JsonSerializer.Deserialize<SolutionConfigurationJsonModel>(allText);
		if (jsonModel != null) return Result.Success(jsonModel);


		var message = $"Unable to deserialize settings at '{solutionConfigurationPath}'";
		return Result.Error(message);
	}
}
