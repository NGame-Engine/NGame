namespace NGameEditor.Bridge.Files;



public class AssetTypeDefinition(
	string name,
	string identifier
)
{
	public string Name { get; } = name;
	public string Identifier { get; } = identifier;
}
