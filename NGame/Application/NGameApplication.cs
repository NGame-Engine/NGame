using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGame.Application;



public sealed class NGameApplication
{
	private readonly IHost _host;


	public NGameApplication(IHost host)
	{
		_host = host;
	}


	public IServiceProvider Services => _host.Services;


	public static INGameApplicationBuilder CreateBuilder() =>
		new NGameApplicationBuilder();


	public void Run()
	{
		var applicationEvents = Services.GetRequiredService<IApplicationEvents>();
		var gameRunner = Services.GetRequiredService<IGameRunner>();
		applicationEvents.Closing += (_, _) => gameRunner.Stop();
		gameRunner.RunGame();
	}
}
