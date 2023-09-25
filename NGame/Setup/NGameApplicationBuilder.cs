using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NGame.Setup;

public interface INGameApplicationBuilder
{
	public IHostEnvironment Environment { get; }
	public ConfigurationManager Configuration { get; }
	public IServiceCollection Services { get; }
	public ILoggingBuilder Logging { get; }

	NGameApplication Build();
}



internal class NGameApplicationBuilder : INGameApplicationBuilder
{
	private readonly HostApplicationBuilder _hostApplicationBuilder = new();


	public IHostEnvironment Environment => _hostApplicationBuilder.Environment;
	public ConfigurationManager Configuration => _hostApplicationBuilder.Configuration;
	public IServiceCollection Services => _hostApplicationBuilder.Services;
	public ILoggingBuilder Logging => _hostApplicationBuilder.Logging;


	public NGameApplication Build()
	{
		var host = _hostApplicationBuilder.Build();
		return new NGameApplication(host);
	}
}
