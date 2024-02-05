using Microsoft.Extensions.Logging;
using NGame.Assets;
using NGame.Assets.Common.Ecs;
using NGameEditor.Backend.Scenes;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;

namespace NGameEditor.Backend.Files;



public interface IAssetController
{
	Result Open(AbsolutePath fileName);
	List<AssetDescription> GetAssetsOfType(AssetTypeDefinition assetTypeDefinition);
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

		var assetDescription = assetFileWatcher
			.GetAssetDescriptions()
			.FirstOrDefault(x => x.FilePath == fileName);

		if (assetDescription == null) return Result.Success();


		var typeId = assetDescription.AssetTypeDefinition.Identifier;
		if (typeId == AssetAttribute.GetDiscriminator(typeof(SceneAsset)))
		{
			return sceneFileReader
				.ReadSceneFile(fileName.ToAbsoluteFilePath())
				.IfError(logger.Log)
				.Then(sceneState.SetLoadedScene);
		}


		logger.LogInformation("Open file {FileName}", fileName.Path);

		return Result.Success();
	}


	public List<AssetDescription> GetAssetsOfType(AssetTypeDefinition assetTypeDefinition) =>
		assetFileWatcher
			.GetAssetDescriptions()
			.Where(x => x.AssetTypeDefinition.Identifier == assetTypeDefinition.Identifier)
			.ToList();
}
