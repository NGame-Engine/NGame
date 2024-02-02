using NGame.Cli.Abstractions.Paths;
using NGame.Cli.PackAssets.CommandValidators;

namespace NGame.Cli.PackAssets.AssetFileReaders;



public interface IAssetListLineParser
{
	ICollection<AssetFileEntry> ReadAssetFileEntries(ValidatedCommand validatedCommand);
}



public class AssetListLineParser : IAssetListLineParser
{
	private const string PackAndPathSeparator = "//";


	public ICollection<AssetFileEntry> ReadAssetFileEntries(ValidatedCommand validatedCommand)
	{
		var assetListFilePath = validatedCommand.AssetList;
		var assetListFileLines = File.ReadAllLines(assetListFilePath.Value);

		return
			assetListFileLines
				.Select(CreateAssetFileInfo)
				.ToList();
	}


	private static AssetFileEntry CreateAssetFileInfo(string line)
	{
		var indexOfPackNameEnd = line.IndexOf(PackAndPathSeparator, StringComparison.OrdinalIgnoreCase);
		if (indexOfPackNameEnd == -1)
		{
			throw new FormatException($"Unable to read {line}");
		}


		var packName = line.Substring(0, indexOfPackNameEnd);
		if (string.IsNullOrEmpty(packName)) packName = Conventions.DefaultAssetPackName;
		var packageName = new PackageName(packName);


		var relativeFilePath =
			line.Substring(indexOfPackNameEnd + PackAndPathSeparator.Length);

		var filePath = NormalizedPath.Create(relativeFilePath);


		return new AssetFileEntry(packageName, filePath);
	}
}
