using NGameEditor.Bridge.Scenes;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;
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
			componentDescription.Name,
			componentDescription.IsRecognized,
			componentDescription
				.Properties
				.Select(MapProperty)
		);


	private static PropertyViewModel MapProperty(PropertyDescription propertyDescription) =>
		new(
			propertyDescription.Name,
			propertyDescription.SerializedValue
		);
}
