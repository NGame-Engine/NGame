using Singulink.IO;

namespace NGameEditor.Backend.Files;



public record FileChangedArgs(IAbsoluteFilePath Path);

public record FileCreatedArgs(IAbsoluteFilePath Path);

public record FileDeletedArgs(IAbsoluteFilePath Path);

public record FileRenamedArgs(IAbsoluteFilePath Path, IAbsoluteFilePath OldPath);



public interface IProjectFileWatcher
{
	event Action<FileChangedArgs> FileChanged;
	event Action<FileCreatedArgs> FileCreated;
	event Action<FileDeletedArgs> FileDeleted;
	event Action<FileRenamedArgs> FileRenamed;

	IEnumerable<IAbsoluteFilePath> GetAllFiles();
}



public class ProjectFileWatcher(
	HashSet<IAbsoluteFilePath> currentFiles,
	FileSystemWatcher fileSystemWatcher
) : IProjectFileWatcher, IDisposable
{
	public event Action<FileChangedArgs>? FileChanged;
	public event Action<FileCreatedArgs>? FileCreated;
	public event Action<FileDeletedArgs>? FileDeleted;
	public event Action<FileRenamedArgs>? FileRenamed;


	private HashSet<IAbsoluteFilePath> AllFiles { get; } = currentFiles;

	public IEnumerable<IAbsoluteFilePath> GetAllFiles() => AllFiles;


	public void OnChanged(object sender, FileSystemEventArgs e)
	{
		var absolutePath = FilePath.ParseAbsolute(e.FullPath);
		var fileChangedArgs = new FileChangedArgs(absolutePath);
		FileChanged?.Invoke(fileChangedArgs);
	}


	public void OnCreated(object sender, FileSystemEventArgs e)
	{
		var absolutePath = FilePath.ParseAbsolute(e.FullPath);
		var fileChangedArgs = new FileCreatedArgs(absolutePath);
		FileCreated?.Invoke(fileChangedArgs);
	}


	public void OnDeleted(object sender, FileSystemEventArgs e)
	{
		var absolutePath = FilePath.ParseAbsolute(e.FullPath);
		var fileChangedArgs = new FileDeletedArgs(absolutePath);
		FileDeleted?.Invoke(fileChangedArgs);
	}


	public void OnRenamed(object sender, RenamedEventArgs e)
	{
		var absolutePath = FilePath.ParseAbsolute(e.FullPath);
		var oldAbsolutePath = FilePath.ParseAbsolute(e.OldFullPath);
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
