using Microsoft.Extensions.DependencyInjection;
using NGame.Renderers;
using NGame.Setup;

namespace NGamePlugin.Renderer2D.Sfml;

public static class Renderer2DSfmlInstaller
{
	public static void AddRenderer2dSfml(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<IRenderTexture, SfmlRenderTexture>();
		builder.Services.AddSingleton<INGameRenderer, SfmlRenderer>();
	}


	public static void UseRenderer2dSfml(this NGameApplication app)
	{
	}
}
