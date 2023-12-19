using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGameEditor.Backend.Projects.Setup;



public static class ProjectsInstaller
{
	public static void AddProjects(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<IProjectDefinitionFactory, ProjectDefinitionFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IProjectDefinitionFactory>().Create()
		);

		builder.Services.AddTransient<IProjectStateFactory, ProjectStateFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IProjectStateFactory>().Create()
		);
	}
}
