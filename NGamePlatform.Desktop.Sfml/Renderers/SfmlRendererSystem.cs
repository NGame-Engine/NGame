using NGame.Components.Renderer2Ds;
using NGame.Ecs;
using NGame.UpdateSchedulers;
using SFML.Graphics;

namespace NGamePlatform.Desktop.Sfml.Renderers;



public class SfmlRendererSystem : DataListSystem<SfmlDrawable>, IDrawable
{
	private readonly AssetLoader _assetLoader;
	private readonly RenderWindow _renderWindow;


	public SfmlRendererSystem(AssetLoader assetLoader, RenderWindow renderWindow)
	{
		_assetLoader = assetLoader;
		_renderWindow = renderWindow;
	}


	public int Order { get; set; } = 60000;

	protected override ICollection<Type> RequiredComponents =>
		new[] { typeof(Renderer2D) };


	protected override SfmlDrawable CreateDataFromEntity(Entity entity)
	{
		var transform = entity.Transform;

		var spriteRenderer = entity.GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
		{
			return new DrawableSprite(transform, spriteRenderer, _assetLoader);
		}

		var textRenderer = entity.GetComponent<TextRenderer>();
		if (textRenderer != null)
		{
			return new DrawableText(transform, textRenderer, _assetLoader);
		}

		var lineRenderer = entity.GetComponent<LineRenderer>();
		if (lineRenderer != null)
		{
			return new DrawableLine(transform, lineRenderer);
		}

		throw new InvalidOperationException(
			$"Renderer {entity.GetComponent<Renderer2D>()} not supported"
		);
	}


	void IDrawable.Draw(GameTime gameTime)
	{
		foreach (var drawable in DataEntries.OrderBy(x => x.Transform.Position.Z))
		{
			drawable.Draw(_renderWindow);
		}
	}
}
