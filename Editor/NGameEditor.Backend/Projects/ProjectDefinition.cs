using Singulink.IO;

namespace NGameEditor.Backend.Projects;



public record ProjectDefinition(
	IAbsoluteFilePath SolutionFilePath,
	IAbsoluteFilePath GameProjectFile,
	IAbsoluteFilePath EditorProjectFile,
	List<Type> AssetTypes,
	List<Type> ComponentTypes
);



public static class ProjectDefinitionExtensions
{
	public static IAbsoluteDirectoryPath GetEditorProjectFolder(
		this ProjectDefinition projectDefinition
	) => projectDefinition.EditorProjectFile.ParentDirectory!;


	public static IAbsoluteFilePath GetUserDataFilePath(
		this ProjectDefinition projectDefinition
	) =>
		projectDefinition
			.EditorProjectFile
			.ParentDirectory!
			.CombineFile(BackendConventions.UserPreferencesFileName);
}
