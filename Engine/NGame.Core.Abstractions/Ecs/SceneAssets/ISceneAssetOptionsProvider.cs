using System.Text.Json;

namespace NGame.Ecs.SceneAssets;



public interface ISceneAssetOptionsProvider
{
	JsonSerializerOptions GetDeserializationOptions();
}
