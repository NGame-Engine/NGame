namespace NGame.Implementations.UpdateLoop;



public interface IRenderContext
{
	/// <summary>
	/// Try to initialize rendering for a frame.
	/// </summary>
	/// <returns>true if successful and rendering can proceed.</returns>
	bool BeginDraw();


	/// <summary>
	/// Finish drawing a frame.
	/// </summary>
	/// <param name="shouldPresent"></param>
	void EndDraw(bool shouldPresent);
}
