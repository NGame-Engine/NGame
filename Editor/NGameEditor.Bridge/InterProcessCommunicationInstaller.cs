using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Setup;

namespace NGameEditor.Bridge;



public static class InterProcessCommunicationInstaller
{
	public static void AddIpcCommon(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<ISolutionConfigurationReader, SolutionConfigurationReader>();
		builder.Services.AddTransient<IFreePortFinder, FreePortFinder>();

		builder.Services.AddTransient<IHostRunnerFactory, HostRunnerFactory>();
	}


	public static void AddBackendCommandSender(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<IBackendProcessRunner, BackendProcessRunner>();
		builder.Services.AddSingleton<IClientRunner<IBackendApi>, ClientRunner<IBackendApi>>();
	}


	public static void AddFrontendCommandSender(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<IClientRunner<IFrontendApi>, ClientRunner<IFrontendApi>>();
	}
}
