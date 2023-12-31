using Avalonia.Controls;
using NGameEditor.ViewModels.Components.CustomEditors;

namespace NGameEditor.Avalonia.Components.CustomEditors;



public partial class UnrecognizedComponentEditorView : UserControl, IView<UnrecognizedComponentEditorViewModel>
{
	public UnrecognizedComponentEditorView()
	{
		InitializeComponent();
	}
}
