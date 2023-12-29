namespace NGameEditor.Bridge.Files;



public class DirectoryDescription(
	string name,
	List<DirectoryDescription> subDirectories,
	List<FileDescription> files
)
{
	public string Name { get; } = name;
	public List<DirectoryDescription> SubDirectories { get; } = subDirectories;
	public List<FileDescription> Files { get; } = files;
}
