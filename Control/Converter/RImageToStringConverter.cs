using BoardDesigner.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BoardDesigner.Converter
{
    public class RImageToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //string to RImage
            if (value == null)
                return null;
            if (value.ToString().IndexOf(":") != -1)//是绝对路径
            {
                return new RImage(new FileInfo(value.ToString()));
            }
            else
            {
                return new RImage(new FileInfo("Images\\"+value.ToString()));
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //RImage to String
            if (value == null)
                return null;
            return (value as RImage).ImageName;
        }
    }
}
