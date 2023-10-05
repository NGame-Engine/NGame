using NGame.Components.Renderer2Ds;
using SFML.Graphics;
using Text = SFML.Graphics.Text;
using NGameFont = NGame.Components.Renderer2Ds.Font;
using Transform = NGame.Components.Transforms.Transform;

namespace NGamePlatform.Desktop.Sfml.Renderers;



public class DrawableText : SfmlDrawable
{
	private readonly TextRenderer _textRenderer;
	private readonly AssetLoader _assetLoader;
	private readonly GraphicsSettings2D _graphicsSettings2D;
	private readonly Text _text = new();


	public DrawableText(
		Transform transform,
		TextRenderer textRenderer,
		AssetLoader assetLoader,
		GraphicsSettings2D graphicsSettings2D
	) : base(transform)
	{
		_textRenderer = textRenderer;
		_assetLoader = assetLoader;
		_graphicsSettings2D = graphicsSettings2D;
		_currentFont = null!;
	}


	private NGameFont _currentFont;


	public override void Draw(RenderWindow renderWindow)
	{
		var nGameText = _textRenderer.Text;
		if (nGameText == null) return;

		var nGameFont = nGameText.Font;
		if (nGameFont != _currentFont)
		{
			_text.Font = _assetLoader.LoadFont(nGameFont);
			_currentFont = nGameFont;
		}


		_text.Position =
			Transform.Position.ToSfmlVector2YInverted() *
			_graphicsSettings2D.PixelPerUnit;

		_text.DisplayedString = nGameText.Content;
		_text.CharacterSize = (uint)nGameText.CharacterSize;
		_text.Origin = nGameText.TransformOrigin.ToSfmlVector2();
		_text.FillColor = nGameText.Color.ToSfmlColor();


		renderWindow.Draw(_text);
	}
}
