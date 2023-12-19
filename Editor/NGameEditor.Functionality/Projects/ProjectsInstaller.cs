using Microsoft.Extensions.DependencyInjection;
using NGameEditor.Functionality.Scenes;

namespace NGameEditor.Functionality.Projects;



public static class ProjectsInstaller
{
	public static void AddProjects(this IServiceCollection services)
	{
		services.AddTransient<IProjectCreator, ProjectCreator>();

		services.AddSingleton<IProjectState, ProjectState>();


		services.AddTransient<IProjectOpener, ProjectOpener>();
		services.AddTransient<IProjectUsageRepository, ProjectUsageRepository>();
		services.AddTransient<IEntityStateMapper, EntityStateMapper>();
	}
}
