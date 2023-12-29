namespace NGameEditor.Bridge.Scenes;



public class ComponentTypeDefinition
{
	public ComponentTypeDefinition(string name, string fullTypeName)
	{
		Name = name;
		FullTypeName = fullTypeName;
	}


	public string Name { get; }
	public string FullTypeName { get; }
}
