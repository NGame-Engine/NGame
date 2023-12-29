using NGameEditor.Bridge.Files;

namespace NGameEditor.Backend.Files;



public class ProjectFileStatus(
	List<DirectoryDescription> directories
)
{
	public List<DirectoryDescription> Directories { get; } = directories;
}
