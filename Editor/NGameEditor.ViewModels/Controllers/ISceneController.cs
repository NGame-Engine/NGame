using System.Windows.Input;
using NGameEditor.Results;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.Controllers;



public interface ISceneController
{
	Result CreateEntity(EntityState? parentEntity);
	Result Remove(EntityState entityState);

	ICommand SaveScene();
}
