namespace NGame.Ecs;

public class Entity
{
	public Guid Id { get; init; }
	public string Name { get; set; } = "Entity";
	public ICollection<IComponent> Components { get; init; } = new List<IComponent>();
}
