using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NGame.Setup;



public interface INGameBuilder
{
	public INGameEnvironment Environment { get; }
	public IConfigurationRoot Configuration { get; }
	public IServiceCollection Services { get; }
	public ILoggingBuilder Logging { get; }
}
