namespace NGameEditor.Functionality.Shared.Files;



public interface IFileAccessWrapper
{
	bool FileExists(string filePath);
	Stream OpenRead(string filePath);
	Stream OpenWrite(string filePath);
	string? GetDirectoryName(string filePath);
}



public class FileAccessWrapper : IFileAccessWrapper
{
	public bool FileExists(string filePath) => File.Exists(filePath);
	public Stream OpenRead(string filePath) => File.OpenRead(filePath);
	public Stream OpenWrite(string filePath) => File.OpenWrite(filePath);
	public string? GetDirectoryName(string filePath) => Path.GetDirectoryName(filePath);
}
