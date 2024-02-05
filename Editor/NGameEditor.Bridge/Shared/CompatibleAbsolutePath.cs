namespace NGameEditor.Bridge.Shared;



/// <summary>
/// Wrapper that enforces a path string to be absolute and can be
/// (de)serialized sent between backend and frontend.
/// </summary>
public class CompatibleAbsolutePath : IEquatable<CompatibleAbsolutePath>
{
	public CompatibleAbsolutePath(string path)
	{
		if (System.IO.Path.IsPathFullyQualified(path) == false)
		{
			throw new InvalidOperationException($"Path {path} is not fully qualified");
		}

		Path = path;
	}


	public string Path { get; }


	public CompatibleAbsolutePath CombineWith(params string[] paths) =>
		new(
			System.IO.Path.Combine(
				paths
					.Prepend(Path)
					.ToArray()
			)
		);


	public bool Equals(CompatibleAbsolutePath? other)
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

		return Equals((CompatibleAbsolutePath)obj);
	}


	public override int GetHashCode()
	{
		return Path.GetHashCode();
	}


	public static bool operator ==(CompatibleAbsolutePath? left, CompatibleAbsolutePath? right)
	{
		return Equals(left, right);
	}


	public static bool operator !=(CompatibleAbsolutePath? left, CompatibleAbsolutePath? right)
	{
		return !Equals(left, right);
	}
}
