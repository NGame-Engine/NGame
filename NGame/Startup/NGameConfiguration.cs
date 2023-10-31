namespace NGame.Startup;



public class NGameConfiguration
{
	public static readonly string JsonElementName = "NGame";

	public List<Guid> Scenes { get; set; } = new();
	public Guid StartScene { get; set; }
}
