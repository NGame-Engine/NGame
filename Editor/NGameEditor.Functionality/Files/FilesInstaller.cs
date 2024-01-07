using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGameEditor.Functionality.Files;



public static class FilesInstaller
{
	public static void AddFiles(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<IFileBrowserUpdater, FileBrowserUpdater>();
		builder.Services.AddTransient<IAssetInspectorMapper, AssetInspectorMapper>();
	}
}
