namespace NGame.UpdateSchedulers;



public interface IDrawableCollection
{
	void Add(IDrawable drawable);
	void Draw(GameTime gameTime);
}



internal class DrawableCollection : IDrawableCollection
{
	private readonly List<IDrawable> _drawableSystems = new();


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
