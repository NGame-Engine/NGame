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


	public Result UpdateEditorValue(Guid uiElementId, string? serializedNewValue) =>
		_editors.TryGetValue(uiElementId, out var editor)
			? editor.SetValue(serializedNewValue)
			: Result.Error($"Unable to find value editor with ID '{uiElementId}'");


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
				)
				.ToList();

		foreach (var editorElement in editorElements)
		{
			DiscoverEditorsRecursive(editorElement);
		}


		var uiElements =
			editorElements
				.Select(x => x.UiElementDto)
				.ToArray();

		var uiElement =
			new UiElementDto(
				Guid.NewGuid(),
				UiElementType.StackPanel,
				null,
				uiElements
			);

		return Result.Success(uiElement);
	}


	private void DiscoverEditorsRecursive(EditorElement editorElement)
	{
		var uiElementDto = editorElement.UiElementDto;

		foreach (var child in editorElement.Children)
		{
			DiscoverEditorsRecursive(child);
		}

		var valueEditor = editorElement.ValueEditor;
		if (valueEditor == null) return;

		_editors.Add(uiElementDto.Id, valueEditor);
	}
}
