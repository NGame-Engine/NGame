using Avalonia.Controls;
using NGameEditor.ViewModels.LauncherWindows.Logs;

namespace NGameEditor.Avalonia.LauncherWindows.Logs;



public partial class LauncherLogView : UserControl, IView<LauncherLogViewModel>
{
	public LauncherLogView()
	{
		InitializeComponent();
	}
}
