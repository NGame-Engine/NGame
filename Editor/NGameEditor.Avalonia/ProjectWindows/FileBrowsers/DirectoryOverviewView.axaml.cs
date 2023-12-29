using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;

namespace NGameEditor.Avalonia.ProjectWindows.FileBrowsers;



public partial class DirectoryOverviewView : UserControl, IView<DirectoryOverviewViewModel>
{
	public DirectoryOverviewView()
	{
		InitializeComponent();
	}
}
