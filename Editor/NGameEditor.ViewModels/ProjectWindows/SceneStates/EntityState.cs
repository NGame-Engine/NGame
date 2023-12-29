using System.Collections.ObjectModel;

namespace NGameEditor.ViewModels.ProjectWindows.SceneStates;



public class EntityState(
	Guid id,
	string name,
	IEnumerable<ComponentState> components,
	IEnumerable<EntityState> children
) : ViewModelBase
{
	public Guid Id { get; } = id;

	public string Name { get; } = name;


	public ObservableCollection<ComponentState> Components { get; } = new(components);
	public ObservableCollection<EntityState> Children { get; set; } = new(children);
}
