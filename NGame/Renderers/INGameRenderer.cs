using Microsoft.Extensions.Logging;
using NGame.UpdaterSchedulers;

namespace NGame.Renderers;

public interface INGameRenderer
{
	void Initialize();

	Task<bool> BeginDraw();
	Task Draw(GameTime drawLoopTime);
	Task EndDraw(bool shouldPresent);
}



class NGameRenderer : INGameRenderer
{
	private readonly ILogger<NGameRenderer> _logger;


	public NGameRenderer(ILogger<NGameRenderer> logger)
	{
		_logger = logger;
	}


	public void Initialize()
	{
		_logger.LogInformation("Initialize");
	}


	public Task<bool> BeginDraw()
	{
		//_logger.LogInformation("BeginDraw");
		return Task.FromResult(true);
	}


	public Task Draw(GameTime drawLoopTime)
	{
		return Task.CompletedTask;
	}


	public Task EndDraw(bool shouldPresent)
	{
		//_logger.LogInformation("EndDraw");
		return Task.CompletedTask;
	}
}
