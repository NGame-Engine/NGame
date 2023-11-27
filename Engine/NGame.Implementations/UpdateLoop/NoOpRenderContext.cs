using NGame.UpdateLoop;

namespace NGame.Core.UpdateLoop;



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
