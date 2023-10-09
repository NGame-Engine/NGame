using NGame.Setup;

namespace NGamePlatform.Base;



public class NGameEnvironment : INGameEnvironment
{
	public static readonly string Development = "Development";
	public static readonly string Production = "Production";


	public NGameEnvironment(
		string applicationName,
		Platform platform,
		string environmentName
	)
	{
		ApplicationName = applicationName;
		Platform = platform;
		EnvironmentName = environmentName;
	}


	public string ApplicationName { get; set; }
	public Platform Platform { get; }
	public string EnvironmentName { get; set; }


	public bool IsDevelopment() => EnvironmentName == Development;
}
