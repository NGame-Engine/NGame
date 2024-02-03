using System.Text.Json;
using System.Text.Json.Serialization;
using NGame.Cli.PackAssetsCommand.Paths;
using NGame.Tooling.Assets;

namespace NGame.Cli.PackAssetsCommand.Writers;



public interface ITableOfContentsWriter
{
	[Obsolete]
	void Write(TableOfContents tableOfContents, AbsoluteNormalizedPath targetFolder);
}



public class TableOfContentsWriter : ITableOfContentsWriter
{
	private readonly IEnumerable<JsonConverter> _jsonConverters;


	public TableOfContentsWriter(IEnumerable<JsonConverter> jsonConverters)
	{
		_jsonConverters = jsonConverters;
	}

	[Obsolete]
	public void Write(TableOfContents tableOfContents, AbsoluteNormalizedPath targetFolder)
	{
		var path = targetFolder.Combine(AssetConventions.TableOfContentsFileName);
		using var fileStream = File.Create(path.Value);

		var jsonSerializerOptions = new JsonSerializerOptions();
		foreach (var jsonConverter in _jsonConverters)
		{
			jsonSerializerOptions.Converters.Add(jsonConverter);
		}

		JsonSerializer.Serialize(fileStream, tableOfContents, jsonSerializerOptions);
	}
}
