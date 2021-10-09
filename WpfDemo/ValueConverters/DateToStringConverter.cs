using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfDemo.ValueConverters
{
    public class DateToStringConverter : IValueConverter
    {
        private const string Culture = "en-GB";

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value == DBNull.Value)
            {
                return string.Empty;
            }

            return ((DateTime)value).ToString("dd/MM/yyyy");

            //bool isParsed = DateTime.TryParse(value.ToString(), new CultureInfo(Culture), DateTimeStyles.None,out DateTime date);

            //if (!isParsed)
            //{
            //    return string.Empty;
            //}

            //return $"{customer.FirstName} {customer.LastName}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DateTime.Parse(value.ToString(), new CultureInfo(Culture));
        }
    }
}
