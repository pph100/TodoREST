using System;
using System.Globalization;
using Xamarin.Forms;

namespace TodoREST
{

    // [ValueConversion(typeof(DateTime), typeof(bool))]
    public class DateConverter : IValueConverter
    {
        // checks whether a given Date (value) is more than or equal X days (parameter) away from today's date
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            string valueString = value.ToString();
            CultureInfo MyCultureInfo = new CultureInfo("de-DE");
            int i = 0;
            DateTime checkDate = DateTime.Parse(valueString, MyCultureInfo);
            DateTime curDate = DateTime.Now;

            TimeSpan span = checkDate.Subtract(curDate);

            i = int.Parse(parameter.ToString());
            if ((int)span.Days >= i)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}