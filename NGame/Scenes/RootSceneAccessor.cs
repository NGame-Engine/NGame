namespace NGame.Scenes;



public interface IRootSceneAccessor
{
	Scene RootScene { get; }
}



public class RootSceneAccessor : IRootSceneAccessor
{
	public RootSceneAccessor(Scene scene)
	{
		RootScene = scene;
	}


	public Scene RootScene { get; }
}
