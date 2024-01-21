using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGameEditor.Backend.Files;



public static class FilesInstaller
{
	public static void AddProjectFiles(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<ProjectFileStatus>();

		builder.Services.AddTransient<IAssetController, AssetController>();

		builder.Services.AddTransient<IBackendStartListener, ProjectFileInitializer>();


		builder.Services.AddTransient<IProjectFileWatcherFactory, ProjectFileWatcherFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IProjectFileWatcherFactory>().Create()
		);
	}
}
