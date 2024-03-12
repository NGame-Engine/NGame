using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace NGame.Assets.UsageFinder.Setup;



public static class AbstractedLoggerInstaller
{
	public static ILoggingBuilder AddAbstractedLogger(
		this ILoggingBuilder builder,
		LogActionSet logActionSet
	)
	{
		builder.AddConfiguration();

		builder.Services.AddSingleton(logActionSet);

		builder.Services.TryAddEnumerable(
			ServiceDescriptor.Singleton<ILoggerProvider, AbstractedLoggerProvider>());

		return builder;
	}
}



public sealed class AbstractedLogger(
	LogActionSet logActionSet
) : ILogger
{
	public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;


	public bool IsEnabled(LogLevel logLevel) => true;


	public void Log<TState>(
		LogLevel logLevel,
		EventId eventId,
		TState state,
		Exception? exception,
		Func<TState, Exception?, string> formatter
	)
	{
		var text = formatter(state, exception);

		var logAction =
			logLevel switch
			{
				LogLevel.Trace => logActionSet.LogTrace,
				LogLevel.Debug => logActionSet.LogDebug,
				LogLevel.Information => logActionSet.LogInformation,
				LogLevel.Warning => logActionSet.LogWarning,
				LogLevel.Error => logActionSet.LogError,
				LogLevel.Critical => logActionSet.LogCritical,
				_ => logActionSet.LogCritical
			};

		logAction(text);
	}
}



public sealed class AbstractedLoggerProvider(
	LogActionSet logActionSet
) : ILoggerProvider
{
	private readonly ConcurrentDictionary<string, AbstractedLogger> _loggers =
		new(StringComparer.OrdinalIgnoreCase);


	public ILogger CreateLogger(string categoryName) =>
		_loggers.GetOrAdd(
			categoryName,
			_ => new AbstractedLogger(logActionSet)
		);


	public void Dispose()
	{
		_loggers.Clear();
	}
}
