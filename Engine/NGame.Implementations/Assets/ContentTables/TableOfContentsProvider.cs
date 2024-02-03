using System.Text.Json;
using System.Text.Json.Serialization;
using NGame.Assets;
using NGame.Implementations.Assets.Readers;

namespace NGame.Implementations.Assets.ContentTables;



public interface ITableOfContentsProvider
{
	TableOfContents Get();
}



public class TableOfContentsProvider(
	IAssetStreamProvider assetStreamProvider,
	IEnumerable<JsonConverter> jsonConverters
)
	: ITableOfContentsProvider
{
	private TableOfContents? TableOfContents { get; set; }


	public TableOfContents Get()
	{
		return TableOfContents ??= ReadTableOfContents();
	}


	private TableOfContents ReadTableOfContents()
	{
		var jsonSerializerOptions = new JsonSerializerOptions();
		foreach (var jsonConverter in jsonConverters)
		{
			jsonSerializerOptions.Converters.Add(jsonConverter);
		}

		using var tocFileStream = assetStreamProvider.Open(AssetConventions.TableOfContentsFileName);
		return JsonSerializer.Deserialize<TableOfContents>(tocFileStream, jsonSerializerOptions)!;
	}
}
