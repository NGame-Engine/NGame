namespace NGameEditor.Backend.UserInterface.ValueEditors;



public interface IValueEditorFactory
{
	public bool CanHandleType(Type type);


	public EditorElement Create(object? value, Action<object?> setValue);
}
