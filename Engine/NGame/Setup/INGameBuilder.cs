using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NGame.Setup;



public interface INGameBuilder
{
	IHostEnvironment Environment { get; }
	ConfigurationManager Configuration { get; }
	IServiceCollection Services { get; }
	ILoggingBuilder Logging { get; }
}
