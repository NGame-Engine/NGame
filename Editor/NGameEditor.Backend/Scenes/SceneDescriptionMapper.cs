using NGame.Assets.Common.Ecs;
using NGame.Ecs;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Backend.Scenes;



public interface ISceneDescriptionMapper
{
	SceneDescription Map(BackendScene backendScene);
	EntityDescription Map(EntityEntry entity);
	ComponentDescription Map(EntityComponent entityComponent);
}



public class SceneDescriptionMapper : ISceneDescriptionMapper
{
	public SceneDescription Map(BackendScene backendScene) =>
		new(
			backendScene.FilePath == null
				? null
				: Path.GetFileName(backendScene.FilePath.Path),
			backendScene.SceneAsset.Id,
			backendScene
				.SceneAsset
				.Entities
				.Select(Map)
				.ToList()
		);


	public EntityDescription Map(EntityEntry entity) =>
		new(
			entity.Id,
			entity.Name,
			entity.Components.Select(Map).ToList(),
			entity.Children.Select(Map).ToList()
		);


	public ComponentDescription Map(EntityComponent entityComponent)
	{
		var componentType = entityComponent.GetType();
		if (componentType == typeof(EntityComponent))
		{
			return new ComponentDescription(
				entityComponent.Id,
				"Unrecognized component",
				false
			);
		}

		var componentDescription = new ComponentDescription(
			entityComponent.Id,
			ComponentAttribute.GetName(componentType),
			true
		);

		return componentDescription;
	}
}
