using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Board.XmlConverter
{
    public class ThicknessConverter
    {
        public static Thickness StringToThickness(string thickness)
        {
            if (string.IsNullOrEmpty(thickness))
                thickness = "0";
            string[] ss = thickness.Split(',');
            if(ss.Length ==1)
            {
                return new Thickness(Convert.ToDouble(ss[0]));
            }
            else if (ss.Length == 2) 
            {
                return new Thickness(Convert.ToDouble(ss[0]), Convert.ToDouble(ss[1]), Convert.ToDouble(ss[0]), Convert.ToDouble(ss[1]));
            }
            else if (ss.Length == 4) 
            {
                return new Thickness(Convert.ToDouble(ss[0]), Convert.ToDouble(ss[1]), Convert.ToDouble(ss[2]), Convert.ToDouble(ss[3]));
            }
            throw new Exception("不正确的XML:" + thickness);
        }
    }
}
