using NGame.Ecs;

namespace NGame.Scenes;

public sealed class Scene
{
	public ICollection<Entity> Entities { get; set; } = new List<Entity>();
}
