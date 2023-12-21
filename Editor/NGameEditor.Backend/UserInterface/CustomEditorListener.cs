using NGame.Ecs;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Backend.UserInterface;



public interface ICustomEditorListener
{
	Result<UiElement> GetEditorForEntity(Guid entityId);
	Result UpdateEditorValue(Guid uiElementId, string? serializedNewValue);
}



public interface IComponentEditorFactory
{
	Type Type { get; }
	UiElement Create(EntityComponent entityComponent);
}



public interface IValueEditor
{
	Result SetValue(string? serializedNewValue);
}



public class GenericValueEditor(Guid uiElementId, Func<string?, Result> applyValue) : IValueEditor
{
	public Guid Id { get; } = uiElementId;


	public Result SetValue(string? serializedNewValue) => applyValue(serializedNewValue);
}



public class CustomEditorListener(ISceneState sceneState) : ICustomEditorListener
{
	private readonly Dictionary<Guid, IValueEditor> _editors = new();


	public Result<UiElement> GetEditorForEntity(Guid entityId)
	{
		_editors.Clear();

		var entityResult =
			sceneState
				.LoadedBackendScene
				.SceneAsset
				.GetEntityById(entityId);
		if (entityResult.HasError) return Result.Error(entityResult.ErrorValue!);

		var entityEntry = entityResult.SuccessValue!;


		var uiElements = new List<UiElement>();
		foreach (var entityComponent in entityEntry.Components)
		{
			var editablePropertyInfos =
				entityComponent
					.GetType()
					.GetProperties()
					.Where(x => x.CanRead && x.CanWrite);

			foreach (var propertyInfo in editablePropertyInfos)
			{
				if (propertyInfo.PropertyType == typeof(bool))
				{
					var id = Guid.NewGuid();


					var boolEditor = new GenericValueEditor(
						id,
						x =>
						{
							if (bool.TryParse(x, out var value) == false)
							{
								return Result.Error($"Unable to parse bool '{x}'");
							}

							propertyInfo.SetValue(entityComponent, value);
							return Result.Success();
						}
					);

					_editors.Add(id, boolEditor);


					var rawValue = propertyInfo.GetValue(entityComponent);
					var currentValue = (bool)rawValue!;
					var currentSerializedValue = currentValue.ToString();
					uiElements.Add(
						new UiElement(
							id,
							UiElementType.CheckBox,
							currentSerializedValue,
							[]
						)
					);

					continue;
				}


				if (propertyInfo.PropertyType == typeof(string))
				{
					var id = Guid.NewGuid();


					var stringEditor = new GenericValueEditor(
						id,
						x =>
						{
							if (x == null)
							{
								return Result.Error("Unable to read null string");
							}

							propertyInfo.SetValue(entityComponent, x);
							return Result.Success();
						}
					);

					_editors.Add(id, stringEditor);


					var rawValue = propertyInfo.GetValue(entityComponent);
					var currentValue = (string)rawValue!;
					uiElements.Add(
						new UiElement(
							id,
							UiElementType.TextEditor,
							currentValue,
							[]
						)
					);
				}
			}
		}

		var uiElement =
			new UiElement(
				Guid.NewGuid(),
				UiElementType.StackPanel,
				null,
				uiElements.ToArray()
			);

		return Result.Success(uiElement);
	}


	public Result UpdateEditorValue(Guid uiElementId, string? serializedNewValue) =>
		_editors.TryGetValue(uiElementId, out var editor)
			? editor.SetValue(serializedNewValue)
			: Result.Error($"Unable to find value editor with ID '{uiElementId}'");
}
