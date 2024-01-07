using NGame.SceneAssets;
using NGameEditor.Backend.Scenes;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Bridge.Scenes;
using NGameEditor.Results;

namespace NGameEditor.Backend.InterProcessCommunication;



public interface IEntityController
{
	Result<EntityDescription> AddEntity(Guid? parentEntityId);
	Result RemoveEntity(Guid entityId);
	Result SetEntityName(Guid entityId, string newName);
}



public class EntityController(
	ISceneState sceneState,
	ISceneDescriptionMapper sceneDescriptionMapper
) : IEntityController
{
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
}
