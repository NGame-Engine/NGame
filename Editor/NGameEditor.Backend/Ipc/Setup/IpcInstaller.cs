using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Backend.Setup.ApplicationConfigurations;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using ServiceWire.TcpIp;

namespace NGameEditor.Backend.Ipc.Setup;



public static class IpcInstaller
{
	public static void AddIpc(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<ITcpHostFactory, TcpHostFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<ITcpHostFactory>().Create()
		);


		builder.Services.AddHostedService<BackendHostedService>();
		builder.Services.AddSingleton<IIpcRunner, IpcRunner>();
		builder.Services.AddTransient<IBackendService, BackendService>();

		builder.Services.AddSingleton(services =>
			{
				var applicationConfiguration = services.GetRequiredService<ApplicationConfiguration>();
				var ipEndPoint = applicationConfiguration.FrontendIpEndPoint;
				return new TcpClient<IFrontendApi>(ipEndPoint);
			}
		);
		builder.Services.AddTransient(services =>
			services.GetRequiredService<TcpClient<IFrontendApi>>().Proxy
		);
	}
}
