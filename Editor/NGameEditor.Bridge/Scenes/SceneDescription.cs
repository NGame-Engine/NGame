namespace NGameEditor.Bridge.Scenes;



public class SceneDescription(string? fileName, Guid id, List<EntityDescription> entities)
{
	public string? FileName { get; } = fileName;
	public Guid Id { get; } = id;
	public List<EntityDescription> Entities { get; } = entities;
}
