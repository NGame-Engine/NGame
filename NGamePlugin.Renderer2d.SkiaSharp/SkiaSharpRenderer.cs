using Microsoft.Extensions.Logging;
using NGame.Maths;
using NGame.OsWindows;
using NGame.Renderers;
using NGame.Sprites;
using NGame.Texts;
using NGame.Transforms;
using SkiaSharp;

namespace NGamePlugin.Renderer2d.SkiaSharp;

public class SkiaSharpRenderer : INGameRenderer
{
	private readonly ILogger<SkiaSharpRenderer> _logger;
	private readonly IOsWindow _window;
	private readonly GraphicsConfiguration _graphicsConfiguration;

	private readonly Dictionary<Texture, SKImage> _textures = new();
	private readonly Dictionary<Font, SKFont> _fonts = new();


	public SkiaSharpRenderer(
		ILogger<SkiaSharpRenderer> logger,
		IOsWindow window,
		GraphicsConfiguration graphicsConfiguration
	)
	{
		_logger = logger;
		_window = window;
		_graphicsConfiguration = graphicsConfiguration;
	}


	private SKImageInfo ImageInfo { get; set; }
	private SKBitmap? Bitmap { get; set; }
	private SKCanvas? Canvas { get; set; }


	void INGameRenderer.Initialize()
	{
		_logger.LogDebug("Initialize");


		var width = _graphicsConfiguration.Width;
		var height = _graphicsConfiguration.Height;

		ImageInfo = new SKImageInfo(
			width: (int)width,
			height: (int)height,
			colorType: SKColorType.Rgba8888,
			alphaType: SKAlphaType.Premul
		);

		Bitmap = new SKBitmap(ImageInfo);
		Canvas = new SKCanvas(Bitmap);
		Canvas.Clear(SKColor.Parse("#003366"));
	}


	bool INGameRenderer.BeginDraw()
	{
		Canvas!.Clear();
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
			Canvas.DrawImage(image, sourceRect, targetRectangle);
		}
	}


	public void Draw(Line line)
	{
		var vertices = line.Vertices;
		for (int i = 0; i < vertices.Count - 1; i++)
		{
			var firstVertex = vertices[i];
			var secondVertex = vertices[i + 1];

			float lineWidth = Random.Shared.Between(1, 10);
			var lineColor = new SKColor(
				red: Random.Shared.NextByte(),
				green: Random.Shared.NextByte(),
				blue: Random.Shared.NextByte(),
				alpha: Random.Shared.NextByte()
			);

			var linePaint = new SKPaint
			{
				Color = lineColor, StrokeWidth = lineWidth, IsAntialias = true, Style = SKPaintStyle.Stroke
			};

			Canvas.DrawLine(firstVertex.X, firstVertex.Y, secondVertex.X, secondVertex.Y, linePaint);
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
		Canvas.DrawText(
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
		var bytes = Bitmap!.Bytes;
		_window.Draw(bytes);
	}
}
