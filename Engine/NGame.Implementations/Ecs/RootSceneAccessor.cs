using NGame.Ecs;

namespace NGame.Implementations.Ecs;



public class RootSceneAccessor : IRootSceneAccessor
{
	public event Action<RootSceneChangedArgs>? RootSceneChanged;

	public Scene RootScene { get; private set; } = new();


	public void SetRootScene(Scene scene)
	{
		var oldScene = RootScene;
		RootScene = scene;
		var args = new RootSceneChangedArgs(oldScene, RootScene);
		RootSceneChanged?.Invoke(args);
	}
}
