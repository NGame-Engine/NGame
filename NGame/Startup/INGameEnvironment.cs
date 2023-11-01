using NGame.Services;

namespace NGame.Startup;



public interface INGameEnvironment
{
	string ApplicationName { get; set; }
	Platform Platform { get; set; }
	string EnvironmentName { get; set; }
	IFileProvider FileProvider { get; set; }

	bool IsDevelopment();
}
