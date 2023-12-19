using System.Collections.ObjectModel;

namespace NGameEditor.ViewModels.ProjectWindows.SceneStates;



public class ComponentState(
	string name,
	bool isRecognized,
	IEnumerable<PropertyState> properties
) : ViewModelBase
{
	public string Name => name;
	public bool IsRecognized { get; } = isRecognized;
	public ObservableCollection<PropertyState> Properties { get; } = new(properties);
}
