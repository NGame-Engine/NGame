using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using NGame.SceneAssets;
using NGame.SceneAssets.Implementations;

namespace NGame.Assets.Implementations;



public static class NGameCoreAssetsInstaller
{
	public static void AddNGameCoreAssets(this IServiceCollection services)
	{
		services.AddTransient<IAssetTypeFinder, AssetTypeFinder>();
		services.AddTransient<IAssetDeserializerOptionsFactory, AssetDeserializerOptionsFactory>();
		services.AddTransient<ISceneDeserializerOptionsFactory, SceneDeserializerOptionsFactory>();

		services.AddTransient<JsonConverter, SemVersionConverter>();
		services.AddTransient<JsonConverter, AssetIdConverter>();
	}
}
