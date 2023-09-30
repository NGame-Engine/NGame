using Microsoft.Extensions.DependencyInjection;
using NGame.Application;
using NGame.Renderers;

namespace NGamePlugin.Renderer2D.Sfml;

public static class Renderer2DSfmlInstaller
{
	public static void AddRenderer2dSfml(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<INGameRenderer, SfmlRenderer>();
	}


	public static void UseRenderer2dSfml(this NGameApplication app)
	{
	}
}
