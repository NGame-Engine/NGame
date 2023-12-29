using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace NGameEditor.Avalonia.Shared.Converters;



public class SystemDrawingColorConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (
			value is not System.Drawing.Color sourceColor ||
			targetType.IsAssignableTo(typeof(IBrush)) == false
		)
		{
			return new BindingNotification(new InvalidCastException(),
				BindingErrorType.Error);
		}

		var color = new Color(sourceColor.A, sourceColor.R, sourceColor.G, sourceColor.B);
		return new SolidColorBrush(color);
	}


	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
