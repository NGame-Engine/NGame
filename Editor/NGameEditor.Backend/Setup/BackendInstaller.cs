using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Setup;
using NGameEditor.Backend.Configurations;
using NGameEditor.Backend.Files;
using NGameEditor.Backend.InterProcessCommunication;
using NGameEditor.Backend.Projects.Setup;
using NGameEditor.Backend.Scenes;
using NGameEditor.Backend.Setup.ApplicationConfigurations;
using NGameEditor.Backend.UserInterface;

namespace NGameEditor.Backend.Setup;



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
		builder.Services.AddNGameCommon();
		builder.AddProjectFiles();
	}
}
