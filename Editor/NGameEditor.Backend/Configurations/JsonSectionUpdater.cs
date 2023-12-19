using System.Text.Json.Nodes;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Backend.Configurations;



public interface IJsonSectionUpdater
{
	void UpdateSection(AbsolutePath filePath, string sectionName, object newContent);
}



public class JsonSectionUpdater : IJsonSectionUpdater
{
	public void UpdateSection(AbsolutePath filePath, string sectionName, object newContent)
	{
		var jsonFileContent = File.ReadAllText(filePath.Path);
		var jsonNode = JsonNode.Parse(jsonFileContent)!;
		jsonNode[sectionName] = JsonValue.Create(newContent);

		var updateJson = jsonNode.ToJsonString();
		File.WriteAllText(filePath.Path, updateJson);
	}
}
