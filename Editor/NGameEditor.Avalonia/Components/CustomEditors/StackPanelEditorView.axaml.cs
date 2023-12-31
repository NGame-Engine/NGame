using Avalonia.Controls;
using NGameEditor.ViewModels.Components.CustomEditors;

namespace NGameEditor.Avalonia.Components.CustomEditors;



public partial class StackPanelEditorView : UserControl, IView<StackPanelEditorViewModel>
{
	public StackPanelEditorView()
	{
		InitializeComponent();
	}
}
