using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CustomConverter //namespace FirstFloor.ModernUI.App
{
    //[System.Windows.Data.ValueConversion(typeof(int), typeof(bool))]
    public class IVConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type t, object parameter, CultureInfo culture)
        {
            //return value.Equals(parameter);
            try
            {
                string _tvalue = "0";
                if (System.Convert.ToString(value) != "남자")
                    _tvalue = "1";

                return _tvalue.Equals(System.Convert.ToString(parameter));
            }
            catch
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type t, object parameter, CultureInfo culture)
        {
            try {
                //return value.Equals(false) ? DependencyProperty.UnsetValue : parameter;
                if (System.Convert.ToString(parameter) == "0")
                    return "남자";
                else
                    return "여자";
            }
            catch
            {
                return "남자";
            }
        }

    }
}
