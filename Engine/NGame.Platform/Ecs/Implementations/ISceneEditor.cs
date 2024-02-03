using NGame.Ecs;

namespace NGame.Platform.Ecs.Implementations;



public interface ISceneEditor
{
	void AddChildToParent(Scene child, Scene? parent);
}
