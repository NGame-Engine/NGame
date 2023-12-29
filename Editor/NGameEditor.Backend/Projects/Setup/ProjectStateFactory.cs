namespace NGameEditor.Backend.Projects.Setup;



public interface IProjectStateFactory
{
	IProjectState Create();
}



internal class ProjectStateFactory : IProjectStateFactory
{
	public IProjectState Create()
	{
		return new ProjectState();
	}
}
