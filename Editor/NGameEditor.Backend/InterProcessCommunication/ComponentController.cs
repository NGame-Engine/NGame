using NGame.Ecs;
using NGameEditor.Backend.Projects;
using NGameEditor.Backend.Scenes;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Bridge.Scenes;
using NGameEditor.Results;

namespace NGameEditor.Backend.InterProcessCommunication;



public interface IComponentController
{
	Result<ComponentDescription> AddComponent(
		Guid entityId,
		ComponentTypeDefinition componentTypeDefinition
	);
}



public class ComponentController(
	ProjectDefinition projectDefinition,
	ISceneState sceneState,
	ISceneDescriptionMapper sceneDescriptionMapper
) : IComponentController
{
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
}
