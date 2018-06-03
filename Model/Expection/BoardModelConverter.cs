using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BoardDesigner.Model.Data
{
    public class BoardModelConverter
    {
        #region 颜色
        public static Color ConvertToColorFromHtmlFormat(string color)
        {
            //color :#FFFF008B
            System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(color);
            return Color.FromArgb(col.A,col.R, col.G, col.B);
        }

        public static string ConvertToHtmlFotmatFromColor(Color color) 
        {
            System.Drawing.Color col = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            int a = Convert.ToInt32(col.A); 
            int b = Convert.ToInt32(col.B); 
            int r = Convert.ToInt32(col.R); 
            int g = Convert.ToInt32(col.G);
            return "#" + String.Format("{0:X2}", col.A)+String.Format("{0:X2}", col.R)+String.Format("{0:X2}", col.G)+String.Format("{0:X2}", col.B);  
        }

        public static bool TryPraseColor(string str, out Color color) 
        {
            color = default(Color);

            string pattern = @"^#([0-9a-fA-F]{8}|[0-9a-fA-F]{6})$";
            if (Regex.IsMatch(str, pattern))
            {
                color = ConvertToColorFromHtmlFormat(str);
                return true;
            }
            else return false;
        }

        #endregion

        public static bool ConvertToDouble(string db, out double value)
        {
            value = 0;
            //color :XX.XXX%
            if (db.IndexOf("%") == 0)
                return false;
            if (db.Split('%').Length > 2)
                return false;
            if (db.IndexOf("%") != db.Length - 1)
                return false;
            if (double.TryParse(db.Replace("%", ""), out value))
                return true;
            else return false;
        }

        public static double GetActuallyWidth(object parentWidth, string xmlWidth, double calcWidth)
        {
            double d = Convert.ToDouble(parentWidth);

            if (xmlWidth == calcWidth.ToString())
                return calcWidth;
            else
            {
                return d * calcWidth;
            }
        }

        public static bool ConvertToGridLength(string value, out GridLength width)
        {
            double v = 0;
            width = new GridLength(1, GridUnitType.Auto);
            if (value.ToLower() == "auto")
                return true;
            else if (value.IndexOf('*') != -1)
            {
                if (!double.TryParse(value.Replace("*", ""), out v))
                    return false;
                else
                    width = new GridLength(v, GridUnitType.Pixel);
            }
            else if (double.TryParse(value, out v))
                width = new GridLength(v, GridUnitType.Star);
            else
                return false;
            return true;
        }

        public static DataGridLength ConvertToDataGridLength(GridLength width)
        {
            double w = width.Value;
            switch (width.GridUnitType)
            {
                case GridUnitType.Pixel:
                    return new DataGridLength(w, DataGridLengthUnitType.Pixel);
                case GridUnitType.Star:
                    return new DataGridLength(w, DataGridLengthUnitType.Star);
                default:
                    return new DataGridLength(0, DataGridLengthUnitType.Auto);
            }
        }

        public static bool ConvertToThickness(string value, out Thickness t)
        {
            t = new Thickness();
            double v = 0;
            if (value.Split(',').Length == 4)
            {
                string[] ss = value.Split(',');

                if (!double.TryParse(ss[0], out v))
                {
                    return false;
                }
                t.Left = v;
                if (!double.TryParse(ss[1], out v))
                {
                    return false;
                }
                t.Top = v;
                if (!double.TryParse(ss[2], out v))
                {
                    return false;
                }
                t.Right = v;
                if (!double.TryParse(ss[3], out v))
                {
                    return false;
                }
                t.Bottom = v;
            }
            else if (value.Split(',').Length == 2)
            {
                string[] ss = value.Split(',');
                if (!double.TryParse(ss[0], out v))
                {
                    return false;
                }
                t.Left = v;
                t.Right = v;
                if (!double.TryParse(ss[1], out v))
                {
                    return false;
                }
                t.Top = v;
                t.Bottom = v;
            }
            else if (double.TryParse(value, out v))
            {
                t = new Thickness(v);
            }
            else return false;
            return true;
        }

        public static DesignerBrush ConvertStringToBrush(string brushString) 
        {
            if (string.IsNullOrEmpty(brushString))
                return null;
            Color color = default(Color);
            //if (TryPraseColor(brushString, out color)) 
            //{
            //    return new SolidColorBrush(color);
            //}
            //else if (System.IO.File.Exists(brushString)) 
            //{
            //    //TODO 相对路径绝对路径还能再分
            //    return new ImageBrush(new BitmapImage(new Uri(brushString, UriKind.Absolute)));
            //}

            throw new Exception("未定义的字符串转画刷结构");
        }
    }
}
