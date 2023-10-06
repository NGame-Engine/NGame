namespace NGame.Application;



public interface INGameSetup
{
	void ConfigureApplication(INGameApplicationBuilder builder);
	void SetUpApplicationStart(NGameApplication app);
}
