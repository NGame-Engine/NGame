using NGame.Components.Lines;
using NGame.Components.Sprites;
using NGame.Components.Texts;
using NGame.Components.Transforms;
using NGame.OsWindows;
using NGame.Renderers;
using SkiaSharp;

namespace NGamePlugin.Renderer2d.SkiaSharp;

public class SkiaSharpRenderer : INGameRenderer
{
	private readonly IOsWindow _window;
	private readonly SKBitmap _bitmap;
	private readonly SKCanvas _canvas;


	private readonly Dictionary<Texture, SKImage> _textures = new();
	private readonly Dictionary<Font, SKFont> _fonts = new();


	public SkiaSharpRenderer(
		IOsWindow window,
		GraphicsConfiguration graphicsConfiguration
	)
	{
		_window = window;

		var imageInfo = new SKImageInfo(
			width: graphicsConfiguration.Width,
			height: graphicsConfiguration.Height,
			colorType: SKColorType.Rgba8888,
			alphaType: SKAlphaType.Premul
		);

		_bitmap = new SKBitmap(imageInfo);
		_canvas = new SKCanvas(_bitmap);
		_canvas.Clear(SKColor.Parse("#003366"));
	}


	bool INGameRenderer.BeginDraw()
	{
		_canvas.Clear();
		return true;
	}


	public void Draw(Sprite sprite, Transform transform)
	{
		var texture = sprite.Texture;
		if (!_textures.ContainsKey(texture))
		{
			var filename = texture.FilePath;
			var image = SKImage.FromEncodedData(filename);
			_textures.Add(texture, image);
		}

		{
			var sourceRect = sprite.SourceRectangle.ToSkRect();

			var targetRectangle =
				SKRect.Create(
					sprite.TargetRectangle.X + transform.Position.X,
					sprite.TargetRectangle.Y + transform.Position.Y,
					sprite.TargetRectangle.Width,
					sprite.TargetRectangle.Height
				);

			var image = _textures[sprite.Texture];
			_canvas.DrawImage(image, sourceRect, targetRectangle);
		}
	}


	public void Draw(Line line)
	{
		var vertices = line.Vertices;
		for (int i = 0; i < vertices.Count - 1; i++)
		{
			var firstVertex = vertices[i];
			var secondVertex = vertices[i + 1];

			var linePaint = new SKPaint
			{
				StrokeWidth = line.Width,
				IsAntialias = true,
				Style = SKPaintStyle.Stroke
			};

			_canvas.DrawLine(firstVertex.X, firstVertex.Y, secondVertex.X, secondVertex.Y, linePaint);
		}
	}


	public void Draw(Text text, Transform transform)
	{
		var nGameFont = text.Font;
		if (!_fonts.ContainsKey(nGameFont))
		{
			var skTypeface = SKTypeface.FromFile(nGameFont.FilePath);
			var skFont = new SKFont(skTypeface, text.CharacterSize);
			_fonts.Add(nGameFont, skFont);
		}

		var font = _fonts[nGameFont];

		var skTextBlob = SKTextBlob.Create(text.Content, font);
		_canvas.DrawText(
			skTextBlob,
			transform.Position.X,
			transform.Position.Y,
			new SKPaint
			{
				Color = SKColor.Parse("F90")
			}
		);
	}


	void INGameRenderer.EndDraw(bool shouldPresent)
	{
		var bytes = _bitmap.Bytes;
		_window.Draw(bytes);
	}
}
