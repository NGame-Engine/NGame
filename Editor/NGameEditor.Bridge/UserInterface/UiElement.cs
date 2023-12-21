namespace NGameEditor.Bridge.UserInterface;



public enum UiElementType
{
	StackPanel,
	CheckBox,
	TextEditor
}



public class UiElement(
	Guid id,
	UiElementType type,
	string? currentSerializedValue, 
	UiElement[] children
)
{
	public Guid Id { get; } = id;
	public UiElementType Type { get; } = type;
	public string? CurrentSerializedValue { get; } = currentSerializedValue;
	public UiElement[] Children { get; } = children;
}
