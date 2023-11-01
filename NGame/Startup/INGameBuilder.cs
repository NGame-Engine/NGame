using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NGame.Startup;



public interface INGameBuilder
{
	event Action<INGame> ApplicationStarting;

	INGameEnvironment Environment { get; }
	IConfigurationRoot Configuration { get; }
	IServiceCollection Services { get; }
	ILoggingBuilder Logging { get; }
}
