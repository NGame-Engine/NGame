using NGame.Ecs;

namespace NGame.Implementations.Ecs;



// ReSharper disable NotAccessedPositionalProperty.Global
public record RootSceneChangedArgs(Scene OldScene, Scene NewScene);
// ReSharper restore NotAccessedPositionalProperty.Global



public interface IRootSceneAccessor
{
	// ReSharper disable once EventNeverSubscribedTo.Global
	event Action<RootSceneChangedArgs> RootSceneChanged;

	Scene RootScene { get; }

	void SetRootScene(Scene scene);
}

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
