using NGame.Ecs;
using NGame.Parallelism;

namespace NGame.Core.Ecs;



public class TagRetriever : ITagRetriever
{
	private readonly ITaskScheduler _taskScheduler;
	private readonly IRootSceneAccessor _rootSceneAccessor;


	public TagRetriever(ITaskScheduler taskScheduler, IRootSceneAccessor rootSceneAccessor)
	{
		_taskScheduler = taskScheduler;
		_rootSceneAccessor = rootSceneAccessor;
	}


	public IEnumerable<Entity> GetEntitiesWithTag(string tag)
	{
		var rootScene = _rootSceneAccessor.RootScene;
		var allScenes = GetAllScenesRecursive(rootScene);


		return
			allScenes
				.SelectMany(x => x.RootTransforms)
				.SelectMany(x => FindTagInTransformsRecursive(x, tag))
				.AsParallel(_taskScheduler);
	}


	private IEnumerable<Scene> GetAllScenesRecursive(Scene scene)
	{
		yield return scene;

		foreach (var childScene in scene.Children)
		{
			yield return childScene;
		}
	}


	private IEnumerable<Entity> FindTagInTransformsRecursive(Entity transform, string tag)
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
