using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;

namespace NGameEditor.Avalonia.ProjectWindows.InspectorViews;



public partial class InspectorView : UserControl, IView<InspectorViewModel>
{
	public InspectorView()
	{
		InitializeComponent();
	}
}
