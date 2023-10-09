namespace NGame.Setup;



public interface INGameEnvironment
{
	string ApplicationName { get; set; }
	Platform Platform { get; }
	string EnvironmentName { get; set; }

	bool IsDevelopment();
}
