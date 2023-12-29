using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGameEditor.Backend.Files;



public static class FilesInstaller
{
	public static void AddProjectFiles(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<IProjectFileStatusFactory, ProjectFileStatusFactory>();
		builder.Services.AddSingleton<ProjectFileStatus>(services =>
			services.GetRequiredService<IProjectFileStatusFactory>().Create()
		);
	}
}
