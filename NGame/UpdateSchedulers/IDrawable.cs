namespace NGame.UpdateSchedulers;

/// <summary>
/// Something that needs to be executed at drawing time.
/// </summary>
public interface IDrawable
{
	void Draw(GameTime gameTime);
}
