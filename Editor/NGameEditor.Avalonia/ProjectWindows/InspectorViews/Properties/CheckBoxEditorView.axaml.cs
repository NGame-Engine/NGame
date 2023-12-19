using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews.Properties;

namespace NGameEditor.Avalonia.ProjectWindows.InspectorViews.Properties;



public partial class CheckBoxEditorView : ReactiveUserControl<CheckBoxEditorViewModel>
{
	public CheckBoxEditorView()
	{
		InitializeComponent();
	}
}
