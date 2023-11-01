namespace NGame.Startup;



public interface INGame
{
	INGameEnvironment NGameEnvironment { get; }
	IServiceProvider Services { get; }
}
