using Microsoft.Extensions.Logging;
using NGame.Assets;
using NGameEditor.Backend.Scenes;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;

namespace NGameEditor.Backend.Files;



public interface IFileOpener
{
	Result Open(AbsolutePath fileName);
}



public class FileOpener(
	ILogger<FileOpener> logger,
	ISceneFileReader sceneFileReader,
	ISceneState sceneState
) : IFileOpener
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
}
