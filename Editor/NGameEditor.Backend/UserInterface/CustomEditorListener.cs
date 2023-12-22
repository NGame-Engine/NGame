using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Backend.UserInterface.ComponentEditors;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Backend.UserInterface;



public interface ICustomEditorListener
{
	Result<UiElementDto> GetEditorForEntity(Guid entityId);
	Result UpdateEditorValue(Guid uiElementId, string? serializedNewValue);
}



public class CustomEditorListener(
	ISceneState sceneState,
	IEnumerable<IComponentEditorElementFactory> componentEditorElementFactories,
	IDefaultComponentEditorElementFactory defaultComponentEditorElementFactory
) : ICustomEditorListener
{
	private readonly Dictionary<Guid, IValueUpdater> _editors = new();

	private readonly Dictionary<Type, IComponentEditorElementFactory> _componentEditorElementFactories =
		componentEditorElementFactories.ToDictionary(x => x.Type);


	public Result<UiElementDto> GetEditorForEntity(Guid entityId)
	{
		_editors.Clear();

		var entityResult =
			sceneState
				.LoadedBackendScene
				.SceneAsset
				.GetEntityById(entityId);
		if (entityResult.HasError) return Result.Error(entityResult.ErrorValue!);

		var entityEntry = entityResult.SuccessValue!;


		var editorElements =
			entityEntry
				.Components
				.SelectMany(
					entityComponent =>
					{
						var componentEditorElementFactory =
							_componentEditorElementFactories.GetValueOrDefault(
								entityComponent.GetType(),
								defaultComponentEditorElementFactory
							);

						return componentEditorElementFactory.Create(entityComponent);
					}
				);

		var uiElements = new List<UiElementDto>();
		foreach (var editorElement in editorElements)
		{
			var uiElementDto = editorElement.UiElementDto;
			uiElements.Add(uiElementDto);

			var valueEditor = editorElement.ValueEditor;
			if (valueEditor == null) continue;

			_editors.Add(uiElementDto.Id, valueEditor);
		}

		var uiElement =
			new UiElementDto(
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
