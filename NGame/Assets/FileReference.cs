namespace NGame.Assets;



public class FileReference : IEquatable<FileReference>
{
	public FileReference(string filePath)
	{
		FilePath = filePath;
	}


	/// <summary>
	/// The file path relative to the running application.
	/// </summary>
	public string FilePath { get; }


	public bool Equals(FileReference? other)
	{
		if (ReferenceEquals(null, other))
		{
			return false;
		}

		if (ReferenceEquals(this, other))
		{
			return true;
		}

		return FilePath == other.FilePath;
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

		return Equals((FileReference)obj);
	}


	public override int GetHashCode()
	{
		return FilePath.GetHashCode();
	}
}
