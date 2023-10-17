using NGame.Ecs;

namespace NGame.Components;



public class Component
{
	public Guid Id { get; init; } = Guid.NewGuid();
	public Entity? Entity;
}



public static class ComponentExtensions
{
	public static void Destroy(this Component component) =>
		component.Entity!.Transform.Scene.DestroyComponent(component);
}
