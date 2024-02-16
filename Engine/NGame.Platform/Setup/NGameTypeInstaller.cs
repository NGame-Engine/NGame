using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets.Common.Assets;
using NGame.Platform.Assets;
using NGame.Platform.Ecs.SceneAssets;

namespace NGame.Platform.Setup;



public static class NGameTypeInstaller
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder InstallComponentAndAssetTypes(
		this IHostApplicationBuilder builder,
		Assembly assembly
	)
	{
		var assetTypeFinder = new AssetTypeFinder();

		var componentTypes = assetTypeFinder.FindComponentTypes(assembly);
		foreach (var componentType in componentTypes)
		{
			builder.Services.AddSingleton(new ComponentTypeEntry(componentType));
		}

		var assetTypes = assetTypeFinder.FindAssetTypes(assembly);
		foreach (var assetType in assetTypes)
		{
			builder.Services.AddSingleton(new AssetTypeEntry(assetType));
		}

		return builder;
	}
}
