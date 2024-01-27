using System.Windows.Input;

namespace NGameEditor.ViewModels.Components.Menus;



public class MenuEntryViewModel(
	string header,
	ICommand? command,
	IEnumerable<MenuEntryViewModel> children
)
	: ViewModelBase
{
	public MenuEntryViewModel(
		string header,
		IEnumerable<MenuEntryViewModel> children
	) : this(header, null, children)
	{
	}


	public MenuEntryViewModel(
		string header,
		ICommand command
	) : this(header, command, [])
	{
	}


	public string Header { get; } = header;
	public ICommand? Command { get; } = command;
	public List<MenuEntryViewModel> Children { get; } = [.. children];
}
