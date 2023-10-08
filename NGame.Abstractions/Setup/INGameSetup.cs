namespace NGame.Setup;



public interface INGameSetup
{
	void ConfigureApplication(INGameBuilder builder);
	void SetUpApplicationStart(INGame app);
}
