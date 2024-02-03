using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace NGame.Implementations.Tests;



public static class XUnitLoggerInstaller
{
	public static ILoggingBuilder AddXUnitLogging(
		this ILoggingBuilder loggingBuilder,
		ITestOutputHelper testOutputHelper
	)
	{
		loggingBuilder.Services.AddSingleton<ILoggerProvider>(
			_ => new XUnitLoggerProvider(testOutputHelper)
		);

		return loggingBuilder;
	}
}



internal class XUnitLogger : ILogger
{
	private readonly ITestOutputHelper _testOutputHelper;
	private readonly string? _categoryName;
	private readonly LoggerExternalScopeProvider _scopeProvider;


	public static ILogger CreateLogger(ITestOutputHelper testOutputHelper) =>
		new XUnitLogger(testOutputHelper, new LoggerExternalScopeProvider(), "");


	public static ILogger<T> CreateLogger<T>(ITestOutputHelper testOutputHelper) =>
		new XUnitLogger<T>(testOutputHelper, new LoggerExternalScopeProvider());


	public XUnitLogger(
		ITestOutputHelper testOutputHelper,
		LoggerExternalScopeProvider scopeProvider,
		string? categoryName
	)
	{
		_testOutputHelper = testOutputHelper;
		_scopeProvider = scopeProvider;
		_categoryName = categoryName;
	}


	public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;


	public IDisposable BeginScope<TState>(TState state) where TState : notnull =>
		_scopeProvider.Push(state);


	public void Log<TState>(
		LogLevel logLevel,
		EventId eventId,
		TState state,
		Exception? exception,
		Func<TState, Exception?, string> formatter
	)
	{
		var sb = new StringBuilder();
		sb.Append(GetLogLevelString(logLevel))
			.Append(" [").Append(_categoryName).Append("] ")
			.Append(formatter(state, exception));

		if (exception != null)
		{
			sb.Append('\n').Append(exception);
		}

		// Append scopes
		_scopeProvider.ForEachScope((scope, scopeState) =>
		{
			scopeState.Append("\n => ");
			scopeState.Append(scope);
		}, sb);

		_testOutputHelper.WriteLine(sb.ToString());
	}


	private static string GetLogLevelString(LogLevel logLevel)
	{
		return logLevel switch
		{
			LogLevel.Trace => "trce",
			LogLevel.Debug => "dbug",
			LogLevel.Information => "info",
			LogLevel.Warning => "warn",
			LogLevel.Error => "fail",
			LogLevel.Critical => "crit",
			_ => throw new ArgumentOutOfRangeException(nameof(logLevel))
		};
	}
}



internal sealed class XUnitLogger<T> : XUnitLogger, ILogger<T>
{
	public XUnitLogger(ITestOutputHelper testOutputHelper, LoggerExternalScopeProvider scopeProvider)
		: base(testOutputHelper, scopeProvider, typeof(T).FullName)
	{
	}
}



internal sealed class XUnitLoggerProvider(
	ITestOutputHelper testOutputHelper
) : ILoggerProvider
{
	private readonly LoggerExternalScopeProvider _scopeProvider = new();


	public ILogger CreateLogger(string categoryName)
	{
		return new XUnitLogger(testOutputHelper, _scopeProvider, categoryName);
	}


	public void Dispose()
	{
	}
}
