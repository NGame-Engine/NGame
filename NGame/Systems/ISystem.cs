namespace NGame.Systems;



public interface ISystem
{
	bool EntityIsMatch(IEnumerable<Type> componentTypes);
	void Add(Entity entity);
	void Remove(Entity entity);
	bool Contains(Entity entity);
}
