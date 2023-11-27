using Microsoft.Extensions.Options;
using NGame.Assets;
using NGame.SceneAssets;

namespace NGame.Core.SceneAssets;



public interface ISceneAssetsConfigurationValidator
{
	ValidSceneAssetsConfiguration Validate();
}



public record ValidSceneAssetsConfiguration(ISet<AssetId> SceneIds, AssetId StartSceneId);



public class SceneAssetsConfigurationValidator : ISceneAssetsConfigurationValidator
{
	private readonly SceneAssetsConfiguration _sceneAssetsConfiguration;


	public SceneAssetsConfigurationValidator(IOptions<SceneAssetsConfiguration> nGameConfiguration)
	{
		_sceneAssetsConfiguration = nGameConfiguration.Value;
	}


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
