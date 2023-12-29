using NGame.SceneAssets;
using NGameEditor.Results;

namespace NGameEditor.Backend.Scenes.SceneStates;



public static class SceneAssetExtensions
{
	public static Result<EntityEntry> GetEntityById(
		this SceneAsset sceneAsset,
		Guid id
	)
	{
		var entityEntry =
			sceneAsset
				.Entities
				.SelectMany(x => x.EnumerateRecursive())
				.FirstOrDefault(x => x.Id == id);

		return
			entityEntry != null
				? Result.Success(entityEntry)
				: Result.Error($"Entity with ID '{id}' not found");
	}


	public static ICollection<EntityEntry>? FindCollectionWithEntity(
		this ICollection<EntityEntry> entityEntries,
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


	public static IEnumerable<EntityEntry> EnumerateRecursive(
		this EntityEntry entityEntry
	)
	{
		yield return entityEntry;

		var descendants =
			entityEntry
				.Children
				.SelectMany(child => child.EnumerateRecursive());

		foreach (var descendant in descendants)
		{
			yield return descendant;
		}
	}
}
