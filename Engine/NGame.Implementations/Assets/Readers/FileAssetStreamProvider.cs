using NGame.Assets;

namespace NGame.Core.Assets.Readers;



public class FileAssetStreamProvider : IAssetStreamProvider
{
	private readonly string _assetFolder;


	public FileAssetStreamProvider(string assetFolder)
	{
		_assetFolder = assetFolder;
	}


	public static FileAssetStreamProvider CreateDefault()
	{
		var assetFolder = Path.Combine(AppContext.BaseDirectory, AssetConventions.AssetPackSubFolder);
		return new FileAssetStreamProvider(assetFolder);
	}


	public Stream Open(string fileName)
	{
		var fullAssetPackPath = Path.Combine(_assetFolder, fileName);
		return File.Open(fullAssetPackPath, FileMode.Open);
	}
}
