using NGameEditor.Bridge.Files;

namespace NGameEditor.Backend.Files;



public class ProjectFileStatus
{
	public event Action<DirectoryDescription>? DirectoriesChanged;


	public DirectoryDescription RootDirectory { get; } = new("root", [], []);


	public void SetDirectories(List<DirectoryDescription> directoryDescriptions)
	{
		RootDirectory.SubDirectories.Clear();
		RootDirectory.SubDirectories.AddRange(directoryDescriptions);
		DirectoriesChanged?.Invoke(RootDirectory);
	}
}
