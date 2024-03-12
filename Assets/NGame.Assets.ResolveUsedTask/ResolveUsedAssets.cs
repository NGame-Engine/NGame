using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets.UsageFinder;
using NGame.Assets.UsageFinder.Setup;
using NGame.Assets.UsageFinder.TaskItems;

namespace NGame.Assets.ResolveUsedTask;



public class ResolveUsedAssets : Microsoft.Build.Utilities.Task
{
	[Required] public string[] AssetLists { get; set; } = null!;
	[Required] public string[] AppSettings { get; set; } = null!;

	[Output] public ITaskItem[]? UsedAssets { get; set; }


	public override bool Execute()
	{
		try
		{
			var builder = Host.CreateApplicationBuilder();


			builder.Logging.AddAbstractedLogger(
				new LogActionSet(
					x => Log.LogMessage(MessageImportance.Low, x),
					x => Log.LogMessage(MessageImportance.Normal, x),
					x => Log.LogMessage(MessageImportance.High, x),
					x => Log.LogWarning(x),
					x => Log.LogError(x),
					x => Log.LogError(x)
				)
			);

			builder.AddUsageFinder();


			var host = builder.Build();


			var commandRunner = host.Services.GetRequiredService<ICommandRunner>();
			var taskParameters = new TaskParameters(AssetLists, AppSettings);
			var assetUsageOverview = commandRunner.Run(taskParameters);

			UsedAssets =
				assetUsageOverview
					.UsedAssetEntries
					.Select(UsedAssetMapper.Map)
					.ToArray();
		}
		catch (Exception e)
		{
			Log.LogErrorFromException(e);
		}

		return Log.HasLoggedErrors == false;
	}
}
