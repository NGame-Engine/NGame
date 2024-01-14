namespace NGameEditor.Bridge.Scenes;



public class ComponentTypeDefinition(
	string name,
	string identifier
)
{
	public string Name { get; } = name;
	public string Identifier { get; } = identifier;
}
