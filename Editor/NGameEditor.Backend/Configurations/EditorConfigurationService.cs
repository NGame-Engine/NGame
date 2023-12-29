using NGameEditor.Backend.Projects;

namespace NGameEditor.Backend.Configurations;



internal interface IEditorConfigurationService
{
	void UpdateSection(
		string sectionName,
		object newContent,
		string? environmentName = null
	);
}



class EditorConfigurationService : IEditorConfigurationService
{
	private readonly ProjectDefinition _projectDefinition;
	private readonly IJsonSectionUpdater _jsonSectionUpdater;


	public EditorConfigurationService(ProjectDefinition projectDefinition, IJsonSectionUpdater jsonSectionUpdater)
	{
		_projectDefinition = projectDefinition;
		_jsonSectionUpdater = jsonSectionUpdater;
	}


	public void UpdateSection(
		string sectionName,
		object newContent,
		string? environmentName
	)
	{
		var fileName =
			environmentName == null
				? "appsettings.json"
				: $"appsettings.{environmentName}.json";

		var filePath =
			_projectDefinition
				.EditorProjectFile
				.GetParentDirectory()!
				.CombineWith(fileName);

		_jsonSectionUpdater.UpdateSection(filePath, sectionName, newContent);
	}
}
