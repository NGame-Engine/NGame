namespace NGame.Platform.UpdateLoop;



public class NoOpRenderContext : IRenderContext
{
	public bool BeginDraw()
	{
		return true;
	}


	public void EndDraw(bool shouldPresent)
	{
	}
}
