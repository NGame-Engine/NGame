namespace NGame.Cli.Abstractions.Paths;



[Obsolete("Use Singulink.IO library instead")]
public class AbsoluteNormalizedPath
{
	private AbsoluteNormalizedPath(string value)
	{
		Value = value;
	}


	public string Value { get; }


	public static AbsoluteNormalizedPath Create(string path) =>
		new(
			Path
				.GetFullPath(path.Length == 0 ? "." : path)
				.Replace('\\', '/')
		);


	public AbsoluteNormalizedPath Combine(string path) =>
		new(
			Path
				.Combine(Value, path)
				.Replace('\\', '/')
		);


	public override string ToString() => $"Absolute Path: {Value}";
}
