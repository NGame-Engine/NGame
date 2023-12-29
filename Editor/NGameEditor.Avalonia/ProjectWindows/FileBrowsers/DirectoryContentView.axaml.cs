using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;

namespace NGameEditor.Avalonia.ProjectWindows.FileBrowsers;



public partial class DirectoryContentView : UserControl, IView<DirectoryContentViewModel>
{
	public DirectoryContentView()
	{
		InitializeComponent();
	}
}
