namespace NGame.Tooling.Ecs;



public class SceneAssetsConfiguration
{
	public static readonly string JsonElementName = "SceneAssets";

	public HashSet<Guid> Scenes { get; set; } = [];
	public Guid StartScene { get; set; }
}
