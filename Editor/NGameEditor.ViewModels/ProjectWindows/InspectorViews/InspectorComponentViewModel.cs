using DynamicData;
using DynamicData.Binding;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews;



public class InspectorComponentViewModel : ViewModelBase
{
	public InspectorComponentViewModel(
		ComponentState componentState,
		Func<PropertyState, PropertyViewModel> mapPropertyState
	)
	{
		ComponentState = componentState;
		Name = componentState.Name;
		IsRecognized = componentState.IsRecognized;

		ComponentState
			.Properties
			.ToObservableChangeSet()
			.Transform(mapPropertyState)
			.Bind(Properties)
			.Subscribe();
	}


	public ComponentState ComponentState { get; }
	public string Name { get; }
	public bool IsRecognized { get; }
	public ObservableCollectionExtended<PropertyViewModel> Properties { get; } = new();
}
