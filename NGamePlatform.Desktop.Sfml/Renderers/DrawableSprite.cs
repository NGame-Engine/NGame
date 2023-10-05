using NGame.Components.Renderer2Ds;
using SFML.Graphics;
using SFML.System;
using NGameSprite = NGame.Components.Renderer2Ds.Sprite;
using NGameTexture = NGame.Components.Renderer2Ds.Texture;
using NGameTransform = NGame.Components.Transforms.Transform;
using NGameFont = NGame.Components.Renderer2Ds.Font;
using NGameText = NGame.Components.Renderer2Ds.Text;
using Sprite = SFML.Graphics.Sprite;

namespace NGamePlatform.Desktop.Sfml.Renderers;



public class DrawableSprite : SfmlDrawable
{
	private readonly SpriteRenderer _spriteRenderer;
	private readonly AssetLoader _assetLoader;
	private readonly GraphicsSettings2D _graphicsSettings2D;
	private readonly Sprite _sprite = new();


	public DrawableSprite(
		NGameTransform transform,
		SpriteRenderer spriteRenderer,
		AssetLoader assetLoader,
		GraphicsSettings2D graphicsSettings2D
	) : base(transform)
	{
		_spriteRenderer = spriteRenderer;
		_assetLoader = assetLoader;
		_graphicsSettings2D = graphicsSettings2D;
		_currentTexture = null!;
	}


	private NGameTexture _currentTexture;


	public override void Draw(RenderWindow renderWindow)
	{
		var ngSprite = _spriteRenderer.Sprite;
		if (ngSprite == null) return;

		var ngTexture = ngSprite.Texture;
		if (ngTexture != _currentTexture)
		{
			_sprite.Texture = _assetLoader.LoadTexture(ngTexture);
			var textureSize = _sprite.Texture.Size;
			_sprite.Origin = new Vector2f(textureSize.X, textureSize.Y) / 2;
			_currentTexture = ngTexture;
		}


		_sprite.TextureRect = new IntRect(
			ngSprite.SourceRectangle.X,
			ngSprite.SourceRectangle.Y,
			ngSprite.SourceRectangle.Width,
			ngSprite.SourceRectangle.Height
		);

		_sprite.Position =
			Transform.Position.ToSfmlVector2YInverted() *
			_graphicsSettings2D.PixelPerUnit;

		_sprite.Rotation = -Transform.EulerAngles.Z;


		renderWindow.Draw(_sprite);
	}
}
