using System.Text.Json;
using System.Text.Json.Serialization;
using NGame.Assets;

namespace NGame.Implementations.Assets.ContentTables;



public interface ITableOfContentsProvider
{
	TableOfContents Get();
}



public class TableOfContentsProvider : ITableOfContentsProvider
{
	private readonly IAssetStreamProvider _assetStreamProvider;
	private readonly IEnumerable<JsonConverter> _jsonConverters;


	public TableOfContentsProvider(
		IAssetStreamProvider assetStreamProvider,
		IEnumerable<JsonConverter> jsonConverters
	)
	{
		_assetStreamProvider = assetStreamProvider;
		_jsonConverters = jsonConverters;
	}


	private TableOfContents? TableOfContents { get; set; }


	public TableOfContents Get()
	{
		return TableOfContents ??= ReadTableOfContents();
	}


	private TableOfContents ReadTableOfContents()
	{
		var jsonSerializerOptions = new JsonSerializerOptions();
		foreach (var jsonConverter in _jsonConverters)
		{
			jsonSerializerOptions.Converters.Add(jsonConverter);
		}

		using var tocFileStream = _assetStreamProvider.Open(AssetConventions.TableOfContentsFileName);
		return JsonSerializer.Deserialize<TableOfContents>(tocFileStream, jsonSerializerOptions)!;
	}
}
