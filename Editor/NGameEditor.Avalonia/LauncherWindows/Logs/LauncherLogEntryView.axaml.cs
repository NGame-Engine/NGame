using Avalonia.Controls;
using NGameEditor.ViewModels.LauncherWindows.Logs;

namespace NGameEditor.Avalonia.LauncherWindows.Logs;



public partial class LauncherLogEntryView : UserControl, IView<LauncherLogEntryViewModel>
{
	public LauncherLogEntryView()
	{
		InitializeComponent();
	}
}
