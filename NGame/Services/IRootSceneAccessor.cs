using NGame.Ecs;

namespace NGame.Services;



public interface IRootSceneAccessor
{
	IScene RootScene { get; }

	void SetRootScene(IScene scene);
}
