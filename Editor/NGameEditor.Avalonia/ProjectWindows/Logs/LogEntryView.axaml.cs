using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.Logs;

namespace NGameEditor.Avalonia.ProjectWindows.Logs;



public partial class LogEntryView : UserControl, IView<LogEntryViewModel>
{
	public LogEntryView()
	{
		InitializeComponent();
	}
}
