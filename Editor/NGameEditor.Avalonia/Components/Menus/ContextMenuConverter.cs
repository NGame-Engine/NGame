using System;
using System.Globalization;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using NGameEditor.ViewModels.Components.Menus;

namespace NGameEditor.Avalonia.Components.Menus;



public class ContextMenuConverter : IValueConverter
{
	public object Convert(
		object? value,
		Type targetType,
		object? parameter,
		CultureInfo culture
	)
	{
		if (value is not ContextMenuViewModel contextMenuViewModel ||
			targetType.IsAssignableTo(typeof(ContextMenu)) == false)
		{
			return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
		}


		return
			new ContextMenu
			{
				ItemsSource =
					contextMenuViewModel
						.Children
						.Select(MenuItemMapper.Map)
			};
	}


	public object ConvertBack(
		object? value,
		Type targetType,
		object? parameter,
		CultureInfo culture
	)
	{
		throw new NotSupportedException();
	}
}
