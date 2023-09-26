using System.Text.Json;
using NGame.Assets;
using NGame.Ecs;
using NGame.Scenes.JsonModels;

namespace NGame.Scenes;

public class Scene
{
    public ICollection<Entity> Entities { get; } = new List<Entity>();
}



public class SceneSerializer : IAssetSerializer<Scene>
{
    public Scene Deserialize(string filePath)
    {
        using var openStream = File.OpenRead(filePath);
        var sceneJson = JsonSerializer.Deserialize<SceneJson>(openStream)!;


        var scene = new Scene();
        foreach (var entityJson in sceneJson.Entities)
        {
            var entity = new Entity
            {
                Id = entityJson.Id,
                Name = entityJson.Name
            };
            scene.Entities.Add(entity);
        }

        return scene;
    }
}
