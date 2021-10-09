using System;
using System.Windows.Data;
using WpfDemo.ViewModels;

namespace WpfDemo.ValueConverters
{
    public class CustomerToDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is CustomerViewModel customer))
            {
                return string.Empty;
            }

            return $"{customer.FirstName} {customer.LastName}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
