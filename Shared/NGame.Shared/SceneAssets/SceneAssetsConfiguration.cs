namespace NGame.SceneAssets;



public class SceneAssetsConfiguration
{
	public static readonly string JsonElementName = "SceneAssets";

	public HashSet<Guid> Scenes { get; set; } = new();
	public Guid StartScene { get; set; }
}
