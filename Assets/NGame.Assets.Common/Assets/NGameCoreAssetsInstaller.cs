using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using NGame.Assets.Common.Ecs;

namespace NGame.Assets.Common.Assets;



public static class NGameCoreAssetsInstaller
{
	public static void AddNGameCoreAssets(this IServiceCollection services)
	{
		services.AddTransient<IAssetTypeFinder, AssetTypeFinder>();
		services.AddTransient<IAssetDeserializerOptionsFactory, AssetDeserializerOptionsFactory>();
		services.AddTransient<ISceneSerializerOptionsFactory, SceneSerializerOptionsFactory>();

		services.AddTransient<JsonConverter, SemVersionConverter>();
	}
}
