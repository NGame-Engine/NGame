using NGameEditor.Bridge.Projects;

namespace NGameEditor.Functionality.Projects;



public class ProjectInformationState
{
	// ReSharper disable once EventNeverSubscribedTo.Global
	public event Action<ProjectInformation>? ProjectInformationUpdated;

	public ProjectInformation ProjectInformation { get; private set; } =
		new([], []);


	public void SetProjectInformation(ProjectInformation projectInformation)
	{
		ProjectInformation = projectInformation;
		ProjectInformationUpdated?.Invoke(ProjectInformation);
	}
}
