using NGame.UpdateSchedulers;

namespace NGame.Ecs;

public interface ISystem
{
	IEnumerable<Type> RequiredComponents { get; }
	void Add(Entity entity);
	void Update(GameTime gameTime);
}
