using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets.UsageFinder.Setup;

namespace NGame.Assets.UsageFinder;



public class TaskParameters(
	string[] assetLists,
	string[] appSettings
)
{
	public string[] AssetLists { get; } = assetLists;
	public string[] AppSettings { get; } = appSettings;
}



public static class Program
{
	public static void Main(params string[] args)
	{
		var builder = Host.CreateApplicationBuilder(args);

		var assetListsArg = builder.Configuration["AssetLists"];
		if (string.IsNullOrWhiteSpace(assetListsArg))
		{
			throw new InvalidOperationException("No AssetLists provided");
		}

		var assetLists = assetListsArg.Split(';');


		var appSettingsArg = builder.Configuration["AppSettings"];
		if (string.IsNullOrWhiteSpace(appSettingsArg))
		{
			var message = $"Invalid AppSettings '{appSettingsArg}'";
			throw new InvalidOperationException(message);
		}

		var appSettings = appSettingsArg.Split(';');
		var taskParameters = new TaskParameters(assetLists, appSettings);


		builder.AddUsageFinder();


		var host = builder.Build();

		var commandRunner = host.Services.GetRequiredService<ICommandRunner>();
		commandRunner.Run(taskParameters);
	}
}
