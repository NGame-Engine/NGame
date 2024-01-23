using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Functionality.Scenes;

namespace NGameEditor.Functionality.Projects;



public static class ProjectsInstaller
{
	public static void AddProjects(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<IProjectCreator, ProjectCreator>();
		builder.Services.AddTransient<IExistingProjectOpener, ExistingExistingProjectOpener>();

		builder.Services.AddTransient<IProjectOpener, ProjectOpener>();
		builder.Services.AddTransient<IProjectUsageRepository, ProjectUsageRepository>();
		builder.Services.AddTransient<IEntityStateMapper, EntityStateMapper>();

		builder.Services.AddSingleton<ProjectInformationState>();
	}
}
