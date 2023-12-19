using NGameEditor.ViewModels.ProjectWindows.InspectorViews.Properties;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews;



public interface IInspectorComponentViewModelMapper
{
	InspectorComponentViewModel Map(ComponentState componentState);
}



public class InspectorComponentViewModelMapper : IInspectorComponentViewModelMapper
{
	public InspectorComponentViewModel Map(ComponentState componentState)
	{
		return new InspectorComponentViewModel(
			componentState,
			Map
		);
	}


	private PropertyViewModel Map(PropertyState propertyState)
	{
		return new PropertyViewModel(
			propertyState,
			GetEditorViewModel(propertyState)
		);
	}


	private EditorViewModel GetEditorViewModel(PropertyState propertyState)
	{
		var typeIdentifier = propertyState.TypeIdentifier;

		if (typeIdentifier == typeof(string).FullName)
		{
			return new StringEditorViewModel
			{
				Value = propertyState.Value
			};
		}

		return new StringEditorViewModel
		{
			Value = propertyState.Value
		};
	}
}
