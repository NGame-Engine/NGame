namespace NGame.Ecs;

public interface ISystem
{
	ICollection<Type> RequiredComponents { get; }
	void Add(Entity entity, ISet<Type> componentTypes);
}
