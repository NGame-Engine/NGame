using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Functionality.Projects;



public record SelectedEntitiesChangedEventArgs(ICollection<EntityDescription> SelectedEntities);



public interface IProjectState
{
	event Action<SelectedEntitiesChangedEventArgs>? SelectedEntitiesChanged;

	void SetSelectedEntities(IEnumerable<EntityDescription> entityDescriptions);
}



public class ProjectState : IProjectState
{
	private readonly List<EntityDescription> _selectedEntities = new();


	public event Action<SelectedEntitiesChangedEventArgs>? SelectedEntitiesChanged;


	public void SetSelectedEntities(IEnumerable<EntityDescription> entityDescriptions)
	{
		_selectedEntities.Clear();
		_selectedEntities.AddRange(entityDescriptions);
		var eventArgs = new SelectedEntitiesChangedEventArgs(_selectedEntities);
		SelectedEntitiesChanged?.Invoke(eventArgs);
	}
}
