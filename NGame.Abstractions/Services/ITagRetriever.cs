namespace NGame.Services;



public interface ITagRetriever
{
	IEnumerable<Entity> GetEntitiesWithTag(string tag);
}
