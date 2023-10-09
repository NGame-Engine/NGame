using Microsoft.Extensions.Logging;
using NGame.Services;
using NGame.Systems;

namespace NGame.GameLoop;



public class DrawableCollection : IDrawableCollection
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
		_drawables.Sort((a, b) => a.Order.CompareTo(b.Order));

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
