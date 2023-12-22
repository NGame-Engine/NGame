using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Backend.UserInterface.ValueEditors;



public class BoolValueEditorFactory : IValueEditorFactory
{
	public Type ValueType { get; } = typeof(bool);


	public EditorElement Create(object? value, Action<object?> setValue)
	{
		var id = Guid.NewGuid();

		return new EditorElement(
			new UiElementDto(
				id,
				UiElementType.CheckBox,
				((bool?)value)?.ToString(),
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
