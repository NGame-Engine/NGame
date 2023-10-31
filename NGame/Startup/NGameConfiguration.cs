namespace NGame.Startup;



public class NGameConfiguration
{
	public static string JsonElementName = "NGame";

	public List<Guid> Scenes { get; init; } = new();
	public Guid StartScene { get; init; }
}
