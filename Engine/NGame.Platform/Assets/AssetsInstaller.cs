using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Platform.Assets.ContentTables;
using NGame.Platform.Assets.Json;
using NGame.Platform.Assets.Readers;
using NGame.Platform.Assets.Registries;

namespace NGame.Platform.Assets;

public static class AssetsInstaller
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder AddAssets(
		this IHostApplicationBuilder builder
	)
	{
		builder.Services.AddTransient<IAssetProcessorCollection, AssetProcessorCollection>();

		builder.Services.AddSingleton<ITableOfContentsProvider, TableOfContentsProvider>();
		builder.Services.AddTransient<IPackedAssetStreamReader, PackedAssetStreamReader>();
		builder.Services.AddTransient<IAssetSerializer, AssetSerializer>();
		builder.Services.AddTransient<IPackedAssetDeserializer, PackedAssetDeserializer>();
		builder.Services.AddSingleton<IAssetRegistry, AssetRegistry>();
		builder.Services.AddTransient<IAssetAccessor, AssetAccessor>();

		builder.Services.AddSingleton<IAssetStreamProvider>(FileAssetStreamProvider.CreateDefault());

		return builder;
	}
}
