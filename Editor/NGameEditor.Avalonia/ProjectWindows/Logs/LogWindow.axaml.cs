using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.Logs;

namespace NGameEditor.Avalonia.ProjectWindows.Logs;



public partial class LogWindow : Window, IView<LogWindowModel>
{
	public LogWindow()
	{
		InitializeComponent();
	}
}
