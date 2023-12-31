using Avalonia.Controls;
using NGameEditor.ViewModels.Components.CustomEditors;

namespace NGameEditor.Avalonia.Components.CustomEditors;



public partial class CheckBoxEditorView : UserControl, IView<CheckBoxEditorViewModel>
{
	public CheckBoxEditorView()
	{
		InitializeComponent();
	}
}
