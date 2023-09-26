namespace NGame.Ecs;

public class Entity
{
	public Guid Id { get; init; }
	public string Name { get; set; }
	public ICollection<Component> Components { get; init; }
}
