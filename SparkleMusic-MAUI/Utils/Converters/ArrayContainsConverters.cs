using System.Diagnostics;
using System.Globalization;

namespace SparkleMusic_MAUI.Utils.Converters;

public class ArrayContainsConverters : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Debug.WriteLine("Coming Here");
        if (value is not null && parameter is not null && value is Array array)
        {
            Debug.WriteLine("Not here");
            return array.Cast<object>().Contains(parameter);
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}