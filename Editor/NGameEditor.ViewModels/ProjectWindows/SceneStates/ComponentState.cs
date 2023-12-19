using System.Collections.ObjectModel;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;

namespace NGameEditor.ViewModels.ProjectWindows.SceneStates;



public class ComponentState(
	string name,
	bool isRecognized,
	IEnumerable<PropertyViewModel> properties
) : ViewModelBase
{
	public string Name => name;
	public bool IsRecognized { get; } = isRecognized;
	public ObservableCollection<PropertyViewModel> Properties { get; } = new(properties);
}
