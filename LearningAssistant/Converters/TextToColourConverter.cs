using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LearningAssistant.Converters
{
    public class TextToColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                switch (value.ToString().ToLower())
                {
                    case "active":
                        return new SolidColorBrush(Colors.Lime);

                    case "inactive":
                        return new SolidColorBrush(Colors.Red);

                    default:
                        return new SolidColorBrush(Colors.Black);

                }
            }
            else
                return new SolidColorBrush(Colors.Black);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

