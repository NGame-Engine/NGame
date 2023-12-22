namespace NGameEditor.Backend.UserInterface.ValueEditors;



public interface IValueEditorFactory
{
	public Type ValueType { get; }


	public EditorElement Create(object? value, Action<object?> setValue);
}
