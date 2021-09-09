using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace ExamCalculator.UI.Converters
{
    public class Percent : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case float p: return p.ToString("P1");
                default: throw new NotSupportedException("Percent formatting only works for floats");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}