using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets.Common.Assets.JsonConverters;
using NGame.Assets.Common.Ecs;

namespace NGame.Assets.Common.Assets;



public static class NGameCoreAssetsInstaller
{
	public static IHostApplicationBuilder AddNGameCoreAssets(this IHostApplicationBuilder builder)
	{
		builder.AddNGameCoreJsonConverters();

		builder.Services.AddTransient<IAssetTypeFinder, AssetTypeFinder>();
		builder.Services.AddTransient<IAssetDeserializerOptionsFactory, AssetDeserializerOptionsFactory>();
		builder.Services.AddTransient<ISceneSerializerOptionsFactory, SceneSerializerOptionsFactory>();

		builder.Services.AddTransient<IStoredAssetReader, SimpleStoredAssetReader>();
		
		return builder;
	}
}
