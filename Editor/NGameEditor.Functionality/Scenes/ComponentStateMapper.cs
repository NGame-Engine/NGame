using NGameEditor.Bridge.Scenes;
using NGameEditor.Functionality.Scenes.State;

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
			componentDescription.IsRecognized
		);



}
