using NGame.Ecs;
using NGame.SceneAssets;
using NGameEditor.Backend.Scenes.Properties;
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
	private readonly Dictionary<string, IComponentMapper> _componentMappers;


	public SceneDescriptionMapper(IEnumerable<IComponentMapper> componentMappers)
	{
		_componentMappers = componentMappers
			.ToDictionary(x => x.TypeIdentifier);
	}


	public SceneDescription Map(BackendScene backendScene) =>
		new(
			backendScene.FilePath == null
				? null
				: Path.GetFileName(backendScene.FilePath.Path),
			backendScene.SceneAsset.Id.Id,
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
			return new(
				entityComponent.Id,
				"Unrecognized component",
				false,
				new List<PropertyDescription>()
			);
		}

		var componentDescription = new ComponentDescription(
			entityComponent.Id,
			ComponentAttribute.GetName(componentType),
			true,
			new List<PropertyDescription>()
		);

		var readableProperties =
			componentType
				.GetProperties()
				.Where(x => x.CanRead);

		foreach (var readableProperty in readableProperties)
		{
			var type = readableProperty.PropertyType;
			var typeName = type.FullName!;
			if (!_componentMappers.TryGetValue(typeName, out var mapper)) continue;

			var propertyDescription = mapper.Map(readableProperty, entityComponent);
			componentDescription.Properties.Add(propertyDescription);
		}


		return componentDescription;
	}
}
