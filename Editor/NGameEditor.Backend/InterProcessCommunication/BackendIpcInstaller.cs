using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Bridge;

namespace NGameEditor.Backend.InterProcessCommunication;



public static class BackendIpcInstaller
{
	public static void AddBackendIpc(this IHostApplicationBuilder builder)
	{
		builder.AddIpcCommon();


		builder.AddBackendCommandReceiver();

		builder.Services.AddHostedService<BackendHostedService>();
		builder.Services.AddTransient<IBackendApi, BackendApi>();


		builder.AddFrontendCommandSender();

		builder.Services.AddTransient<IFrontendApiFactory, FrontendApiFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IFrontendApiFactory>().Create()
		);
	}
}
