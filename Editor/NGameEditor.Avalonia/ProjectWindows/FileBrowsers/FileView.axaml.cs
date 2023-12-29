using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;

namespace NGameEditor.Avalonia.ProjectWindows.FileBrowsers;



public partial class FileView : UserControl, IView<FileViewModel>
{
	public FileView()
	{
		InitializeComponent();
	}
}

