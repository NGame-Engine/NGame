namespace NGame.Ecs;



public record RootSceneChangedArgs(Scene OldScene, Scene NewScene);



public interface IRootSceneAccessor
{
	event Action<RootSceneChangedArgs> RootSceneChanged;

	Scene RootScene { get; }

	void SetRootScene(Scene scene);
}
