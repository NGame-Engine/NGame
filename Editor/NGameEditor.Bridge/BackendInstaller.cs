using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Bridge;



public static class BackendInstaller
{
	public static void AddBackendRunner(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<IBackendRunner, BackendRunner>();
		builder.Services.AddSingleton<IBackendProcessRunner, BackendProcessRunner>();

		builder.Services.AddTransient<IFreePortFinder, FreePortFinder>();
		builder.Services.AddTransient<IFrontendTcpHostFactory, FrontendTcpHostFactory>();

		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IFrontendTcpHostFactory>().Create()
		);
	}
}
