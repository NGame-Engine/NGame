using System.Text.Json;

namespace NGame.SceneAssets;



public interface ISceneAssetOptionsProvider
{
	JsonSerializerOptions GetDeserializationOptions();
}
