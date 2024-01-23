using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Backend.UserInterface.ValueEditors;



public class StringValueEditorFactory : IValueEditorFactory
{
	public bool CanHandleType(Type type) => type == typeof(string);


	public EditorElement Create(Type type, Func<object?> getValue, Action<object?> setValue)
	{
		var id = Guid.NewGuid();

		return new EditorElement(
			new UiElementDto(
				id,
				UiElementType.TextEditor,
				((string?)getValue()),
				[]
			),
			new ValueUpdater(
				x =>
				{
					setValue(x);
					return Result.Success();
				}
			),
			[]
		);
	}
}
