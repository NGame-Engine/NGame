using NGame.Assets;

namespace NGame.Services.Scenes;



public class RootSceneAccessor : IRootSceneAccessor
{
	public RootSceneAccessor(Scene scene)
	{
		RootScene = scene;
	}


	public Scene RootScene { get; }
}
