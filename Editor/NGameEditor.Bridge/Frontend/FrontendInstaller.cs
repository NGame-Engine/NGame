using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Bridge.Frontend;



public static class FrontendInstaller
{
	public static void AddIpcFrontend(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<IFreePortFinder, FreePortFinder>();
		builder.Services.AddTransient<IBackendHostFactory, BackendHostFactory>();
		builder.Services.AddTransient<IClientServiceFactory, ClientServiceFactory>();
	}
}
