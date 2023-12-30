namespace NGameEditor.Bridge.Scenes;



public class ComponentTypeDefinition(
	string name,
	string fullTypeName
)
{
	public string Name { get; } = name;
	public string FullTypeName { get; } = fullTypeName;
}
