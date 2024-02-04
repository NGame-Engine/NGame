using NGame.Ecs;

namespace NGame.Platform.Ecs.Implementations;



public class SceneEditor : ISceneEditor
{
	public void AddChildToParent(Scene child, Scene? parent)
	{
		child.Parent?.InternalChildren.Remove(child);
		child.Parent = parent;
		parent?.InternalChildren.Add(child);
	}
}
