namespace NGame.UpdateSchedulers;



/// <summary>
/// Something that needs to be executed every frame before drawing time.
/// </summary>
public interface IUpdatable
{
	/// <summary>
	/// The <see cref="Update"/> method of different <see cref="IUpdatable"/>s will be called
	/// in order of this number (lower values will be called earlier).
	/// This should be set before adding this to an <see cref="IUpdatableCollection"/>.
	/// </summary>
	int Order { get; set; }

	void Update(GameTime gameTime);
}
