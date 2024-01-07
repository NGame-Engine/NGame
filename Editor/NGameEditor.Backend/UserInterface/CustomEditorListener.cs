using System.Text.Json;
using NGame.Assets;
using NGameEditor.Backend.Projects;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Backend.UserInterface.AssetEditors;
using NGameEditor.Backend.UserInterface.ComponentEditors;
using NGameEditor.Bridge.Shared;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Backend.UserInterface;



public interface ICustomEditorListener
{
	Result<UiElementDto> GetEditorForEntity(Guid entityId);
	Result<UiElementDto> GetEditorForFile(AbsolutePath filePath);
	Result UpdateEditorValue(Guid uiElementId, string? serializedNewValue);
}



public class CustomEditorListener(
	ISceneState sceneState,
	IEnumerable<IComponentEditorElementFactory> componentEditorElementFactories,
	IDefaultComponentEditorElementFactory defaultComponentEditorElementFactory,
	IAssetDeserializerOptionsFactory assetDeserializerOptionsFactory,
	IEnumerable<IAssetEditorElementFactory> assetEditorElementFactories,
	IDefaultAssetEditorElementFactory defaultAssetEditorElementFactory,
	ProjectDefinition projectDefinition
) : ICustomEditorListener
{
	private readonly Dictionary<Guid, IValueUpdater> _editors = new();

	private readonly Dictionary<Type, IComponentEditorElementFactory> _componentEditorElementFactories =
		componentEditorElementFactories.ToDictionary(x => x.Type);

	private readonly Dictionary<Type, IAssetEditorElementFactory> _assetEditorElementFactories =
		assetEditorElementFactories.ToDictionary(x => x.Type);


	public Result UpdateEditorValue(Guid uiElementId, string? serializedNewValue) =>
		_editors.TryGetValue(uiElementId, out var editor)
			? editor.SetValue(serializedNewValue)
			: Result.Error($"Unable to find value editor with ID '{uiElementId}'");


	public Result<UiElementDto> GetEditorForFile(AbsolutePath filePath)
	{
		_editors.Clear();

		if (filePath.Path.EndsWith(AssetConventions.SceneFileEnding))
		{
			return GetEditorForSceneFile();
		}

		if (filePath.Path.EndsWith(AssetConventions.AssetFileEnding))
		{
			return GetEditorForAssetFile(filePath);
		}


		return Result.Success(
			new UiElementDto(
				Guid.NewGuid(),
				UiElementType.StackPanel,
				null,
				[]
			)
		);
	}


	private Result<UiElementDto> GetEditorForSceneFile() =>
		Result.Success(
			new UiElementDto(
				Guid.NewGuid(),
				UiElementType.StackPanel,
				null,
				[]
			)
		);


	private Result<UiElementDto> GetEditorForAssetFile(AbsolutePath filePath)
	{
		var allText = File.ReadAllText(filePath.Path);

		var assetTypes = projectDefinition.AssetTypes;
		var options = assetDeserializerOptionsFactory.Create(assetTypes);
		options.WriteIndented = true;

		Asset asset;

		try
		{
			asset = JsonSerializer.Deserialize<Asset>(allText, options)!;
		}
		catch (NotSupportedException e)
		{
			var assetTypeIsNotRecognized = e.Message.StartsWith(
				"Deserialization of types without a parameterless constructor",
				StringComparison.OrdinalIgnoreCase
			);

			if (assetTypeIsNotRecognized)
			{
				return Result.Error($"Unknown type of asset at {filePath.Path}");
			}

			throw;
		}

		var assetEditorElementFactory =
			_assetEditorElementFactories.GetValueOrDefault(
				asset.GetType(),
				defaultAssetEditorElementFactory
			);

		var editorElements =
			assetEditorElementFactory
				.Create(
					asset,
					x =>
					{
						var newContent = JsonSerializer.Serialize(x, options);
						File.WriteAllText(filePath.Path, newContent);
					})
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
