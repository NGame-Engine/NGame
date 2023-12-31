using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Functionality.Shared.Files;
using NGameEditor.Functionality.Shared.Json;

namespace NGameEditor.Functionality.Shared;



public static class NGameEditorDomainSharedInstaller
{
	public static void AddNGameEditorDomainShared(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<ICommandRunner, CommandRunner>();

		builder.Services.AddTransient<IFileAccessWrapper, FileAccessWrapper>();
		builder.Services.AddTransient<IAppFilePathProvider, AppFilePathProvider>();


		builder.Services.AddTransient<IEditorSerializerOptionsFactory, EditorSerializerOptionsFactory>();
		builder.Services.AddTransient(serviceProvider =>
			serviceProvider.GetRequiredService<IEditorSerializerOptionsFactory>().Create());
	}
}
