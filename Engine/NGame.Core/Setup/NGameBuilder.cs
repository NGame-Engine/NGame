using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGame.Setup;

namespace NGame.Core.Setup;



public class NGameBuilder : INGameBuilder
{
	private readonly HostApplicationBuilder _hostApplicationBuilder;


	public NGameBuilder(HostApplicationBuilder hostApplicationBuilder)
	{
		_hostApplicationBuilder = hostApplicationBuilder;
	}


	public IHostEnvironment Environment => _hostApplicationBuilder.Environment;
	public ConfigurationManager Configuration => _hostApplicationBuilder.Configuration;
	public IServiceCollection Services => _hostApplicationBuilder.Services;
	public ILoggingBuilder Logging => _hostApplicationBuilder.Logging;


	public static NGameBuilder CreateDefault(params string[] args)
	{
		var settings = new HostApplicationBuilderSettings { Args = args };
#if DEBUG
		settings.EnvironmentName = Environments.Development;
#endif

		var builder = new HostApplicationBuilder(settings);

		builder.Services.AddOptions();
		builder.Services.AddLogging();


		var callingAssemblyTitle =
			Assembly
				.GetCallingAssembly()
				.GetCustomAttribute<AssemblyTitleAttribute>()?
				.Title;

		if (callingAssemblyTitle != null)
		{
			builder.Environment.ApplicationName = callingAssemblyTitle;
		}


		if (builder.Environment.IsDevelopment())
		{
			builder.Logging.AddConsole();
		}

		return new NGameBuilder(builder);
	}


	public IHost Build() => _hostApplicationBuilder.Build();
}
