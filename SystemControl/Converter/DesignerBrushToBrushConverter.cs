using BoardDesigner.Model;
using BoardDesigner.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BoardDesigner.Converter
{
    public class DesignerBrushToBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
       {
            //若存在图片资源，优先选用图片资源
           if (values[0] != null)
           {
               //若图存在，选用这张图
               if (!string.IsNullOrEmpty(values[0].ToString()))
               {
                   if (System.IO.File.Exists(values[0].ToString()))
                       return new ImageBrush(new BitmapImage(new Uri(values[0].ToString(), UriKind.RelativeOrAbsolute)));
               }    
           }
            //若不存在，验证画笔资源是否可用
           if (values[1] != null) 
           {
               if (values[1] is SolidColorBrush)
                   return (SolidColorBrush)values[1];
               else if (values[1] is GradientBrush)
                   return (GradientBrush)values[1];
           }

            //均无效则使用默认白色背景
           return new SolidColorBrush(Color.FromRgb(255, 255, 255));
       }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] brush = new object[2];
            if (value is ImageBrush)
            {
                ImageBrush ib = (ImageBrush)value;
                brush[0] = ib.ToString();
            }
            else if (value is SolidColorBrush)
            {
                SolidColorBrush ib = (SolidColorBrush)value;
                brush[1] = "颜色";//ib.Color.ToString();
            }
            else if (value is GradientBrush) 
            {
                GradientBrush ib = (GradientBrush)value;
                brush[1] = "渐变色";//ib.Color.ToString();
            }
            else
                return null;
            return brush;
        }
        public static BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }
    }


}
