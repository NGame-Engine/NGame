using NGameEditor.Bridge.Shared;

namespace NGameEditor.Backend.Files;



public record FileChangedArgs(AbsolutePath Path);

public record FileCreatedArgs(AbsolutePath Path);

public record FileDeletedArgs(AbsolutePath Path);

public record FileRenamedArgs(AbsolutePath Path, AbsolutePath OldPath);



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
		var fileChangedArgs = new FileChangedArgs(absolutePath);
		FileChanged?.Invoke(fileChangedArgs);
	}


	public void OnCreated(object sender, FileSystemEventArgs e)
	{
		var absolutePath = new AbsolutePath(e.FullPath);
		var fileChangedArgs = new FileCreatedArgs(absolutePath);
		FileCreated?.Invoke(fileChangedArgs);
	}


	public void OnDeleted(object sender, FileSystemEventArgs e)
	{
		var absolutePath = new AbsolutePath(e.FullPath);
		var fileChangedArgs = new FileDeletedArgs(absolutePath);
		FileDeleted?.Invoke(fileChangedArgs);
	}


	public void OnRenamed(object sender, RenamedEventArgs e)
	{
		var absolutePath = new AbsolutePath(e.FullPath);
		var oldAbsolutePath = new AbsolutePath(e.OldFullPath);
		var fileChangedArgs = new FileRenamedArgs(absolutePath, oldAbsolutePath);
		FileRenamed?.Invoke(fileChangedArgs);
	}


	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}


	protected void Dispose(bool disposing)
	{
		if (!disposing) return;
		fileSystemWatcher.Dispose();
	}
}
