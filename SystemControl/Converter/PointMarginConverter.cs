using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BoardDesigner.Converter
{
  
    public class PointMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Point d = (Point)value;
            Size sz = (Size)parameter;
            double x = d.X*sz.Width;
            double y = d.Y*sz.Height;
            return new Thickness(x,y , 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness t = (Thickness)value;
             Size sz = (Size)parameter;
             double x = t.Left / sz.Width;
             double y = t.Top / sz.Height;
            return new Point(x,y);
        }
    }
}
