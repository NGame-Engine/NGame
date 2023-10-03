namespace NGame.Ecs;



public class Component
{
	public readonly Guid Id = Guid.NewGuid();
	public Entity Entity { get; internal set; } = null!;
}
