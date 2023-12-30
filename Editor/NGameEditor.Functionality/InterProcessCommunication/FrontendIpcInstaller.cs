using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Functionality.InterProcessCommunication;



public static class FrontendIpcInstaller
{
	public static void AddFrontendIpc(this IHostApplicationBuilder builder)
	{
		builder.AddIpcCommon();


		builder.Services.AddTransient<IFrontendApi, FrontendApi>();
		builder.Services.AddTransient<IFrontendHostRunnerFactory, FrontendHostRunnerRunnerFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IFrontendHostRunnerFactory>().Create()
		);


		builder.AddBackendCommandSender();


		builder.Services.AddSingleton<IBackendStarter, BackendStarter>();
	}
}
