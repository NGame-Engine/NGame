using NGame.Ecs;
using NGame.SceneAssets;

namespace NGame.Core.SceneAssets;



public interface IScenePopulator
{
	Scene Populate(SceneAsset sceneAsset);
}



public class ScenePopulator : IScenePopulator
{
	private readonly IEntityEditor _entityChanger;


	public ScenePopulator(IEntityEditor entityChanger)
	{
		_entityChanger = entityChanger;
	}


	public Scene Populate(SceneAsset sceneAsset)
	{
		var scene = new Scene();

		foreach (var entityEntry in sceneAsset.Entities)
		{
			var entity = _entityChanger.CreateEntity(scene);
			foreach (var entityComponent in entityEntry.Components)
			{
				_entityChanger.AddComponent(entity, entityComponent);
			}
		}

		return scene;
	}
}
