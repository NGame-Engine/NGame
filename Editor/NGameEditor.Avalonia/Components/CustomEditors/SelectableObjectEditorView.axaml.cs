using Avalonia.Controls;
using NGameEditor.ViewModels.Components.CustomEditors;

namespace NGameEditor.Avalonia.Components.CustomEditors;



public partial class SelectableObjectEditorView : UserControl, IView<SelectableObjectEditorViewModel>
{
	public SelectableObjectEditorView()
	{
		InitializeComponent();
	}
}
