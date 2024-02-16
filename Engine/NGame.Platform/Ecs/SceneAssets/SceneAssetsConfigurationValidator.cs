using Microsoft.Extensions.Options;
using NGame.Assets.Common.Ecs;

namespace NGame.Platform.Ecs.SceneAssets;



public interface ISceneAssetsConfigurationValidator
{
	ValidSceneAssetsConfiguration Validate();
}



// TODO Clean up comment when list of included scene is used properly
// ReSharper disable once NotAccessedPositionalProperty.Global
public record ValidSceneAssetsConfiguration(ISet<Guid> SceneIds, Guid StartSceneId);



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
			.ToHashSet();

		var startSceneId = _sceneAssetsConfiguration.StartScene;

		return new ValidSceneAssetsConfiguration(sceneIds, startSceneId);
	}
}
