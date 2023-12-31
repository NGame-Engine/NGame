using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;

namespace NGameEditor.Avalonia.ProjectWindows.FileBrowsers;



public partial class DirectoryContentItemView : UserControl, IView<DirectoryContentItemViewModel>
{
	public DirectoryContentItemView()
	{
		InitializeComponent();
	}
}
