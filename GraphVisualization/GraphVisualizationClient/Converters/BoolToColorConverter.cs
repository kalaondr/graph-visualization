using System;
using System.Windows.Data;
using System.Windows.Media;

namespace GraphVisualizationClient.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var bValue = value as bool?;
            return new SolidColorBrush(bValue.GetValueOrDefault() ? Colors.Red : Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}