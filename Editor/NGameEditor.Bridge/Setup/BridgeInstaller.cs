using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGameEditor.Bridge.Setup;



public static class BridgeInstaller
{
	public static void AddBridge(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<ISolutionConfigurationReader, SolutionConfigurationReader>();
	}
}
