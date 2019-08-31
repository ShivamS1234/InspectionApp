using System;
using System.Globalization;
using Xamarin.Forms;

namespace InspectionApp.Converter
{
    public class IntToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (parameter != null && parameter == "Decimal")
                {
                    if ((decimal)value == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return value;
                    }
                }
                else
                {
                    if ((int)value == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return value;
                    }
                }
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            //throw new NotImplementedException();
        }
    }
}
