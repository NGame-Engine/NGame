using NGameEditor.Bridge.Shared;
using NGameEditor.Results;

namespace NGameEditor.Backend.Scenes;



public interface ISceneFileWatcher
{
	Result<AbsolutePath> GetPathToSceneFile(Guid id);
}



internal class SceneFileWatcher : ISceneFileWatcher, IDisposable
{
	private readonly FileSystemWatcher _fileSystemWatcher;
	private readonly ISceneFileIdReader _sceneFileIdReader;


	public SceneFileWatcher(FileSystemWatcher fileSystemWatcher, ISceneFileIdReader sceneFileIdReader, HashSet<string> allFiles)
	{
		_fileSystemWatcher = fileSystemWatcher;
		_sceneFileIdReader = sceneFileIdReader;
		AllFiles = allFiles;
	}


	private HashSet<string> AllFiles { get; }
	private bool HasChanges { get; set; } = true;
	private Dictionary<Guid, AbsolutePath> SceneFilesById { get; } = new();


	public Result<AbsolutePath> GetPathToSceneFile(Guid id)
	{
		if (HasChanges)
		{
			var updateResult = UpdateSceneFilesById();
			if (updateResult.HasError) return Result.Error(updateResult.ErrorValue!);
		}

		return
			SceneFilesById.TryGetValue(id, out var sceneFilePath)
				? Result.Success(sceneFilePath)
				: Result.Error($"Scene with id '{id}' not found");
	}


	private Result UpdateSceneFilesById()
	{
		HasChanges = false;
		SceneFilesById.Clear();
		foreach (var filePath in AllFiles)
		{
			var absolutePath = new AbsolutePath(filePath);
			var result = _sceneFileIdReader.GetId(absolutePath);
			if (result.HasError) return result;

			var id = result.SuccessValue;
			SceneFilesById.Add(id, absolutePath);
		}

		return Result.Success();
	}


	public void OnChanged(object sender, FileSystemEventArgs e)
	{
		HasChanges = true;
	}


	public void OnCreated(object sender, FileSystemEventArgs e)
	{
		AllFiles.Add(e.FullPath);
		HasChanges = true;
	}


	public void OnDeleted(object sender, FileSystemEventArgs e)
	{
		AllFiles.Remove(e.FullPath);
		HasChanges = true;
	}


	public void OnRenamed(object sender, RenamedEventArgs e)
	{
		AllFiles.Remove(e.OldFullPath);
		AllFiles.Add(e.FullPath);
		HasChanges = true;
	}


	public void OnError(object sender, ErrorEventArgs e)
	{
		throw e.GetException();
	}


	void IDisposable.Dispose()
	{
		_fileSystemWatcher.Dispose();
	}
}
