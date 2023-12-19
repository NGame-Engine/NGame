using Microsoft.Extensions.Configuration;
using NGameEditor.Backend.Projects;
using NGameEditor.Results;

namespace NGameEditor.Backend.Configurations;



public interface IGameConfigurationService
{
	Result<TConfig> GetSection<TConfig>(string sectionName, string? environmentName = null);
	void UpdateSection(string sectionName, object newContent, string? environmentName);
}



class GameConfigurationService : IGameConfigurationService
{
	private readonly IJsonSectionUpdater _jsonSectionUpdater;
	private readonly ProjectDefinition _projectDefinition;


	public GameConfigurationService(
		IJsonSectionUpdater jsonSectionUpdater,
		ProjectDefinition projectDefinition
	)
	{
		_jsonSectionUpdater = jsonSectionUpdater;
		_projectDefinition = projectDefinition;
	}


	public Result<TConfig> GetSection<TConfig>(string sectionName, string? environmentName)
	{
		var configurationManager = new ConfigurationManager();

		var defaultFile =
			_projectDefinition
				.GameProjectFile
				.GetParentDirectory()!
				.CombineWith("appsettings.json");

		configurationManager.AddJsonFile(defaultFile.Path);

		if (environmentName != null)
		{
			var environmentFile =
				_projectDefinition
					.GameProjectFile
					.GetParentDirectory()!
					.CombineWith($"appsettings.{environmentName}.json");

			configurationManager.AddJsonFile(environmentFile.Path);
		}

		var config = configurationManager
			.GetSection(sectionName)
			.Get<TConfig>();

		return
			config == null
				? Result.Error($"Unable to read configuration from section {sectionName}")
				: Result.Success<TConfig>(config);
	}


	public void UpdateSection(string sectionName, object newContent, string? environmentName)
	{
		var fileName =
			environmentName == null
				? "appsettings.json"
				: $"appsettings.{environmentName}.json";

		var filePath =
			_projectDefinition
				.GameProjectFile
				.GetParentDirectory()!
				.CombineWith(fileName);

		_jsonSectionUpdater.UpdateSection(filePath, sectionName, newContent);
	}
}
