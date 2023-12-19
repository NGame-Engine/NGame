namespace NGameEditor.Bridge.Scenes;



public class ComponentDescription(
	string name,
	bool isRecognized,
	List<PropertyDescription> properties
)
{
	public string Name { get; } = name;
	public bool IsRecognized { get; } = isRecognized;
	public List<PropertyDescription> Properties { get; } = properties;
}
