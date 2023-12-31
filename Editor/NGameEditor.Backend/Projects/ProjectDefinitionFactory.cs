using System.Reflection;
using Microsoft.Extensions.Logging;
using NGame.Assets;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Setup;

namespace NGameEditor.Backend.Projects;



public interface IProjectDefinitionFactory
{
	ProjectDefinition Create();
}



public class ProjectDefinitionFactory(
	ILogger<ProjectDefinitionFactory> logger,
	IAssetTypeFinder assetTypeFinder,
	BackendApplicationArguments backendApplicationArguments,
	ISolutionConfigurationReader solutionConfigurationReader
)
	: IProjectDefinitionFactory
{
	public ProjectDefinition Create()
	{
		var solutionFilePath = backendApplicationArguments.SolutionFilePath;

		var configurationResult = solutionConfigurationReader.Read(solutionFilePath);
		if (configurationResult.HasError)
		{
			throw new NotImplementedException();
		}

		var solutionConfigurationJsonModel = configurationResult.SuccessValue!;

		var solutionDirectory = solutionFilePath.GetParentDirectory()!;
		var relativeGameProjectFile = solutionConfigurationJsonModel.GameProjectFile;
		var gameProjectFile = solutionDirectory.CombineWith(relativeGameProjectFile);
		var relativeEditorProjectFile = solutionConfigurationJsonModel.EditorProjectFile;
		var editorProjectFile = solutionDirectory.CombineWith(relativeEditorProjectFile);


		var assembly = Assembly.GetEntryAssembly()!;
		logger.LogDebug("Creating project definition from {Assembly}", assembly.FullName);


		var assetTypes = assetTypeFinder
			.FindAssetTypes(assembly)
			.ToList();
		logger.LogDebug("Found {Count} assets", assetTypes.Count);


		var componentTypes = assetTypeFinder
			.FindComponentTypes(assembly)
			.ToList();
		logger.LogDebug("Found {Count} components", componentTypes.Count);


		return new ProjectDefinition(
			solutionFilePath,
			gameProjectFile,
			editorProjectFile,
			assetTypes,
			componentTypes
		);
	}
}
