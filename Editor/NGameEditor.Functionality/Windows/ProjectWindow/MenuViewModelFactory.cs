using System.Diagnostics;
using NGameEditor.Functionality.Scenes;
using NGameEditor.ViewModels.ProjectWindows.MenuViews;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.ProjectWindow;



public interface IMenuViewModelFactory
{
	MenuViewModel Create();
}



public class MenuViewModelFactory(
	IAboutWindow aboutWindow,
	ISceneSaver sceneSaver
) : IMenuViewModelFactory
{
	public MenuViewModel Create()
	{
		var menuViewModel = new MenuViewModel();
		
		menuViewModel.SaveScene =ReactiveCommand.Create(sceneSaver.SaveCurrentScene);
		
		menuViewModel.OpenAboutWindow = ReactiveCommand.Create(aboutWindow.Open);
		
		menuViewModel.OpenDocumentation = 
			ReactiveCommand.Create(() =>
				Process.Start(
					new ProcessStartInfo
					{
						FileName = "https://github.com/NGame-Engine/NGame/wiki",
						UseShellExecute = true
					}
				)
			);


		return menuViewModel;
	}
}
