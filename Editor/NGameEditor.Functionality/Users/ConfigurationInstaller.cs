using Microsoft.Extensions.DependencyInjection;

namespace NGameEditor.Functionality.Users;



public static class ConfigurationInstaller
{
	public static void AddConfigurations(this IServiceCollection services)
	{
		services.AddTransient<IConfigRepository, ConfigRepository>();
	}
}
