using System.Collections.Concurrent;
using System.Runtime.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace NGameEditor.Functionality.Logging;



[UnsupportedOSPlatform("browser")]
public sealed class UiLoggerProvider(
	Func<UiLogger> factoryMethod
) : ILoggerProvider
{
	private readonly ConcurrentDictionary<string, UiLogger> _loggers =
		new(StringComparer.OrdinalIgnoreCase);


	public ILogger CreateLogger(string categoryName) =>
		_loggers.GetOrAdd(
			categoryName,
			_ => factoryMethod()
		);


	public void Dispose()
	{
		_loggers.Clear();
	}
}



public static class UiLoggerExtensions
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static ILoggingBuilder AddUiLogger(this ILoggingBuilder builder)
	{
		builder.Services.TryAddEnumerable(
			ServiceDescriptor.Singleton<ILoggerProvider, UiLoggerProvider>());

		return builder;
	}
}
