namespace NGame.UpdateSchedulers;



/// <summary>
/// Something that needs to be executed every frame at drawing time.
/// </summary>
public interface IDrawable
{
	/// <summary>
	/// The <see cref="Draw"/> method of different <see cref="IDrawable"/>s will be called
	/// in order of this number (lower values will be called earlier).
	/// This should be set before adding this to an <see cref="IDrawableCollection"/>.
	/// </summary>
	int Order { get; set; }

	void Draw(GameTime gameTime);
}
