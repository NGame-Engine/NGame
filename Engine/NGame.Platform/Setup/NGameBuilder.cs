using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGame.Platform.Setup;

public static class NGameBuilder
{
	public static HostApplicationBuilder CreateDefault(params string[] args)
	{
		var builder = Host.CreateApplicationBuilder(args);

		builder.Services.AddOptions();

		var callingAssemblyTitle =
			Assembly
				.GetCallingAssembly()
				.GetCustomAttribute<AssemblyTitleAttribute>()?
				.Title;

		if (callingAssemblyTitle != null)
		{
			builder.Environment.ApplicationName = callingAssemblyTitle;
		}


		return builder;
	}
}
