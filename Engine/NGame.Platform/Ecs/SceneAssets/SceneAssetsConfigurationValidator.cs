using Microsoft.Extensions.Options;
using NGame.Assets;
using NGame.Tooling.Ecs;

namespace NGame.Platform.Ecs.SceneAssets;



public interface ISceneAssetsConfigurationValidator
{
	ValidSceneAssetsConfiguration Validate();
}



// TODO Clean up when list of included scene is used properly
// ReSharper disable once NotAccessedPositionalProperty.Global
public record ValidSceneAssetsConfiguration(ISet<AssetId> SceneIds, AssetId StartSceneId);



public class SceneAssetsConfigurationValidator(
	IOptions<SceneAssetsConfiguration> nGameConfiguration
)
	: ISceneAssetsConfigurationValidator
{
	private readonly SceneAssetsConfiguration _sceneAssetsConfiguration = nGameConfiguration.Value;


	public ValidSceneAssetsConfiguration Validate()
	{
		var sceneIds = _sceneAssetsConfiguration
			.Scenes
			.Select(AssetId.Create)
			.ToHashSet();

		var startSceneId = AssetId.Create(_sceneAssetsConfiguration.StartScene);

		return new ValidSceneAssetsConfiguration(sceneIds, startSceneId);
	}
}
