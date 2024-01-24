using NGameEditor.Bridge.Shared;

namespace NGameEditor.Bridge.UserInterface;



public enum UiElementType
{
	StackPanel,
	CheckBox,
	TextEditor,
	Asset
}



public class UiElementDto(
	Guid id,
	UiElementType type,
	string? currentSerializedValue,
	UiElementDto[] children
)
{
	public Guid Id { get; } = id;
	public UiElementType Type { get; } = type;
	public string? CurrentSerializedValue { get; } = currentSerializedValue;
	public UiElementDto[] Children { get; } = children;
}



public class JsonAssetInfo
{
	public string TypeName { get; set; } = null!;
	public string TypeIdentifier { get; set; } = null!;
	public AbsolutePath? SelectedFilePath { get; set; }
}
