using NGame.Ecs;

namespace NGame.Services;



public interface IUpdatableCollection
{
	void Add(IUpdatable updatable);
	void Update(GameTime gameTime);
}
