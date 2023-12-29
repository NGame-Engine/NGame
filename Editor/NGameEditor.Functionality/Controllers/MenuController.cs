using System.Diagnostics;
using System.Windows.Input;
using NGameEditor.Functionality.Windows;
using NGameEditor.ViewModels.Controllers;
using ReactiveUI;

namespace NGameEditor.Functionality.Controllers;



public class MenuController(
	IAboutWindow aboutWindow
) : IMenuController
{
	public ICommand OpenAboutWindow() =>
		ReactiveCommand.Create(aboutWindow.Open);


	public ICommand OpenDocumentation() =>
		ReactiveCommand.Create(() =>
			Process.Start(
				new ProcessStartInfo
				{
					FileName = "https://github.com/NGame-Engine/NGame/wiki",
					UseShellExecute = true
				}
			)
		);
}
