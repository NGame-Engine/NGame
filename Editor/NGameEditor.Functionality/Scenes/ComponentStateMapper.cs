using NGameEditor.Bridge.Scenes;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.Functionality.Scenes;



public interface IComponentStateMapper
{
	ComponentState MapComponent(ComponentDescription componentDescription);
}



public class ComponentStateMapper : IComponentStateMapper
{
	public ComponentState MapComponent(ComponentDescription componentDescription) =>
		new(
			componentDescription.Id,
			componentDescription.Name,
			componentDescription.IsRecognized,
			componentDescription
				.Properties
				.Select(MapProperty)
		);


	private static PropertyState MapProperty(PropertyDescription propertyDescription) =>
		new(
			propertyDescription.Name,
			propertyDescription.TypeIdentifier,
			propertyDescription.SerializedValue
		);
}
