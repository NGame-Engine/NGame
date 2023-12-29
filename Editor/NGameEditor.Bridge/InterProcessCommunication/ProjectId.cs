using NGameEditor.Bridge.Shared;

namespace NGameEditor.Bridge.InterProcessCommunication;



public class ProjectId : IEquatable<ProjectId>
{
	public ProjectId(AbsolutePath solutionFilePath)
	{
		SolutionFilePath = solutionFilePath;
	}


	public AbsolutePath SolutionFilePath { get; }


	public AbsolutePath GetAbsoluteSolutionFolder() =>
		SolutionFilePath.GetParentDirectory()!;


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
