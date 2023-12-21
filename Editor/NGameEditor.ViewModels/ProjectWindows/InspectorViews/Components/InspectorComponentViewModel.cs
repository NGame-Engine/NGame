using DynamicData;
using DynamicData.Binding;
using NGameEditor.ViewModels.Components.CustomEditors;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews.Properties;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews.Components;



public class InspectorComponentViewModel : ViewModelBase
{
	public InspectorComponentViewModel(
		ComponentState componentState,
		Func<PropertyState, PropertyViewModel> mapPropertyState,
		ComponentEditorViewModel componentEditorViewModel
	)
	{
		ComponentState = componentState;
		ComponentEditorViewModel = componentEditorViewModel;

		ComponentState
			.Properties
			.ToObservableChangeSet()
			.Transform(mapPropertyState)
			.Bind(Properties)
			.Subscribe();
	}


	public ComponentState ComponentState { get; }
	public ComponentEditorViewModel ComponentEditorViewModel { get; }

	public ObservableCollectionExtended<PropertyViewModel> Properties { get; } = new();
}
