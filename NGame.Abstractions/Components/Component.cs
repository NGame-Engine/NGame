namespace NGame.Components;



public class Component
{
	public readonly Guid Id = Guid.NewGuid();
	public Entity? Entity;
}



public static class ComponentExtensions
{
	public static void Destroy(this Component component) =>
		component.Entity!.Scene.Destroy(component);
}
