namespace NGameEditor.Bridge.Scenes;



public class ComponentDescription(
	Guid id,
	string name,
	bool isRecognized,
	List<PropertyDescription> properties
)
{
	public Guid Id { get; } = id;
	public string Name { get; } = name;
	public bool IsRecognized { get; } = isRecognized;
	public List<PropertyDescription> Properties { get; } = properties;
}
