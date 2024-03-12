using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets.Common.Setup;
using NGame.Assets.UsageFinder.AssetOverviews;
using NGame.Assets.UsageFinder.AssetUsages;
using NGame.Assets.UsageFinder.Commands;

namespace NGame.Assets.UsageFinder.Setup;



public static class UsageFinderInstaller
{
	public static void AddUsageFinder(
		this IHostApplicationBuilder builder
	)
	{
		builder.AddNGameAssetsCommon();


		builder.Services.AddTransient<ICommandRunner, CommandRunner>();

		builder.Services.AddTransient<IParameterValidator, ParameterValidator>();


		builder.Services.AddTransient<IAssetUsageFinder, AssetUsageFinder>();

		builder.Services.AddTransient<IAssetOverviewCreator, AssetOverviewCreator>();

		builder.Services.AddTransient<IAssetUsageOverviewFactory, AssetUsageOverviewFactory>();
		builder.Services.AddTransient<IJsonNodeAssetIdFinder, JsonNodeAssetIdFinder>();
	}
}
