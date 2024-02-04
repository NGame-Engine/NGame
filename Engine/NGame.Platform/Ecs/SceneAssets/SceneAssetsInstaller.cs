using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Assets.Common.Ecs;
using NGame.Ecs;
using NGame.Setup;

namespace NGame.Platform.Ecs.SceneAssets;

public static class SceneAssetsInstaller
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder AddSceneAssets(this IHostApplicationBuilder builder)
	{
		builder.Services.Configure<SceneAssetsConfiguration>(
			builder.Configuration.GetSection(SceneAssetsConfiguration.JsonElementName));

		builder.Services.AddTransient<ISceneAssetsConfigurationValidator, SceneAssetsConfigurationValidator>();


		builder.Services.AddTransient<ISceneLoader, SceneLoader>();
		builder.Services.AddTransient<ISceneLoadOperationExecutor, SceneLoadOperationExecutor>();
		builder.Services.AddTransient<ISceneUnloadOperationExecutor, SceneUnloadOperationExecutor>();
		builder.Services.AddTransient<IScenePopulator, ScenePopulator>();


		builder.Services.AddTransient<IAssetReferenceReplacer, AssetReferenceReplacer>();


		builder.Services.AddTransient<IBeforeApplicationStartListener, GameStartLoader>();

		return builder;
	}
}
