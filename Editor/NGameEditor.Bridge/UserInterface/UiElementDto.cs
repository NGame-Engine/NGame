namespace NGameEditor.Bridge.UserInterface;



public enum UiElementType
{
	StackPanel,
	CheckBox,
	TextEditor
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
