using NGame.Setup;

namespace NGamePlatform.Base;



public sealed class NGame : INGame
{
	public NGame(INGameEnvironment nGameEnvironment, IServiceProvider services)
	{
		NGameEnvironment = nGameEnvironment;
		Services = services;
	}


	public INGameEnvironment NGameEnvironment { get; }
	public IServiceProvider Services { get; }
}
