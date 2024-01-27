using System.Collections.ObjectModel;
using NGameEditor.ViewModels;

namespace NGameEditor.Functionality.Scenes.State;



public class SceneState : ViewModelBase
{
	public ObservableCollection<EntityState> SceneEntities { get; } = [];


	public void RemoveAllEntities() => SceneEntities.Clear();
}



public static class SceneStateExtensions
{
	public static ICollection<EntityState>? FindCollectionWithEntity(
		this ICollection<EntityState> entityEntries,
		Guid id
	)
	{
		var containsId = entityEntries.Any(x => x.Id == id);
		if (containsId) return entityEntries;

		return
			entityEntries
				.Select(x => x.Children.FindCollectionWithEntity(id))
				.FirstOrDefault(x => x != null);
	}
}
