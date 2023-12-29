using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Bridge;

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
	}
}
