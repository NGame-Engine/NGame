using NGameEditor.Bridge.Scenes;
using NGameEditor.Functionality.Scenes.State;

namespace NGameEditor.Functionality.Scenes;



public interface IEntityStateMapper
{
	EntityState Map(EntityDescription entityDescription);
}



public class EntityStateMapper(
	IComponentStateMapper componentStateMapper
) : IEntityStateMapper
{
	public EntityState Map(EntityDescription entityDescription) =>
		new(
			entityDescription.Id,
			entityDescription.Name,
			entityDescription.Components.Select(componentStateMapper.MapComponent),
			entityDescription.Children.Select(Map)
		);
}
