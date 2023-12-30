using System.Reflection;
using Microsoft.Extensions.Logging;
using NGame.Assets;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Setup;

namespace NGameEditor.Backend.Projects.Setup;



public interface IProjectDefinitionFactory
{
	ProjectDefinition Create();
}



public class ProjectDefinitionFactory : IProjectDefinitionFactory
{
	private readonly ILogger<ProjectDefinitionFactory> _logger;
	private readonly IAssetTypeFinder _assetTypeFinder;
	private readonly BackendApplicationArguments _backendApplicationArguments;
	private readonly ISolutionConfigurationReader _solutionConfigurationReader;


	public ProjectDefinitionFactory(
		ILogger<ProjectDefinitionFactory> logger,
		IAssetTypeFinder assetTypeFinder,
		BackendApplicationArguments backendApplicationArguments,
		ISolutionConfigurationReader solutionConfigurationReader
	)
	{
		_logger = logger;
		_assetTypeFinder = assetTypeFinder;
		_backendApplicationArguments = backendApplicationArguments;
		_solutionConfigurationReader = solutionConfigurationReader;
	}


	public ProjectDefinition Create()
	{
		var solutionFilePath = _backendApplicationArguments.SolutionFilePath;

		var configurationResult = _solutionConfigurationReader.Read(solutionFilePath);
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
		_logger.LogDebug("Creating project definition from {Assembly}", assembly.FullName);

		var assetTypes = _assetTypeFinder
			.FindAssetTypes(assembly)
			.ToList();
		_logger.LogDebug("Found {Count} assets", assetTypes.Count);

		var componentTypes = _assetTypeFinder
			.FindComponentTypes(assembly)
			.ToList();
		_logger.LogDebug("Found {Count} components", componentTypes.Count);


		return new ProjectDefinition(
			solutionFilePath,
			gameProjectFile,
			editorProjectFile,
			assetTypes,
			componentTypes
		);
	}
}
