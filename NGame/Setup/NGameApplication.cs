using Microsoft.Extensions.Hosting;

namespace NGame.Setup;

public class NGameApplication
{
	private readonly IHost _host;


	public NGameApplication(IHost host)
	{
		_host = host;
	}


	public IServiceProvider Services => _host.Services;


	public static INGameApplicationBuilder CreateBuilder() => new NGameApplicationBuilder();


	public async Task Run() => await _host.StartAsync();
}
