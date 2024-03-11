using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets.UsageFinder;
using NGame.Assets.UsageFinder.Setup;

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
					.Select(x =>
					{
						var taskItem = new TaskItem(x.MainPathInfo.SourcePath.PathDisplay);
						taskItem.SetMetadata("AssetId", x.Id.ToString());
						taskItem.SetMetadata("ProjectDirectory", $"{x.ProjectDirectory.PathDisplay}/");
						taskItem.SetMetadata("Package", x.PackageName);
						taskItem.SetMetadata("JsonFilePath", x.MainPathInfo.TargetPath.PathDisplay);
						taskItem.SetMetadata("DataFilePath", x.SatellitePathInfo?.TargetPath.PathDisplay);
						return taskItem;
					})
					.ToArray<ITaskItem>();
		}
		catch (Exception e)
		{
			Log.LogErrorFromException(e);
		}

		return Log.HasLoggedErrors == false;
	}
}
