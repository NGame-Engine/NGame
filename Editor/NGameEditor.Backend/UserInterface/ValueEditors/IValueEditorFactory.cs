namespace NGameEditor.Backend.UserInterface.ValueEditors;



public interface IValueEditorFactory
{
	public bool CanHandleType(Type type);


	public EditorElement Create(Type type, object? value, Action<object?> setValue);
}
