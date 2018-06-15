using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Board.Converter.Converter.BindConverter
{
    public class DynamicLabelFormatTextConverter : IMultiValueConverter
    {
        //1 Text 2 BindName 3 FormatString return FormatText
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(string.IsNullOrEmpty(values[0].ToString()))
                values[0] ="";
            if(string.IsNullOrEmpty(values[1].ToString()))
                values[1] ="Value";
            string para = "[#" + values[1].ToString() + "]";
            if (string.IsNullOrEmpty(values[2].ToString()))
                values[2] = para;
            return (values[2].ToString()).Replace(para, values[0].ToString());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
