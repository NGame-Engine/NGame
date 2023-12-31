using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows;

namespace NGameEditor.Avalonia.ProjectWindows;



public partial class ProjectWindow : Window, IView<ProjectWindowViewModel>
{
	public ProjectWindow()
	{
		InitializeComponent();
	}
}
