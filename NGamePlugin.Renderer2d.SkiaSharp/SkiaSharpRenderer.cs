using Microsoft.Extensions.Logging;
using NGame.Maths;
using NGame.OsWindows;
using NGame.Renderers;
using NGame.UpdateSchedulers;
using SkiaSharp;

namespace NGamePlugin.Renderer2d.SkiaSharp;

public class SkiaSharpRenderer : INGameRenderer
{
	private readonly ILogger<SkiaSharpRenderer> _logger;
	private readonly IOsWindow _window;
	private readonly GraphicsConfiguration _graphicsConfiguration;


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


	public void Initialize()
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


	public bool BeginDraw()
	{
		//_logger.LogInformation("BeginDraw");
		return true;
	}


	public void Draw(GameTime drawLoopTime)
	{
		DrawRandomLine();

		var bytes = Bitmap!.Bytes;
		_window.Draw(bytes);
	}


	public void EndDraw(bool shouldPresent)
	{
		//_logger.LogInformation("EndDraw");
	}


	private void DrawRandomLine()
	{
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

		int x1 = Random.Shared.Next(ImageInfo.Width);
		int y1 = Random.Shared.Next(ImageInfo.Height);
		int x2 = Random.Shared.Next(ImageInfo.Width);
		int y2 = Random.Shared.Next(ImageInfo.Height);
		Canvas.DrawLine(x1, y1, x2, y2, linePaint);
	}
}
