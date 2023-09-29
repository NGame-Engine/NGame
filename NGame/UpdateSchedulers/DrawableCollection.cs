using Microsoft.Extensions.Logging;

namespace NGame.UpdateSchedulers;

public interface IDrawableCollection
{
	void Add(IDrawable drawable);
	void Draw(GameTime gameTime);
}



public class DrawableCollection : IDrawableCollection
{
	private readonly ILogger<DrawableCollection> _logger;
	private readonly List<IDrawable> _drawableSystems = new();


	public DrawableCollection(ILogger<DrawableCollection> logger)
	{
		_logger = logger;
	}


	public void Add(IDrawable drawable)
	{
		_drawableSystems.Add(drawable);
	}


	public void Draw(GameTime gameTime)
	{
		foreach (var drawable in _drawableSystems)
		{
			drawable.Draw(gameTime);
		}
	}
}
