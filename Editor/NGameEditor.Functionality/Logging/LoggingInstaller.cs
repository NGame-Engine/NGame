using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGameEditor.Functionality.Logging;



public static class LoggingInstaller
{
	public static void AddLogging(this IHostApplicationBuilder builder)
	{
		builder.Logging.AddUiLogger();

		builder.Services.AddTransient<UiLogger>();
		builder.Services.AddTransient<Func<UiLogger>>(services =>
			services.GetRequiredService<UiLogger>
		);
	}
}
