namespace NGame.Ecs;



public interface ITagRetriever
{
	IEnumerable<Entity> GetEntitiesWithTag(string tag);
}
