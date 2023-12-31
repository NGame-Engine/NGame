using NGameEditor.ViewModels.Controllers;
using NGameEditor.ViewModels.ProjectWindows.MenuViews;

namespace NGameEditor.Functionality.Windows;



public interface IMenuViewModelFactory
{
	MenuViewModel Create();
}



public class MenuViewModelFactory(
	ISceneController sceneController,
	IMenuController menuController
) : IMenuViewModelFactory
{
	public MenuViewModel Create()
	{
		var menuViewModel = new MenuViewModel
		{
			SaveScene = sceneController.SaveScene(),
			OpenAboutWindow = menuController.OpenAboutWindow(),
			OpenDocumentation = menuController.OpenDocumentation()
		};


		return menuViewModel;
	}
}
