using System.Windows.Input;

namespace NGameEditor.ViewModels.ProjectWindows.MenuViews;



public class MenuViewModel : ViewModelBase
{
	public ICommand? SaveScene { get; set; }
	public ICommand? OpenAboutWindow { get; set; }
	public ICommand? OpenDocumentation { get; set; }
}
