using SFML.Graphics;
using NgTransform = NGame.Components.Transforms.Transform;
using NGameFont = NGame.Components.Renderer2Ds.Font;
using NGameText = NGame.Components.Renderer2Ds.Text;

namespace NGamePlatform.Desktop.Sfml.Renderers;



public abstract class SfmlDrawable
{
	public readonly NgTransform Transform;


	protected SfmlDrawable(NgTransform transform)
	{
		Transform = transform;
	}


	public abstract void Draw(RenderWindow renderWindow);
}
