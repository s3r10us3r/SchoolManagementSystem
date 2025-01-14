using System.Globalization;
using System.Windows.Data;

namespace SchoolManagementSystem.AdminPanel.Resources
{
    public class DateOnlyToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateOnly dateOnly)
            {
                return dateOnly.ToString("yyyy-MM-dd");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && DateOnly.TryParse(str, out var dateOnly))
            {
                return dateOnly;
            }
            return null;
        }
    }
}
