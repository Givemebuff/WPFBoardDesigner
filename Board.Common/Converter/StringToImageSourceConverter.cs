using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Board.Converter
{
    //文件名 TO ImageSource
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //TODO
            if (value == null)
                return null;
            string fileName = value.ToString();
            string path = Directory.GetCurrentDirectory() + "\\Images\\" + fileName;
            if (File.Exists(path))
            {
                return new BitmapImage(new Uri(path, UriKind.Absolute));
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {           
           return (value as BitmapImage).UriSource.AbsolutePath;
        }
    }
}
