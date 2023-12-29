using System.Windows.Input;

namespace NGameEditor.ViewModels.Controllers;



public interface IMenuController
{
	ICommand OpenAboutWindow();
	ICommand OpenDocumentation();
}
