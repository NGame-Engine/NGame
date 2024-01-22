using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors;

namespace NGameEditor.Avalonia.ProjectWindows.ObjectSelectors;



public partial class ObjectView : UserControl, IView<ObjectViewModel>
{
	public ObjectView()
	{
		this.DoubleTapped += (_, _) => DoubleClick.Command?.Execute(null);
		InitializeComponent();
	}
}
