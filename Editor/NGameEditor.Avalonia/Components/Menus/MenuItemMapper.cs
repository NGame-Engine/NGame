using System.Linq;
using Avalonia.Controls;
using NGameEditor.ViewModels.Components.Menus;

namespace NGameEditor.Avalonia.Components.Menus;



public static class MenuItemMapper
{
	public static MenuItem Map(MenuEntryViewModel menuEntryViewModel) =>
		new()
		{
			Header = menuEntryViewModel.Header,
			Command = menuEntryViewModel.Command,
			ItemsSource =
				menuEntryViewModel
					.Children
					.Select(Map)
		};
}
