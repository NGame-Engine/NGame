namespace NGame.Assets;



public abstract class Asset
{
	protected Asset(string filePath)
	{
		FilePath = filePath;
	}


	public string FilePath { get; }
}
