using Avalonia.Controls;
using NGameEditor.ViewModels.LauncherWindows.HistoryViews;

namespace NGameEditor.Avalonia.LauncherWindows.ProjectHistoryViews;



public partial class HistoryEntryView : UserControl, IView<HistoryEntryViewModel>
{
	public HistoryEntryView()
	{
		InitializeComponent();
	}
}
