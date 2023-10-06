using Microsoft.Extensions.Logging;
using NGame.Services;
using NGame.Systems;

namespace NGame.GameLoop;



internal class DrawableCollection : IDrawableCollection
{
	private readonly ILogger<DrawableCollection> _logger;
	private readonly List<IDrawable> _drawables = new();


	public DrawableCollection(ILogger<DrawableCollection> logger)
	{
		_logger = logger;
	}


	void IDrawableCollection.Add(IDrawable drawable)
	{
		_drawables.Add(drawable);


		var newEntries =
			_drawables
				.Append(drawable)
				.OrderBy(x => x.Order);

		_drawables.Clear();

		foreach (var entry in newEntries)
		{
			_drawables.Add(entry);
		}


		_logger.LogInformation("Drawable {Drawable} added", drawable);
	}


	void IDrawableCollection.Draw(GameTime gameTime)
	{
		foreach (var drawable in _drawables)
		{
			drawable.Draw(gameTime);
		}
	}
}
