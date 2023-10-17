namespace NGame.Services;



public class FileEntry
{
	public FileEntry(string path)
	{
		Path = path;
	}


	public string Path { get; }
}



public interface IFileProvider
{
	Stream GetFile(string path);
	ICollection<FileEntry> GetAllAssets();
	Stream GetStream(FileEntry fileEntry);
}
