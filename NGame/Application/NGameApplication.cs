using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGame.Application;



public class NGameApplication
{
	private readonly IHost _host;


	public NGameApplication(IHost host)
	{
		_host = host;
	}


	public IServiceProvider Services => _host.Services;


	public static INGameApplicationBuilder CreateBuilder() => new NGameApplicationBuilder();


	public async Task Run()
	{
		var cancellationTokenSource = new CancellationTokenSource();
		var cancellationToken = cancellationTokenSource.Token;

		await _host.StartAsync(cancellationToken);

		var hostedService = _host.Services.GetRequiredService<NGameHostedService>();
		hostedService.RunGame();
	}
}
