using Microsoft.Extensions.DependencyInjection;
using NGame.Assets;
using NGame.Services;
using NGame.Setup;

namespace NGame;



public static class Graphics2DInstaller
{
	public static INGameBuilder AddGraphics2D(this INGameBuilder builder)
	{
		builder.Services.AddSingleton<IResourceRegistry<Font>, ResourceRegistry<Font>>();
		builder.Services.AddSingleton<IResourceRegistry<Texture>, ResourceRegistry<Texture>>();

		return builder;
	}


	public static INGame UseGraphics2D(this INGame app)
	{
		app.RegisterComponentsFromAssembly(typeof(Graphics2DInstaller).Assembly);

		return app;
	}
}
