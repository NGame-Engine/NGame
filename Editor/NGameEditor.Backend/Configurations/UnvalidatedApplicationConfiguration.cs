namespace NGameEditor.Backend.Configurations;



public class UnvalidatedApplicationConfiguration
{
	public int? FrontendPort { get; init; }
	public int BackendPort { get; init; }
	public string Solution { get; init; } = "";
}
