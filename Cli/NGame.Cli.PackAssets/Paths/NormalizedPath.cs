namespace NGame.Cli.PackAssets.Paths;



public class NormalizedPath
{
	private NormalizedPath(string value)
	{
		Value = value;
	}


	public string Value { get; }


	public static NormalizedPath Create(string path) =>
		new(path.Replace('\\', '/'));
}
