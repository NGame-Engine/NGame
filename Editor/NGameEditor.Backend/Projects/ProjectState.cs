using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Backend.Projects;



public record SelectedEntitiesChangedEventArgs(ICollection<EntityDescription> SelectedEntities);



public interface IProjectState
{
	event Action<SelectedEntitiesChangedEventArgs> SelectedEntitiesChanged;
	IReadOnlyCollection<EntityDescription> SelectedEntities { get; }

	void SetSelectedEntities(IEnumerable<EntityDescription> entityDescriptions);
}



public class ProjectState : IProjectState
{
	private readonly List<EntityDescription> _selectedEntities = new();


	public event Action<SelectedEntitiesChangedEventArgs>? SelectedEntitiesChanged;


	public IReadOnlyCollection<EntityDescription> SelectedEntities => _selectedEntities;


	public void SetSelectedEntities(IEnumerable<EntityDescription> entityDescriptions)
	{
		_selectedEntities.Clear();
		_selectedEntities.AddRange(entityDescriptions);
		var eventArgs = new SelectedEntitiesChangedEventArgs(_selectedEntities);
		SelectedEntitiesChanged?.Invoke(eventArgs);
	}
}
