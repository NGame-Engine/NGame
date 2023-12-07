using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets;
using NGame.Core.Assets.ContentTables;
using NGame.Core.Assets.Json;
using NGame.Core.Assets.Readers;
using NGame.Core.Assets.Registries;

namespace NGame.Core.Assets;

public static class AssetsInstaller
{
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
		builder.Services.AddTransient<IAssetFromPackReader, AssetFromPackReader>();

		builder.Services.AddSingleton<IAssetStreamProvider>(FileAssetStreamProvider.CreateDefault());

		return builder;
	}
}
