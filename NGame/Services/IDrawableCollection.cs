using NGame.Ecs;

namespace NGame.Services;



public interface IDrawableCollection
{
	void Add(IDrawable drawable);
	void Draw(GameTime gameTime);
}
