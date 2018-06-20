using Board.DesignerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Board.XmlConverter
{
    public class ColorConverter
    {
        public static Brush XmlToBrush(string color) 
        {
            if (string.IsNullOrEmpty(color))
                return new SolidColorBrush(Color.FromRgb(255, 255, 255));
         
            if (color.Length > 9) 
            {
                //1,#12345678,0.0,#123456789,1.0
                string type = color.Substring(0, 1);
                int tp = Convert.ToInt32(type);
                if (tp == 1)//RadialGradientBrush 径向渐变
                {
                    string content = color.Substring(2, color.Length - 2);
                    string[] ss = content.Split(',');
                    
                    GradientStopCollection ngsc = new GradientStopCollection();
                    for (int i = 0; i < ss.Length; i+=2) 
                    {
                        System.Drawing.Color c = System.Drawing.ColorTranslator.FromHtml(ss[i]);
                        GradientStop ngs = new GradientStop();
                        ngs.Color = Color.FromArgb(c.A, c.R, c.G, c.B);
                        ngs.Offset = Convert.ToDouble(ss[i + 1]);
                        ngsc.Add(ngs);
                    }
                    return new RadialGradientBrush(ngsc);
                }
                else if (tp == 0)//LinearGradientBrush//线性渐变//0,0,0,1,1,#12345678,0.0,#123456789,1.0
                {
                    string content = color.Substring(2, color.Length - 2);
                    string[] ss = content.Split(',');

                    Point startPoint = new Point(Convert.ToDouble(ss[0]), Convert.ToDouble(ss[1]));
                    Point endPoint = new Point(Convert.ToDouble(ss[2]), Convert.ToDouble(ss[3]));
                    GradientStopCollection ngsc = new GradientStopCollection();
                    for (int i = 4; i < ss.Length; i += 2)//0,0,1,1,#12345678,0.0,#123456789,1.0
                    {                      
                        System.Drawing.Color c = System.Drawing.ColorTranslator.FromHtml(ss[i]);
                        GradientStop ngs = new GradientStop();
                        ngs.Color = Color.FromArgb(c.A, c.R, c.G, c.B);
                        ngs.Offset = Convert.ToDouble(ss[i + 1]);
                        ngsc.Add(ngs);
                    }
                    return new LinearGradientBrush(ngsc, startPoint, endPoint);
                }
                else return null;
            }
            else    //#12345678
            {
                System.Drawing.Color c = System.Drawing.ColorTranslator.FromHtml(color);
                return new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B));
            }
           
        }

        public static string BrushToString(Brush brush)
        {
             if (brush is SolidColorBrush)
                return brush.ToString();            
            else
            {
                if (brush is LinearGradientBrush)
                {
                    string result = "0,";
                    Point startPoint = (brush as LinearGradientBrush).StartPoint;
                    result += (startPoint.X + "," + startPoint.Y + ",");
                    Point endPoint = (brush as LinearGradientBrush).EndPoint;
                    result += (endPoint.X + "," + endPoint.Y);
                    foreach (GradientStop s in (brush as LinearGradientBrush).GradientStops)
                    {
                        result += ("," + s.Color.ToString() + "," + s.Offset.ToString());
                    }
                    return result;
                }
                else if (brush is RadialGradientBrush)
                {
                    string result = "1";
                    foreach (GradientStop s in (brush as RadialGradientBrush).GradientStops)
                    {
                        result += ("," + s.Color.ToString() + "," + s.Offset.ToString());
                    }
                    return result;
                }
                else
                    throw new NotImplementedException();
            }
        }
    }
}
