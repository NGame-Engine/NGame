namespace NGame.Ecs;



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
