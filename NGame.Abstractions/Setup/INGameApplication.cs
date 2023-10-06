namespace NGame.Setup;



public interface INGameApplication
{
	INGameEnvironment NGameEnvironment { get; }
	IServiceProvider Services { get; }
}
