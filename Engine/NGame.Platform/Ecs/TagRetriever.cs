using NGame.Ecs;
using NGame.Parallelism;

namespace NGame.Implementations.Ecs;



public interface ITagRetriever
{
	IEnumerable<Entity> GetEntitiesWithTag(string tag);
}



public class TagRetriever(
	ITaskScheduler taskScheduler,
	IRootSceneAccessor rootSceneAccessor
)
	: ITagRetriever
{
	public IEnumerable<Entity> GetEntitiesWithTag(string tag)
	{
		var rootScene = rootSceneAccessor.RootScene;
		var allScenes = GetAllScenesRecursive(rootScene);


		return
			allScenes
				.SelectMany(x => x.RootTransforms)
				.SelectMany(x => FindTagInTransformsRecursive(x, tag))
				.AsParallel(taskScheduler);
	}


	private static IEnumerable<Scene> GetAllScenesRecursive(Scene scene)
	{
		yield return scene;

		foreach (var childScene in scene.Children)
		{
			yield return childScene;
		}
	}


	private static IEnumerable<Entity> FindTagInTransformsRecursive(Entity transform, string tag)
	{
		if (transform.Tag == tag) yield return transform;

		var resultsFromChildren =
			transform
				.Children
				.SelectMany(child => FindTagInTransformsRecursive(child, tag));

		foreach (var result in resultsFromChildren)
		{
			yield return result;
		}
	}
}
