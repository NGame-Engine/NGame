using NGame.Components.Renderer2Ds;
using SFML.Graphics;
using NGameSprite = NGame.Components.Renderer2Ds.Sprite;
using NGameTexture = NGame.Components.Renderer2Ds.Texture;
using NGameTransform = NGame.Components.Transforms.Transform;
using NGameFont = NGame.Components.Renderer2Ds.Font;
using NGameText = NGame.Components.Renderer2Ds.Text;
using Sprite = SFML.Graphics.Sprite;
using Texture = SFML.Graphics.Texture;

namespace NGamePlatform.Desktop.Sfml.Renderers;



public class DrawableSprite : SfmlDrawable
{
	private readonly SpriteRenderer _spriteRenderer;
	private readonly AssetLoader _assetLoader;


	public DrawableSprite(
		NGameTransform transform,
		SpriteRenderer spriteRenderer,
		AssetLoader assetLoader
	) : base(transform)
	{
		_spriteRenderer = spriteRenderer;
		_assetLoader = assetLoader;
	}


	public override void Draw(RenderWindow renderWindow)
	{
		var sprite = _spriteRenderer.Sprite;
		if (sprite == null) return;


		var texture = sprite.Texture;
		if (!_assetLoader.Textures.ContainsKey(texture))
		{
			var filename = texture.FilePath;
			var image = new Texture(filename);
			_assetLoader.Textures.Add(texture, image);
		}

		var spriteTexture = _assetLoader.Textures[sprite.Texture];
		var sp = new Sprite(spriteTexture);

		sp.TextureRect = new IntRect(
			sprite.SourceRectangle.X,
			sprite.SourceRectangle.Y,
			sprite.SourceRectangle.Width,
			sprite.SourceRectangle.Height
		);

		sp.Position = (Transform.Position).ToSfmlVector2YInverted();


		renderWindow.Draw(sp);
	}
}
