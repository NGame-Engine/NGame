namespace NGameEditor.Backend.UserInterface.ValueEditors;



public interface IValueEditorFactory
{
	public bool CanHandleType(Type type);


	public EditorElement Create(
		Type type,
		Func<object?> getValue,
		Action<object?> setValue
	);
}
