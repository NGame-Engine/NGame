using Singulink.IO;

namespace NGame.Assets.UsageFinder.Commands;



public interface IParameterValidator
{
	ValidatedCommand ValidateCommand(TaskParameters taskParameters);
}



public class ParameterValidator : IParameterValidator
{
	public ValidatedCommand ValidateCommand(TaskParameters taskParameters)
	{
		var assetListPaths =
			taskParameters
				.AssetLists
				.Select(x => FilePath.ParseAbsolute(x))
				.ToList();


		var appSettingsPaths = taskParameters.AppSettings;
		if (appSettingsPaths.Length == 0)
		{
			throw new InvalidOperationException("No appsettings provided");
		}

		if (appSettingsPaths.Length > 1)
		{
			var pathsCombined = string.Join(Environment.NewLine, appSettingsPaths.Length);
			throw new InvalidOperationException($"Too many appsettings files: {pathsCombined}");
		}

		var appSettingsArg = appSettingsPaths[0];
		var appSettingsPath = FilePath.ParseAbsolute(appSettingsArg);
		if (appSettingsPath.Exists == false)
		{
			throw new InvalidOperationException($"Invalid appsettings '{appSettingsPath}'");
		}

		return new ValidatedCommand(
			assetListPaths,
			appSettingsPath
		);
	}
}
