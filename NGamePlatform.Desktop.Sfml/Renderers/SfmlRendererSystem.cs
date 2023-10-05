using Microsoft.Extensions.Options;
using NGame.Components.Renderer2Ds;
using NGame.Ecs;
using NGame.UpdateSchedulers;
using NGamePlatform.Desktop.Sfml.Assets;
using SFML.Graphics;

namespace NGamePlatform.Desktop.Sfml.Renderers;



public class SfmlRendererSystem : DataListSystem<SfmlDrawable>, IDrawable
{
	private readonly IAssetLoader _assetLoader;
	private readonly RenderWindow _renderWindow;
	private readonly SfmlDesktopConfiguration _sfmlDesktopConfiguration;


	public SfmlRendererSystem(
		IAssetLoader assetLoader,
		RenderWindow renderWindow,
		IOptions<SfmlDesktopConfiguration> configuration
	)
	{
		_assetLoader = assetLoader;
		_renderWindow = renderWindow;
		_sfmlDesktopConfiguration = configuration.Value;
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
			if (spriteRenderer.Sprite != null)
			{
				_assetLoader.LoadTexture(spriteRenderer.Sprite.Texture);
			}

			return new DrawableSprite(transform, spriteRenderer, _assetLoader, _sfmlDesktopConfiguration);
		}


		var textRenderer = entity.GetComponent<TextRenderer>();
		if (textRenderer != null)
		{
			if (textRenderer.Text != null)
			{
				_assetLoader.LoadFont(textRenderer.Text.Font);
			}

			return new DrawableText(transform, textRenderer, _assetLoader, _sfmlDesktopConfiguration);
		}


		var lineRenderer = entity.GetComponent<LineRenderer>();
		if (lineRenderer != null)
		{
			return new DrawableLine(transform, lineRenderer, _sfmlDesktopConfiguration);
		}

		throw new InvalidOperationException(
			$"Renderer {entity.GetComponent<Renderer2D>()} not supported"
		);
	}


	void IDrawable.Draw(GameTime gameTime)
	{
		var orderedEntries = DataEntries.OrderBy(x => -x.Transform.Position.Z);
		foreach (var drawable in orderedEntries)
		{
			drawable.Draw(_renderWindow);
		}
	}
}
