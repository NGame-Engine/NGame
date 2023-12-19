namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews;



public class InspectorViewModel(
	InspectorEntityViewModel entity
) : ViewModelBase
{
	public InspectorEntityViewModel Entity
	{
		get => entity;
		set => this.RaiseAndSetIfChanged(ref entity, value);
	}
}
