using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NGame.Setup;

namespace NGamePlatform.Base;



public class NGameBuilder : INGameBuilder
{
	public NGameBuilder(
		INGameEnvironment environment,
		IConfigurationRoot configuration,
		IServiceCollection services,
		ILoggingBuilder logging
	)
	{
		Environment = environment;
		Configuration = configuration;
		Services = services;
		Logging = logging;
	}


	public static NGameBuilder CreateDefault(Platform platform)
	{
		var gameServiceCollection = new ServiceCollection();
		gameServiceCollection.AddOptions();
		gameServiceCollection.AddLogging();

		return
			new NGameBuilder(
				new NGameEnvironment(
					"",
					platform,
					GetEnvironmentName()
				),
				new ConfigurationManager(),
				gameServiceCollection,
				new LoggingBuilder(gameServiceCollection)
			);
	}


	private static string GetEnvironmentName()
	{
#pragma warning disable CS0162 // Unreachable code detected
#if DEBUG
		return NGameEnvironment.Development;
#endif
		return NGameEnvironment.Production;
#pragma warning restore CS0162 // Unreachable code detected
	}


	public INGameEnvironment Environment { get; }
	public IConfigurationRoot Configuration { get; }
	public IServiceCollection Services { get; }
	public ILoggingBuilder Logging { get; }


	public NGame Build() =>
		new(
			Environment,
			Services
				.BuildServiceProvider(
					new ServiceProviderOptions
					{
						ValidateScopes = Environment.IsDevelopment(),
						ValidateOnBuild = Environment.IsDevelopment(),
					}
				)
		);



	private sealed class LoggingBuilder : ILoggingBuilder
	{
		public LoggingBuilder(IServiceCollection services)
		{
			Services = services;
		}


		public IServiceCollection Services { get; }
	}
}
