using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGameEditor.Backend.Files;



public static class FilesInstaller
{
	public static void AddProjectFiles(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<ProjectFileStatus>();

		builder.Services.AddTransient<IAssetController, AssetController>();
		builder.Services.AddTransient<IBackendAssetDeserializer, BackendAssetDeserializer>();

		builder.Services.AddTransient<IBackendStartListener, ProjectFileInitializer>();


		builder.Services.AddTransient<IProjectFileWatcherFactory, ProjectFileWatcherFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IProjectFileWatcherFactory>().Create()
		);

		builder.Services.AddTransient<ISceneFileWatcherFactory, SceneFileWatcherFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<ISceneFileWatcherFactory>().Create()
		);

		builder.Services.AddTransient<IAssetFileWatcherFactory, AssetFileWatcherFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IAssetFileWatcherFactory>().Create()
		);
	}
}
