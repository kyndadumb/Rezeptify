﻿using System.Globalization;

namespace Rezeptify;

public class BarcodeEventArgsConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var val = (ZXing.Net.Maui.BarcodeDetectionEventArgs)value;
        if (val == null) return null;
        return val.Results[0].Value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
