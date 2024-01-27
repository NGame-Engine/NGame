namespace NGame.UpdateLoop;



/// <summary>
/// Something that needs to be executed every frame at drawing time.
/// </summary>
public interface IDrawable
{
	/// <summary>
	/// The <see cref="Draw"/> method of different <see cref="IDrawable"/>s will be called
	/// in order of this number (lower values will be called earlier).
	/// </summary>
	int Order { get; set; }

	// ReSharper disable once UnusedParameter.Global
	void Draw(GameTime gameTime);
}
