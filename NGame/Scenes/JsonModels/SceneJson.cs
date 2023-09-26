using NGame.Assets;

namespace NGame.Scenes.JsonModels;

public class SceneJson : Asset
{
    public IEnumerable<EntityJson> Entities { get; set; }
}
