using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews.Properties;



public class PropertyViewModel : ViewModelBase
	{
	public PropertyViewModel(
		PropertyState propertyState,
		EditorViewModel editorViewModel
	)
	{
		_name = propertyState.Name;
		EditorViewModel = editorViewModel;

		propertyState.WhenAnyValue(x => x.Name)
			.BindTo(this, x => x.Name);
	}


	private string _name;

	public string Name
	{
		get => _name;
		set => this.RaiseAndSetIfChanged(ref _name, value);
	}


	public EditorViewModel EditorViewModel { get; }
}
