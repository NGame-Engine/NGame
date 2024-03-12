using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Platform.Assets.Json;
using NGame.Platform.Assets.Processors;
using NGame.Platform.Assets.Registries;

namespace NGame.Platform.Assets;



public static class PlatformAssetsInstaller
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder AddPlatformAssets(
		this IHostApplicationBuilder builder
	)
	{
		builder.Services.AddTransient<IAssetProcessorCollection, AssetProcessorCollection>();
		builder.Services.AddTransient<IAssetSerializer, AssetSerializer>();
		builder.Services.AddSingleton<IAssetRegistry, AssetRegistry>();
		builder.Services.AddTransient<IAssetAccessor, AssetAccessor>();

		return builder;
	}
}
