using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.HierarchyViews;

namespace NGameEditor.Avalonia.ProjectWindows.HierarchyViews;



public partial class HierarchyView : UserControl, IView<HierarchyViewModel>
{
	public HierarchyView()
	{
		InitializeComponent();
	}
}
