using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGameEditor.Functionality.Users;



public static class ConfigurationInstaller
{
	public static void AddConfigurations(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<IConfigRepository, ConfigRepository>();
	}
}
