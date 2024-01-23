namespace NGameEditor.Bridge.Files;



public class AssetTypeDefinition(
	string name,
	string identifier,
	bool isRecognized
)
{
	public string Name { get; } = name;
	public string Identifier { get; } = identifier;
	public bool IsRecognized { get; } = isRecognized;
}
