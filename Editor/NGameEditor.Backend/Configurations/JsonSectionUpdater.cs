using System.Text.Json.Nodes;
using Singulink.IO;

namespace NGameEditor.Backend.Configurations;



public interface IJsonSectionUpdater
{
	void UpdateSection(IAbsoluteFilePath filePath, string sectionName, object newContent);
}



public class JsonSectionUpdater : IJsonSectionUpdater
{
	public void UpdateSection(IAbsoluteFilePath filePath, string sectionName, object newContent)
	{
		var jsonFileContent = File.ReadAllText(filePath.PathExport);
		var jsonNode = JsonNode.Parse(jsonFileContent)!;
		jsonNode[sectionName] = JsonValue.Create(newContent);

		var updateJson = jsonNode.ToJsonString();
		File.WriteAllText(filePath.PathExport, updateJson);
	}
}
