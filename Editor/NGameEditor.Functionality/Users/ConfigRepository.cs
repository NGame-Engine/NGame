using System.Text.Json;
using NGameEditor.Bridge.Shared;
using NGameEditor.Functionality.Shared.Files;
using NGameEditor.Functionality.Shared.Json;

namespace NGameEditor.Functionality.Users;



public interface IConfigRepository
{
	NGameStudioConfiguration GetAppConfiguration();
	void SaveAppConfiguration(NGameStudioConfiguration nGameStudioConfiguration);
}



public class ConfigRepository : IConfigRepository
{
	private readonly IAppFilePathProvider _appFilePathProvider;
	private readonly IFileAccessWrapper _fileAccessWrapper;
	private readonly IEditorSerializerOptionsFactory _editorSerializerOptionsFactory;


	public ConfigRepository(
		IAppFilePathProvider appFilePathProvider,
		IFileAccessWrapper fileAccessWrapper,
		IEditorSerializerOptionsFactory editorSerializerOptionsFactory
	)
	{
		_appFilePathProvider = appFilePathProvider;
		_fileAccessWrapper = fileAccessWrapper;
		_editorSerializerOptionsFactory = editorSerializerOptionsFactory;
	}


	private JsonSerializerOptions? JsonSerializerOptions { get; set; }


	public NGameStudioConfiguration GetAppConfiguration()
	{
		var configFilePath = _appFilePathProvider.GetConfigFilePath();

		if (!_fileAccessWrapper.FileExists(configFilePath))
		{
			var newConfiguration = new NGameStudioConfiguration();
			SaveAppConfiguration(newConfiguration);
		}

		var allText = File.ReadAllText(configFilePath);
		var nGameStudioConfiguration =
			JsonSerializer.Deserialize<NGameStudioConfiguration>(allText, GetJsonSerializerOptions());

		if (nGameStudioConfiguration != null) return nGameStudioConfiguration;


		var message = $"Config file at {configFilePath} is faulty";
		throw new InvalidOperationException(message);
	}


	public void SaveAppConfiguration(NGameStudioConfiguration nGameStudioConfiguration)
	{
		var configFilePath = _appFilePathProvider.GetConfigFilePath();

		var configFolder = Path.GetDirectoryName(configFilePath)!;
		Directory.CreateDirectory(configFolder);

		var json = JsonSerializer.Serialize(nGameStudioConfiguration, GetJsonSerializerOptions());
		FileHelper.SaveFileContentViaIntermediate(json, configFilePath);
	}


	private JsonSerializerOptions GetJsonSerializerOptions() =>
		JsonSerializerOptions ??= _editorSerializerOptionsFactory.Create();
}
