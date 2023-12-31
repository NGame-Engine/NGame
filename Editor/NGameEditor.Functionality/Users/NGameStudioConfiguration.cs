using NGameEditor.Bridge.Projects;

namespace NGameEditor.Functionality.Users;



public record ProjectUsage(ProjectId ProjectId, DateTime LastUsed);



public class NGameStudioConfiguration
{
	public List<ProjectUsage> ProjectHistory { get; init; } = new();
}
