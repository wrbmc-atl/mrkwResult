using System.Windows.Data;

namespace mrkwResult.Common
{
    public class BooleanToCharConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string s && s == "1")
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool b && b)
            {
                return "1";
            }
            return "0";
        }
    }
}