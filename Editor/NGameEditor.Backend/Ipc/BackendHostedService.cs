using Microsoft.Extensions.Hosting;
using NGameEditor.Backend.Ipc.Setup;

namespace NGameEditor.Backend.Ipc;



public class BackendHostedService : IHostedService
{
	private readonly IIpcRunner _ipcRunner;


	public BackendHostedService(IIpcRunner ipcRunner)
	{
		_ipcRunner = ipcRunner;
	}


	public Task StartAsync(CancellationToken cancellationToken)
	{
		_ipcRunner.Start();
		return Task.CompletedTask;
	}


	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
