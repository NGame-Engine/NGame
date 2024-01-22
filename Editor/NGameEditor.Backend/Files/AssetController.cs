using Microsoft.Extensions.Logging;
using NGame.Assets;
using NGameEditor.Backend.Scenes;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;

namespace NGameEditor.Backend.Files;



public interface IAssetController
{
	Result Open(AbsolutePath fileName);
	Result<List<AssetDescription>> GetAssetsOfType(AssetTypeDefinition assetTypeDefinition);
}



internal class AssetController(
	ILogger<AssetController> logger,
	ISceneFileReader sceneFileReader,
	ISceneState sceneState,
	IAssetFileWatcher assetFileWatcher
) : IAssetController
{
	public Result Open(AbsolutePath fileName)
	{
		var path = fileName.Path;
		if (File.Exists(path) == false)
		{
			return Result.Error($"File '{path}' not found");
		}


		var extension = Path.GetExtension(path);
		if (extension == AssetConventions.SceneFileEnding)
		{
			return sceneFileReader
				.ReadSceneFile(fileName)
				.IfError(logger.Log)
				.Then(sceneState.SetLoadedScene);
		}

		logger.LogInformation("Open file {FileName}", fileName.Path);

		return Result.Success();
	}


	public Result<List<AssetDescription>> GetAssetsOfType(AssetTypeDefinition assetTypeDefinition) =>
		assetFileWatcher
			.GetAssetsOfType(assetTypeDefinition)
			.Then(x => x.ToList());
}
