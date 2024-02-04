using Singulink.IO;

namespace NGameEditor.Bridge.Projects;



public class ProjectId(IAbsoluteFilePath solutionFilePath) : IEquatable<ProjectId>
{
	public IAbsoluteFilePath SolutionFilePath { get; } = solutionFilePath;


	public IAbsoluteDirectoryPath GetAbsoluteSolutionFolder() =>
		SolutionFilePath.ParentDirectory!;


	public bool Equals(ProjectId? other)
	{
		if (ReferenceEquals(null, other))
		{
			return false;
		}

		if (ReferenceEquals(this, other))
		{
			return true;
		}

		return SolutionFilePath.Equals(other.SolutionFilePath);
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

		return Equals((ProjectId)obj);
	}


	public override int GetHashCode()
	{
		return SolutionFilePath.GetHashCode();
	}


	public static bool operator ==(ProjectId? left, ProjectId? right)
	{
		return Equals(left, right);
	}


	public static bool operator !=(ProjectId? left, ProjectId? right)
	{
		return !Equals(left, right);
	}
}
