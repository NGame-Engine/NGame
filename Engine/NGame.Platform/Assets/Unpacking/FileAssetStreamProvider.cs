using NGame.Assets.Common.Assets;

namespace NGame.Platform.Assets.Unpacking;

public interface IAssetStreamProvider
{
	Stream Open(string fileName);
}


public class FileAssetStreamProvider(string assetFolder) : IAssetStreamProvider
{
	public static FileAssetStreamProvider CreateDefault()
	{
		var assetFolder = Path.Combine(AppContext.BaseDirectory, AssetConventions.AssetPackSubFolder);
		return new FileAssetStreamProvider(assetFolder);
	}


	public Stream Open(string fileName)
	{
		var fullAssetPackPath = Path.Combine(assetFolder, fileName);
		return File.Open(fullAssetPackPath, FileMode.Open);
	}
}
