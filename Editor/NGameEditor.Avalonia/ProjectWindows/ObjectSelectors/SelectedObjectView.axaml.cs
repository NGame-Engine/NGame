using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors;

namespace NGameEditor.Avalonia.ProjectWindows.ObjectSelectors;



public partial class SelectedObjectView : UserControl, IView<SelectedObjectViewModel>
{
	public SelectedObjectView()
	{
		InitializeComponent();
	}
}
