using System.Text.Json;
using System.Text.Json.Serialization;
using NGame.Assets.Common.Assets;
using NGame.Assets.Packer.AssetOverviews;
using Singulink.IO;

namespace NGame.Assets.Packer.FileWriters;



public interface ITableOfContentsWriter
{
	void Write(
		IEnumerable<AssetEntry> assetFileSpecifications,
		IAbsoluteDirectoryPath targetFolder
	);
}



public class TableOfContentsWriter(
	IEnumerable<JsonConverter> jsonConverters
) : ITableOfContentsWriter
{
	public void Write(
		IEnumerable<AssetEntry> assetFileSpecifications,
		IAbsoluteDirectoryPath targetFolder
	)
	{
		var tableOfContents = CreateTableOfContents(assetFileSpecifications);
		WriteTableOfContentsFile(targetFolder, tableOfContents);
	}


	private static TableOfContents CreateTableOfContents(IEnumerable<AssetEntry> assetFileSpecifications) =>
		new()
		{
			ResourceIdentifiers =
				assetFileSpecifications
					.ToDictionary(
						x => x.Id,
						x => new ContentEntry
						{
							PackFileName = $"{x.PackageName}{AssetConventions.PackFileEnding}",
							FilePath = x.MainPathInfo.TargetPath.PathDisplay
						}
					)
		};


	private void WriteTableOfContentsFile(IAbsoluteDirectoryPath targetFolder, TableOfContents tableOfContents)
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
