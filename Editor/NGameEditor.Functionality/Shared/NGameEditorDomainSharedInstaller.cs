using Microsoft.Extensions.DependencyInjection;
using NGameEditor.Functionality.Shared.Files;
using NGameEditor.Functionality.Shared.Json;

namespace NGameEditor.Functionality.Shared;



public static class NGameEditorDomainSharedInstaller
{
	public static void AddNGameEditorDomainShared(this IServiceCollection services)
	{
		services.AddTransient<ICommandRunner, CommandRunner>();

		services.AddTransient<IFileAccessWrapper, FileAccessWrapper>();
		services.AddTransient<IAppFilePathProvider, AppFilePathProvider>();


		services.AddTransient<IEditorSerializerOptionsFactory, EditorSerializerOptionsFactory>();
		services.AddTransient(serviceProvider =>
			serviceProvider.GetRequiredService<IEditorSerializerOptionsFactory>().Create());
	}
}
