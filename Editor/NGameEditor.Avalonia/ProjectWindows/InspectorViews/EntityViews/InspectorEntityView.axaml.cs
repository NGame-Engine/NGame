using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;

namespace NGameEditor.Avalonia.ProjectWindows.InspectorViews.EntityViews;



public partial class InspectorEntityView : UserControl, IView<InspectorEntityViewModel>
{
	public InspectorEntityView()
	{
		InitializeComponent();
	}
}
