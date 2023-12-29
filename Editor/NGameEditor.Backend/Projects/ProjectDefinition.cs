using NGameEditor.Bridge.Shared;

namespace NGameEditor.Backend.Projects;



public record ProjectDefinition(
	AbsolutePath SolutionFilePath,
	AbsolutePath GameProjectFile,
	AbsolutePath EditorProjectFile,
	List<Type> AssetTypes,
	List<Type> ComponentTypes
);



public static class ProjectDefinitionExtensions
{
	public static AbsolutePath GetEditorProjectFolder(
		this ProjectDefinition projectDefinition
	) => projectDefinition.EditorProjectFile.GetParentDirectory()!;


	public static AbsolutePath GetUserDataFilePath(
		this ProjectDefinition projectDefinition
	) =>
		projectDefinition
			.EditorProjectFile
			.GetParentDirectory()!
			.CombineWith(BackendConventions.UserPreferencesFileName);
}
