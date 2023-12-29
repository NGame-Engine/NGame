using System.Collections.ObjectModel;

namespace NGameEditor.ViewModels.ProjectWindows.SceneStates;



public class ComponentState(
	Guid id,
	string name,
	bool isRecognized,
	IEnumerable<PropertyState> properties
) : ViewModelBase
{
	public Guid Id => id;
	public string Name => name;
	public bool IsRecognized { get; } = isRecognized;
	public ObservableCollection<PropertyState> Properties { get; } = new(properties);
}
