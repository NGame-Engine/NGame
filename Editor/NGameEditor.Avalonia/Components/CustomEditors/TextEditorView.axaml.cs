using Avalonia.Controls;
using NGameEditor.ViewModels.Components.CustomEditors;

namespace NGameEditor.Avalonia.Components.CustomEditors;



public partial class TextEditorView : UserControl, IView<TextEditorViewModel>
{
	public TextEditorView()
	{
		InitializeComponent();
	}
}
