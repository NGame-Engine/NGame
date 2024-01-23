using System.Windows.Input;

namespace NGameEditor.ViewModels.Components.CustomEditors;



public class SelectableObjectEditorViewModel(
	string objectName,
	ICommand openSelector
) : CustomEditorViewModel
{
	public string ObjectName { get; set; } = objectName;

	public ICommand OpenSelector { get; set; } = openSelector;
}
