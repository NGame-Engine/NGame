using NGame.Components.Transforms;
using NGame.NGameSystem.Parallelism;
using NGame.Scenes;

namespace NGame.Ecs;



public interface ITagRetriever
{
	IEnumerable<Entity> GetEntitiesWithTag(string tag);
}



public class TagRetriever : ITagRetriever
{
	private readonly IRootSceneAccessor _rootSceneAccessor;
	private readonly ITaskScheduler _taskScheduler;


	public TagRetriever(IRootSceneAccessor rootSceneAccessor, ITaskScheduler taskScheduler)
	{
		_rootSceneAccessor = rootSceneAccessor;
		_taskScheduler = taskScheduler;
	}


	public IEnumerable<Entity> GetEntitiesWithTag(string tag)
	{
		var rootScene = _rootSceneAccessor.RootScene;
		var allScenes = GetAllScenesRecursive(rootScene);


		return
			allScenes
				.SelectMany(x => x.RootEntities)
				.Select(x => x.Transform)
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


	private IEnumerable<Entity> FindTagInTransformsRecursive(Transform transform, string tag)
	{
		if (transform.Entity.Tag == tag) yield return transform.Entity;

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
