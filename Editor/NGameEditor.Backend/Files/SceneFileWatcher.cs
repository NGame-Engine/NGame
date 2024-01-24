using NGame.Assets;
using NGameEditor.Backend.Scenes;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;

namespace NGameEditor.Backend.Files;



public interface ISceneFileWatcher
{
	Result<AbsolutePath> GetPathToSceneFile(Guid id);
}



internal record SceneFileEntry(Guid Id, AbsolutePath FilePath);



internal class SceneFileWatcher(
	ISceneFileIdReader sceneFileIdReader,
	HashSet<AbsolutePath> allFiles
)
	: ISceneFileWatcher
{
	private HashSet<AbsolutePath> AllFiles { get; } = allFiles;
	private bool HasChanges { get; set; } = true;
	private List<SceneFileEntry> SceneFilesById { get; } = new();


	private static bool IsSceneFilePath(AbsolutePath absolutePath) =>
		absolutePath.Path.EndsWith(AssetConventions.SceneFileEnding);


	public Result<AbsolutePath> GetPathToSceneFile(Guid id)
	{
		if (HasChanges)
		{
			var updateResult = UpdateSceneFilesById();
			if (updateResult.HasError) return Result.Error(updateResult.ErrorValue!);
		}

		var sceneFileEntry = SceneFilesById.FirstOrDefault(x => x.Id == id);

		return sceneFileEntry == null
			? Result.Error($"Scene with id '{id}' not found")
			: Result.Success(sceneFileEntry.FilePath);
	}


	private Result UpdateSceneFilesById()
	{
		HasChanges = false;
		SceneFilesById.Clear();
		foreach (var filePath in AllFiles)
		{
			var result = sceneFileIdReader.GetId(filePath);
			if (result.HasError) return result;

			var id = result.SuccessValue;
			var sceneFileEntry = new SceneFileEntry(id, filePath);
			SceneFilesById.Add(sceneFileEntry);
		}

		return Result.Success();
	}


	public void OnChanged(FileChangedArgs args)
	{
		if (IsSceneFilePath(args.Path) == false) return;
		HasChanges = true;
	}


	public void OnCreated(FileCreatedArgs args)
	{
		if (IsSceneFilePath(args.Path) == false) return;

		AllFiles.Add(args.Path);
		HasChanges = true;
	}


	public void OnDeleted(FileDeletedArgs args)
	{
		if (IsSceneFilePath(args.Path) == false) return;

		AllFiles.Remove(args.Path);
		HasChanges = true;
	}


	public void OnRenamed(FileRenamedArgs args)
	{
		var oldPathIsSceneFilePath = IsSceneFilePath(args.OldPath);
		var newPathIsSceneFilePath = IsSceneFilePath(args.Path);


		if (oldPathIsSceneFilePath)
		{
			AllFiles.Remove(args.OldPath);
		}

		if (newPathIsSceneFilePath)
		{
			AllFiles.Add(args.Path);
		}

		if (oldPathIsSceneFilePath == false &&
		    newPathIsSceneFilePath == false) return;

		HasChanges = true;
	}
}
