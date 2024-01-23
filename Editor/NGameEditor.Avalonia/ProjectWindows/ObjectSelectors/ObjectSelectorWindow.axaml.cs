using Avalonia.Controls;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors;

namespace NGameEditor.Avalonia.ProjectWindows.ObjectSelectors;



public partial class ObjectSelectorWindow : Window, IView<ObjectSelectorViewModel>
{
	public ObjectSelectorWindow()
	{
		InitializeComponent();
	}
}
