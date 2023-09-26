using NGame.Ecs;

namespace NGame.Scenes;

public class Scene
{
	public ICollection<Entity> Entities { get; set; } = new List<Entity>();
}
