using System.Drawing;
using SkiaSharp;

namespace NGamePlugin.Renderer2d.SkiaSharp;

public static class Extensions
{
	public static SKRect ToSkRect(this Rectangle rectangle) =>
		SKRect.Create(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
}
