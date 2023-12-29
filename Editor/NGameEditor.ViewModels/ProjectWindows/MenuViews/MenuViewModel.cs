using System.Windows.Input;
using NGameEditor.ViewModels.Controllers;

namespace NGameEditor.ViewModels.ProjectWindows.MenuViews;



public class MenuViewModel(
	ISceneController sceneController,
	IMenuController menuController
) : ViewModelBase
{
	public ICommand SaveScene { get; } = sceneController.SaveScene();
	public ICommand OpenAboutWindow { get; } = menuController.OpenAboutWindow();
	public ICommand OpenDocumentation { get; } = menuController.OpenDocumentation();
}
