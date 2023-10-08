namespace NGame.Setup;



public interface INGame
{
	INGameEnvironment NGameEnvironment { get; }
	IServiceProvider Services { get; }
}
