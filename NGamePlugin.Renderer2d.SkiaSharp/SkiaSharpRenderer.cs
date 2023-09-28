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


	public SkiaSharpRenderer(ILogger<SkiaSharpRenderer> logger, IOsWindow window)
	{
		_logger = logger;
		_window = window;
	}


	private SKImageInfo ImageInfo { get; set; }
	private SKBitmap? Bitmap { get; set; }
	private SKSurface? Surface { get; set; }
	private SKCanvas? Canvas { get; set; }

	public void Initialize()
	{
		_logger.LogDebug("Initialize");

		
		 ImageInfo = new SKImageInfo(
			width: 250,
			height: 250,
			colorType: SKColorType.Rgba8888,
			alphaType: SKAlphaType.Premul);

		Bitmap = new SKBitmap(ImageInfo);

		
		 Surface = SKSurface.Create(ImageInfo);

		 Canvas = new SKCanvas(Bitmap);
		//Surface.Canvas;

		Canvas.Clear(SKColor.Parse("#003366"));

		/*
		int lineCount = 1000;
		for (int i = 0; i < lineCount; i++)
		{
			DrawRandomLine();
		}*/

		
	}


	private IntPtr _pixelsPointer;
	public void SetPixelsPointer(IntPtr pixelsPointer)
	{
		_pixelsPointer = pixelsPointer;
		Bitmap.SetPixels(pixelsPointer);
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
	
	
	private  void DrawRandomLine()
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
