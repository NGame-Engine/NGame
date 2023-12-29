namespace NGameEditor.Functionality.Shared.Files;



public interface IAppFilePathProvider
{
	string GetConfigFilePath();
}



public class AppFilePathProvider : IAppFilePathProvider
{
	public string GetConfigFilePath()
	{
		var appDataFolder = Environment.GetFolderPath(
			Environment.SpecialFolder.ApplicationData,
			Environment.SpecialFolderOption.Create
		);

		return Path.Combine(appDataFolder, "NGame.Editor", "config.json");
	}
}
