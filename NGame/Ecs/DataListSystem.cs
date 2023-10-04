namespace NGame.Ecs;



public abstract class DataListSystem<TData> : ISystem
{
	private readonly Dictionary<Entity, TData> _datas = new();


	public virtual bool EntityIsMatch(IEnumerable<Type> componentTypes)
	{
		if (!RequiredComponents.Any()) return true;


		var checkRequiredTypes = RequiredComponents.ToList();

		foreach (var componentType in componentTypes)
		{
			for (var j = checkRequiredTypes.Count - 1; j >= 0; j--)
			{
				if (!checkRequiredTypes[j].IsAssignableFrom(componentType))
				{
					continue;
				}

				checkRequiredTypes.RemoveAt(j);

				if (checkRequiredTypes.Count == 0) return true;
			}
		}

		return false;
	}


	public virtual void Add(Entity entity)
	{
		var data = CreateDataFromEntity(entity);
		_datas.Add(entity, data);
	}


	public virtual void Remove(Entity entity)
	{
		_datas.Remove(entity);
	}


	public virtual bool Contains(Entity entity) => _datas.ContainsKey(entity);


	protected abstract ICollection<Type> RequiredComponents { get; }

	protected IReadOnlyCollection<TData> DataEntries => _datas.Values;

	protected abstract TData CreateDataFromEntity(Entity entity);
}
