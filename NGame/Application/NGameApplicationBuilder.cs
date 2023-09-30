using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NGame.Application;

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
	private readonly HostApplicationBuilder _builder =
		new(
			new HostApplicationBuilderSettings
			{
				ApplicationName =
					Assembly
						.GetEntryAssembly()?
						.GetCustomAttribute<AssemblyTitleAttribute>()?
						.Title
			}
		);


	public NGameApplicationBuilder()
	{
		_builder.Services.AddSingleton<NGameHostedService>();

		_builder.Services.AddHostedService(
			services => services.GetRequiredService<NGameHostedService>()
		);

		_builder.Services.AddSingleton<IApplicationEvents, ApplicationEvents>();
	}


	public IHostEnvironment Environment => _builder.Environment;
	public ConfigurationManager Configuration => _builder.Configuration;
	public IServiceCollection Services => _builder.Services;
	public ILoggingBuilder Logging => _builder.Logging;


	public NGameApplication Build()
	{
		var host = _builder.Build();
		return new NGameApplication(host);
	}
}
