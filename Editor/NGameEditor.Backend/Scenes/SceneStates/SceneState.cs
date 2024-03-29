using NGame.Assets.Common.Ecs;

namespace NGameEditor.Backend.Scenes.SceneStates;



public record LoadedSceneChangedEventArgs(BackendScene NewBackendScene);



public interface ISceneState
{
	event Action<LoadedSceneChangedEventArgs> LoadedSceneChanged;

	BackendScene LoadedBackendScene { get; }

	void SetLoadedScene(BackendScene backendSceneDescription);
}



public class SceneState : ISceneState
{
	public event Action<LoadedSceneChangedEventArgs>? LoadedSceneChanged;

	public BackendScene LoadedBackendScene { get; private set; } = new(null, new SceneAsset());


	public void SetLoadedScene(BackendScene backendSceneDescription)
	{
		LoadedBackendScene = backendSceneDescription;
		var eventArgs = new LoadedSceneChangedEventArgs(backendSceneDescription);
		LoadedSceneChanged?.Invoke(eventArgs);
	}
}
