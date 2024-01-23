using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Backend.UserInterface.ValueEditors;



public class BoolValueEditorFactory : IValueEditorFactory
{
	public bool CanHandleType(Type type) => type == typeof(bool);


	public EditorElement Create(Type type, Func<object?> getValue, Action<object?> setValue)
	{
		var id = Guid.NewGuid();

		return new EditorElement(
			new UiElementDto(
				id,
				UiElementType.CheckBox,
				((bool?)getValue())?.ToString(),
				[]
			),
			new ValueUpdater(
				x =>
				{
					if (bool.TryParse(x, out var result) == false)
					{
						return Result.Error($"Unable to parse bool '{x}'");
					}

					setValue(result);
					return Result.Success();
				}
			),
			[]
		);
	}
}
