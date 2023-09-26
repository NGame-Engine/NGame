using NGame.UpdaterSchedulers;

namespace NGame.Ecs;

public interface ISystem
{
	IEnumerable<Type> RequiredComponents { get; }
	Task Update(GameTime gameTime, CancellationToken cancellationToken);
	void Add(Entity entity);
}
