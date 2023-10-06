namespace NGame.Setup;



public interface INGameSetup
{
	void ConfigureApplication(INGameApplicationBuilder builder);
	void SetUpApplicationStart(INGameApplication app);
}
