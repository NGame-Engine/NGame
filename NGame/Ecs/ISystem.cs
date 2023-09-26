namespace NGame.Ecs;

public interface ISystem
{
	IEnumerable<Type> RequiredComponents { get; }
	Task Update(CancellationToken cancellationToken);
	void Add(Entity entity);
}
