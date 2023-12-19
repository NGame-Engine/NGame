namespace NGameEditor.Bridge.Scenes;



public class EntityDescription(
	Guid id,
	string name,
	List<ComponentDescription> components,
	List<EntityDescription> children
)
{
	public Guid Id { get; } = id;
	public string Name { get; } = name;
	public List<ComponentDescription> Components { get; } = components;
	public List<EntityDescription> Children { get; } = children;
}
