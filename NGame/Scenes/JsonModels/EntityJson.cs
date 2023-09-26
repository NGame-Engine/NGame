namespace NGame.Scenes.JsonModels;

public class EntityJson
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public IEnumerable<ComponentJson> Components { get; set; }
}
