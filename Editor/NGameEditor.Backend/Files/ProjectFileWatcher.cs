using NGameEditor.Bridge.Shared;

namespace NGameEditor.Backend.Files;



public record FileChangedArgs(AbsolutePath Path, string? Name);

public record FileCreatedArgs(AbsolutePath Path, string? Name);

public record FileDeletedArgs(AbsolutePath Path, string? Name);

public record FileRenamedArgs(AbsolutePath Path, string? Name, AbsolutePath OldPath, string? OldName);



public interface IProjectFileWatcher
{
	event Action<FileChangedArgs> FileChanged;
	event Action<FileCreatedArgs> FileCreated;
	event Action<FileDeletedArgs> FileDeleted;
	event Action<FileRenamedArgs> FileRenamed;

	IEnumerable<AbsolutePath> GetAllFiles();
}



public class ProjectFileWatcher(
	HashSet<AbsolutePath> currentFiles,
	FileSystemWatcher fileSystemWatcher
) : IProjectFileWatcher, IDisposable
{
	public event Action<FileChangedArgs>? FileChanged;
	public event Action<FileCreatedArgs>? FileCreated;
	public event Action<FileDeletedArgs>? FileDeleted;
	public event Action<FileRenamedArgs>? FileRenamed;


	private HashSet<AbsolutePath> AllFiles { get; } = currentFiles;

	public IEnumerable<AbsolutePath> GetAllFiles() => AllFiles;


	public void OnChanged(object sender, FileSystemEventArgs e)
	{
		var absolutePath = new AbsolutePath(e.FullPath);
		var fileChangedArgs = new FileChangedArgs(absolutePath, e.Name);
		FileChanged?.Invoke(fileChangedArgs);
	}


	public void OnCreated(object sender, FileSystemEventArgs e)
	{
		var absolutePath = new AbsolutePath(e.FullPath);
		var fileChangedArgs = new FileCreatedArgs(absolutePath, e.Name);
		FileCreated?.Invoke(fileChangedArgs);
	}


	public void OnDeleted(object sender, FileSystemEventArgs e)
	{
		var absolutePath = new AbsolutePath(e.FullPath);
		var fileChangedArgs = new FileDeletedArgs(absolutePath, e.Name);
		FileDeleted?.Invoke(fileChangedArgs);
	}


	public void OnRenamed(object sender, RenamedEventArgs e)
	{
		var absolutePath = new AbsolutePath(e.FullPath);
		var oldAbsolutePath = new AbsolutePath(e.OldFullPath);

		var fileChangedArgs = new FileRenamedArgs(
			absolutePath,
			e.Name,
			oldAbsolutePath,
			e.OldName
		);

		FileRenamed?.Invoke(fileChangedArgs);
	}


	public void OnError(object sender, ErrorEventArgs e)
	{
		throw e.GetException();
	}


	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}


	protected virtual void Dispose(bool disposing)
	{
		if (!disposing) return;
		fileSystemWatcher.Dispose();
	}
}
