using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Setup;

namespace NGame.Application;



public sealed class NGameApplication : INGameApplication
{
	private readonly IHost _host;


	public NGameApplication(INGameEnvironment nGameEnvironment, IHost host)
	{
		NGameEnvironment = nGameEnvironment;
		_host = host;
	}


	public INGameEnvironment NGameEnvironment { get; }
	public IServiceProvider Services => _host.Services;


	public static NGameApplicationBuilder CreateBuilder() => new();


	public void Run()
	{
		var applicationEvents = Services.GetRequiredService<IApplicationEvents>();
		var gameRunner = Services.GetRequiredService<IGameRunner>();
		applicationEvents.Closing += (_, _) => gameRunner.Stop();
		gameRunner.RunGame();
	}
}
