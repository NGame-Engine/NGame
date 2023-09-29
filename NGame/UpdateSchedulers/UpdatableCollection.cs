using Microsoft.Extensions.Logging;

namespace NGame.UpdateSchedulers;

public interface IUpdatableCollection
{
	void Add(IUpdatable updatable);
	void Update(GameTime gameTime);
}



public class UpdatableCollection : IUpdatableCollection
{
	private readonly ILogger<UpdatableCollection> _logger;
	private readonly List<IUpdatable> _updatableSystems = new();


	public UpdatableCollection(ILogger<UpdatableCollection> logger)
	{
		_logger = logger;
	}


	public void Add(IUpdatable updatable)
	{
		_updatableSystems.Add(updatable);
	}


	public void Update(GameTime gameTime)
	{
		foreach (var updatableSystem in _updatableSystems)
		{
			updatableSystem.Update(gameTime);
		}
	}
}
