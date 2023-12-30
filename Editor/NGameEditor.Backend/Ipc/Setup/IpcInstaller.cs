using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Backend.Setup.ApplicationConfigurations;
using NGameEditor.Bridge;
using NGameEditor.Bridge.Frontend;
using NGameEditor.Bridge.InterProcessCommunication;
using ServiceWire.TcpIp;

namespace NGameEditor.Backend.Ipc.Setup;



public static class IpcInstaller
{
	public static void AddIpc(this IHostApplicationBuilder builder)
	{
		builder.AddIpcFrontend();


		builder.Services.AddHostedService<BackendHostedService>();
		builder.Services.AddTransient<IBackendService, BackendService>();


		builder.Services.AddTransient<IFrontendApiFactory, FrontendApiFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IFrontendApiFactory>().Create()
		);
	}
}
