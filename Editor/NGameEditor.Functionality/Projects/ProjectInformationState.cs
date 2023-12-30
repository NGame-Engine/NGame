using NGameEditor.Bridge.Projects;

namespace NGameEditor.Functionality.Projects;



public class ProjectInformationState
{
	public event Action<ProjectInformation>? ProjectInformationUpdated;

	public ProjectInformation ProjectInformation { get; private set; } =
		new ProjectInformation([]);


	public void SetProjectInformation(ProjectInformation projectInformation)
	{
		ProjectInformation = projectInformation;
		ProjectInformationUpdated?.Invoke(ProjectInformation);
	}
}
