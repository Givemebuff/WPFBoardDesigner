using BoardDesigner.Model;
using BoardDesigner.Properties;
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

namespace BoardDesigner.Converter
{
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) 
            {
                return DesignerBrushToBrushConverter.BitmapToBitmapImage(Resources.ImageDemo);
            }
            else 
            {
                string fileName = Directory.GetCurrentDirectory() + "\\Images\\" + value.ToString();             
                if (System.IO.File.Exists(fileName))
                {                 

                    return (ImageSource)(new BitmapImage(new Uri( fileName, UriKind.Absolute)));
                }
                else
                {
                    return DesignerBrushToBrushConverter.BitmapToBitmapImage(Resources.ImageDemo);
                }         
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {           
           return (value as BitmapImage).UriSource.AbsolutePath;
        }
    }
}
