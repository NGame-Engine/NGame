using Microsoft.Extensions.DependencyInjection;

namespace NGameEditor.Bridge.InterProcessCommunication;



public static class BackendInstaller
{
	public static void AddBackend(this IServiceCollection services)
	{
		services.AddSingleton<IBackendRunner, BackendRunner>();
		services.AddSingleton<IBackendProcessRunner, BackendProcessRunner>();
	}
}
