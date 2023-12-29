using NGame.Ecs;
using NGame.SceneAssets;
using NGameEditor.Backend.Files;
using NGameEditor.Backend.Projects;
using NGameEditor.Backend.Scenes;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Backend.UserInterface;
using NGameEditor.Bridge;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.Scenes;
using NGameEditor.Bridge.UserInterface;
using NGameEditor.Results;

namespace NGameEditor.Backend.Ipc;



public class BackendService(
	ProjectDefinition projectDefinition,
	ISceneState sceneState,
	ISceneDescriptionMapper sceneDescriptionMapper,
	ISceneSaver sceneSaver,
	ICustomEditorListener customEditorListener,
	ProjectFileStatus projectFileStatus
)
	: IBackendService
{
	public Result<DirectoryDescription> GetProjectFiles()
	{
		return Result.Success(
			new DirectoryDescription(
				"root",
				projectFileStatus.Directories
			)
		);
	}


	public SceneDescription GetLoadedScene()
	{
		var backendScene = sceneState.LoadedBackendScene;
		return sceneDescriptionMapper.Map(backendScene);
	}


	public Result SaveCurrentScene() => sceneSaver.SaveCurrentScene();


	public Result<EntityDescription> AddEntity(Guid? parentEntityId)
	{
		var backendScene = sceneState.LoadedBackendScene;
		var sceneAsset = backendScene.SceneAsset;

		var entityEntry = new EntityEntry
		{
			Id = Guid.NewGuid(),
			Name = "New Entity"
		};

		var entityDescription = sceneDescriptionMapper.Map(entityEntry);

		if (parentEntityId != null)
		{
			return
				sceneAsset
					.GetEntityById(parentEntityId.Value)
					.Then(x => x.Children.Add(entityEntry))
					.Then(() => entityDescription);
		}


		sceneAsset.Entities.Add(entityEntry);
		return Result.Success(entityDescription);
	}


	public Result RemoveEntity(Guid entityId)
	{
		var backendScene = sceneState.LoadedBackendScene;
		var sceneAsset = backendScene.SceneAsset;

		var containingCollection =
			sceneAsset
				.Entities
				.FindCollectionWithEntity(entityId);

		if (containingCollection == null)
		{
			return Result.Error($"Unable to find entity with Id '{entityId}'");
		}

		var entity = containingCollection.First(x => x.Id == entityId);
		containingCollection.Remove(entity);

		return Result.Success();
	}


	public Result SetEntityName(Guid entityId, string newName) =>
		sceneState
			.LoadedBackendScene
			.SceneAsset
			.GetEntityById(entityId)
			.Then(x => x.Name = newName);


	public List<ComponentTypeDefinition> GetComponentTypes() =>
		projectDefinition
			.ComponentTypes
			.Select(x =>
				new ComponentTypeDefinition(
					ComponentAttribute.GetName(x),
					x.FullName!
				)
			)
			.ToList();


	public Result<ComponentDescription> AddComponent(
		Guid entityId,
		ComponentTypeDefinition componentTypeDefinition
	)
	{
		var entityEntryResult =
			sceneState
				.LoadedBackendScene
				.SceneAsset
				.GetEntityById(entityId);

		if (entityEntryResult.HasError)
		{
			return Result.Error(entityEntryResult.ErrorValue!);
		}

		var entityEntry = entityEntryResult.SuccessValue!;


		var componentType =
			projectDefinition
				.ComponentTypes
				.FirstOrDefault(x => x.FullName == componentTypeDefinition.FullTypeName);

		if (componentType == null)
		{
			return Result.Error($"Unable to find Component Type {componentTypeDefinition.FullTypeName}");
		}


		var newComponentRaw = Activator.CreateInstance(componentType);
		var newComponent = (EntityComponent)newComponentRaw!;

		entityEntry.Components.Add(newComponent);


		var componentDescription = sceneDescriptionMapper.Map(newComponent);
		return Result.Success(componentDescription);
	}


	public Result<UiElementDto> GetEditorForEntity(Guid entityId) =>
		customEditorListener.GetEditorForEntity(entityId);


	public Result UpdateEditorValue(Guid uiElementId, string? serializedNewValue) =>
		customEditorListener.UpdateEditorValue(uiElementId, serializedNewValue);
}
