namespace NGame.Ecs;

public interface IEntityTracker
{
	void AddEntity(Entity entity);
}



public class EntityTracker : IEntityTracker
{
	private readonly ISystemCollection _systemCollection;


	public EntityTracker(ISystemCollection systemCollection)
	{
		_systemCollection = systemCollection;
	}


	public void AddEntity(Entity entity)
	{
		var components = 
			entity
				.Components
				.Select(x=>x.GetType())
				.ToHashSet();

		foreach (var system in _systemCollection.GetSystems())
		{
			if(!components.IsSupersetOf(system.RequiredComponents)) continue;

			system.Add(entity);
		}
	}
}
