using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;

namespace NGameEditor.Avalonia.ProjectWindows.FileBrowsers;



public partial class FileBrowserView : UserControl, IView<FileBrowserViewModel>
{
	public FileBrowserView()
	{
		InitializeComponent();
	}
}

