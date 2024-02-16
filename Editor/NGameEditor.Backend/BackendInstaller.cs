using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets.Common.Setup;
using NGameEditor.Backend.Configurations;
using NGameEditor.Backend.Files;
using NGameEditor.Backend.InterProcessCommunication;
using NGameEditor.Backend.Projects;
using NGameEditor.Backend.Scenes;
using NGameEditor.Backend.UserInterface;

namespace NGameEditor.Backend;



public static class BackendInstaller
{
	public static void InstallBackend(this IHostApplicationBuilder builder, Type typeFromGameAssembly)
	{
		builder.Services.AddTransient<IApplicationConfigurationValidator, ApplicationConfigurationValidator>();
		builder.Services.AddSingleton(services =>
			services
				.GetRequiredService<IApplicationConfigurationValidator>()
				.Validate(builder.Configuration)
		);


		builder.AddConfigurations();
		builder.AddProjects();
		builder.AddScenes();
		builder.AddBackendIpc();
		builder.AddUserInterface();
		builder.AddNGameAssetsCommon();
		builder.AddProjectFiles();
	}
}
