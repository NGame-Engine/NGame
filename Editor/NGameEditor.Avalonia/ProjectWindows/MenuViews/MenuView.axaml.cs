using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.MenuViews;

namespace NGameEditor.Avalonia.ProjectWindows.MenuViews;



public partial class MenuView : UserControl, IView<MenuViewModel>
{
	public MenuView()
	{
		InitializeComponent();
	}
}
