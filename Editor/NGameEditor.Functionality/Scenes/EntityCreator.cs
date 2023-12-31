using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Results;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.Functionality.Scenes;



public interface IEntityCreator
{
	Result CreateEntity(EntityState? parentEntity);
}



public class EntityCreator(
	IClientRunner<IBackendApi> clientRunner,
	IEntityStateMapper entityStateMapper,
	SceneState sceneState,
	ILogger<EntityCreator> logger
) : IEntityCreator
{
	public Result CreateEntity(EntityState? parentEntity) =>
		clientRunner
			.GetClientService()
			.Then(x => x.AddEntity(parentEntity?.Id))
			.Then(entityStateMapper.Map)
			.Then(x =>
			{
				if (parentEntity != null) parentEntity.Children.Add(x);
				else sceneState.SceneEntities.Add(x);
			})
			.IfError(logger.Log);
}
