namespace NGameEditor.Bridge.UserInterface;



public interface  IUiElement
{
}



public class StackPanel(List<IUiElement> uiElements) : IUiElement
{
	public List<IUiElement> UiElements { get; } = uiElements;
}

public class StringEditor(
	Func<string> getValueFunc,
	Action<string> setValueAction
) : IUiElement
{
	public Func<string> GetValueFunc { get; } = getValueFunc;
	public Action<string> SetValueAction { get; } = setValueAction;
}
