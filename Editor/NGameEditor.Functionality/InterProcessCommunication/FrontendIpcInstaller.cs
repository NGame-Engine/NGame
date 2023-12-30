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


		builder.AddFrontendCommandReceiver();

		builder.Services.AddTransient<IFrontendApi, FrontendApi>();


		builder.AddBackendCommandSender();


		builder.Services.AddSingleton<IBackendStarter, BackendStarter>();
	}
}
