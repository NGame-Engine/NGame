using NGame.Systems;

namespace NGame.Services;



public interface IUpdatableCollection
{
	void Add(IUpdatable updatable);
	void Update(GameTime gameTime);
}
