using NGame.Components.Renderer2Ds;
using SFML.Graphics;
using Font = SFML.Graphics.Font;
using Text = SFML.Graphics.Text;
using Transform = NGame.Components.Transforms.Transform;

namespace NGamePlatform.Desktop.Sfml.Renderers;



public class DrawableText : SfmlDrawable
{
	private readonly TextRenderer _textRenderer;
	private readonly AssetLoader _assetLoader;


	public DrawableText(
		Transform transform,
		TextRenderer textRenderer,
		AssetLoader assetLoader
	) : base(transform)
	{
		_textRenderer = textRenderer;
		_assetLoader = assetLoader;
	}


	public override void Draw(RenderWindow renderWindow)
	{
		var nGameText = _textRenderer.Text;
		if (nGameText == null) return;

		var nGameFont = nGameText.Font;
		if (!_assetLoader.Fonts.ContainsKey(nGameFont))
		{
			_assetLoader.Fonts.Add(nGameFont, new Font(nGameFont.FilePath));
		}

		var font = _assetLoader.Fonts[nGameFont];
		var text = new Text(nGameText.Content, font);
		text.CharacterSize = (uint)nGameText.CharacterSize;
		text.Origin = nGameText.TransformOrigin.ToSfmlVector2();
		text.Position = Transform.Position.ToSfmlVector2YInverted();
		text.FillColor = nGameText.Color.ToSfmlColor();

		renderWindow.Draw(text);
	}
}
