namespace NGame.Ecs;

public interface IEntityTracker
{
	void AddEntity(Entity entity);
}



internal class EntityTracker : IEntityTracker
{
	private readonly ISystemCollection _systemCollection;


	public EntityTracker(ISystemCollection systemCollection)
	{
		_systemCollection = systemCollection;
	}


	public void AddEntity(Entity entity)
	{
		ISet<Type> componentTypes =
			entity
				.Components
				.Select(x => x.GetType())
				.ToHashSet();

		foreach (var system in _systemCollection.GetSystems())
		{
			system.Add(entity, componentTypes);
		}
	}
}
