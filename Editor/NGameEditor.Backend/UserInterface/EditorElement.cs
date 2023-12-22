using NGameEditor.Bridge.UserInterface;

namespace NGameEditor.Backend.UserInterface;



public class EditorElement(
	UiElementDto uiElementDto,
	IValueUpdater? valueEditor,
	ICollection<EditorElement> children
)
{
	public UiElementDto UiElementDto { get; } = uiElementDto;
	public IValueUpdater? ValueEditor { get; } = valueEditor;
	public ICollection<EditorElement> Children { get; } = children;
}
