using Microsoft.Extensions.DependencyInjection;
using NGame.Assets;
using NGame.Assets.Implementations;
using NGame.Core.Assets.ContentTables;
using NGame.Core.Assets.Json;
using NGame.Core.Assets.Readers;
using NGame.Core.Assets.Registries;
using NGame.Setup;

namespace NGame.Core.Assets;



public static class AssetsInstaller
{
	public static INGameBuilder AddAssets(
		this INGameBuilder builder
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

		builder.Services.AddTransient<IAssetTypeFinder, AssetTypeFinder>();

		return builder;
	}
}
