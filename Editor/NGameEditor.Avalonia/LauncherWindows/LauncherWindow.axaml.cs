using Avalonia.Controls;
using NGameEditor.ViewModels.LauncherWindows;

namespace NGameEditor.Avalonia.LauncherWindows;



public partial class LauncherWindow : Window, IView<LauncherWindowViewModel>
{
	public LauncherWindow()
	{
		InitializeComponent();
	}
}
