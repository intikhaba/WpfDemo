using System;
using System.Windows.Data;

namespace WpfDemo.ValueConverters
{
    public class BooleanToStringConverter : IValueConverter
    {
        private const string YesText = "Yes";
        private const string NoText = "No";

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value == DBNull.Value)
            {
                return string.Empty;
            }

            return ((bool)value) ? YesText : NoText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (string)value == YesText;
        }
    }
}
