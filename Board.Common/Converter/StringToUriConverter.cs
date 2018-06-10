using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Board.Converter
{
    public class StringToUriConverterForImage : IValueConverter
    {
        //String to Uri
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\Images\\"+value.ToString();
            return new Uri(path, UriKind.Absolute);

        }

        //Uri TO string
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StringToUriConverterForMedia : IValueConverter
    {
        //String to Uri
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\Medias\\" + value.ToString();
            return new Uri(path, UriKind.Absolute);

        }

        //Uri TO string
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
