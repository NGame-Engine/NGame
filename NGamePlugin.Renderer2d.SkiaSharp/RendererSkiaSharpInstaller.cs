using Microsoft.Extensions.DependencyInjection;
using NGame.Renderers;
using NGame.Setup;

namespace NGamePlugin.Renderer2d.SkiaSharp;

public static class RendererSkiaSharpInstaller
{
	public static void AddRendererSkiaSharp(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<INGameRenderer, SkiaSharpRenderer>();
	}


	public static void UseRendererSkiaSharp(this NGameApplication app)
	{
	}
}
