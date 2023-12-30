using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Projects;
using NGameEditor.Functionality.Users;

namespace NGameEditor.Functionality.Projects;



public interface IProjectUsageRepository
{
	void MarkProjectAsOpened(ProjectId projectId);
	IEnumerable<ProjectUsage> GetRecentlyOpenedProjects();
}



public class ProjectUsageRepository : IProjectUsageRepository
{
	private readonly IConfigRepository _configRepository;


	public ProjectUsageRepository(IConfigRepository configRepository)
	{
		_configRepository = configRepository;
	}


	public void MarkProjectAsOpened(ProjectId projectId)
	{
		var nGameStudioConfiguration = _configRepository.GetAppConfiguration();

		nGameStudioConfiguration
			.ProjectHistory
			.RemoveAll(x => x.ProjectId == projectId);

		var projectUsage = new ProjectUsage(projectId, DateTime.UtcNow);
		nGameStudioConfiguration.ProjectHistory.Add(projectUsage);

		_configRepository.SaveAppConfiguration(nGameStudioConfiguration);
	}


	public IEnumerable<ProjectUsage> GetRecentlyOpenedProjects() =>
		_configRepository
			.GetAppConfiguration()
			.ProjectHistory
			.OrderByDescending(x => x.LastUsed);
}
