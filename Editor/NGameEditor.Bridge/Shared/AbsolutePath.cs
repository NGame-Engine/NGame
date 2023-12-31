namespace NGameEditor.Bridge.Shared;



public class AbsolutePath : IEquatable<AbsolutePath>
{
	public AbsolutePath(string path)
	{
		if (System.IO.Path.IsPathFullyQualified(path) == false)
		{
			throw new InvalidOperationException($"Path {path} is not fully qualified");
		}

		Path = path;
	}


	public string Path { get; }


	public AbsolutePath CombineWith(params string[] paths) =>
		new(
			System.IO.Path.Combine(
				paths
					.Prepend(Path)
					.ToArray()
			)
		);


	public AbsolutePath? GetParentDirectory()
	{
		var parentDirectory = System.IO.Path.GetDirectoryName(Path);
		return
			parentDirectory == null
				? null
				: new AbsolutePath(parentDirectory!);
	}


	public bool Equals(AbsolutePath? other)
	{
		if (ReferenceEquals(null, other))
		{
			return false;
		}

		if (ReferenceEquals(this, other))
		{
			return true;
		}

		return Path == other.Path;
	}


	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj))
		{
			return false;
		}

		if (ReferenceEquals(this, obj))
		{
			return true;
		}

		if (obj.GetType() != this.GetType())
		{
			return false;
		}

		return Equals((AbsolutePath)obj);
	}


	public override int GetHashCode()
	{
		return Path.GetHashCode();
	}


	public static bool operator ==(AbsolutePath? left, AbsolutePath? right)
	{
		return Equals(left, right);
	}


	public static bool operator !=(AbsolutePath? left, AbsolutePath? right)
	{
		return !Equals(left, right);
	}
}
