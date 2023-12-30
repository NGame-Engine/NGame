using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Backend.InterProcessCommunication;



public static class BackendIpcInstaller
{
	public static void AddBackendIpc(this IHostApplicationBuilder builder)
	{
		builder.AddIpcCommon();


		builder.Services.AddHostedService<BackendHostedService>();
		builder.Services.AddTransient<IBackendApi, BackendApi>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IHostRunnerFactory>().Create()
		);


		builder.AddFrontendCommandSender();

		builder.Services.AddTransient<IFrontendApiFactory, FrontendApiFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IFrontendApiFactory>().Create()
		);
	}
}
