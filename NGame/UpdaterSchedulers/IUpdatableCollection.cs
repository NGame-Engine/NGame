namespace NGame.UpdaterSchedulers;

public interface IUpdatableCollection
{
	void Initialize();
	Task Update(GameTime gameTime);
}
