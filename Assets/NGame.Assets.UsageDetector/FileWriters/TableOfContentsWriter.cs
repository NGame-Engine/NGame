using System.Text.Json;
using System.Text.Json.Serialization;
using NGame.Tooling.Assets;
using Singulink.IO;

namespace NGame.Assets.UsageDetector.FileWriters;



public interface ITableOfContentsWriter
{
	void Write(TableOfContents tableOfContents, IAbsoluteDirectoryPath targetFolder);
}



public class TableOfContentsWriter(
	IEnumerable<JsonConverter> jsonConverters
) : ITableOfContentsWriter
{
	public void Write(TableOfContents tableOfContents, IAbsoluteDirectoryPath targetFolder)
	{
		var jsonSerializerOptions = new JsonSerializerOptions();
		foreach (var jsonConverter in jsonConverters)
		{
			jsonSerializerOptions.Converters.Add(jsonConverter);
		}

		var jsonString = JsonSerializer.Serialize(tableOfContents, jsonSerializerOptions);


		var path = targetFolder.CombineFile(AssetConventions.TableOfContentsFileName);
		File.WriteAllText(path.PathDisplay, jsonString);
	}
}
