using Microsoft.Extensions.Logging;
using NGame.Services;
using NGame.Systems;

namespace NGame.GameLoop;



internal class UpdatableCollection : IUpdatableCollection
{
	private readonly ILogger<UpdatableCollection> _logger;
	private readonly List<IUpdatable> _updatables = new();


	public UpdatableCollection(ILogger<UpdatableCollection> logger)
	{
		_logger = logger;
	}


	void IUpdatableCollection.Add(IUpdatable updatable)
	{
		_updatables.Add(updatable);
		_updatables.Sort((a, b) => a.Order.CompareTo(b.Order));

		_logger.LogInformation("Updatable {Updatable} added", updatable);
	}


	void IUpdatableCollection.Update(GameTime gameTime)
	{
		foreach (var updatableSystem in _updatables)
		{
			updatableSystem.Update(gameTime);
		}
	}
}
