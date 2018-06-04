using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BoardDesigner.Model.Data
{
    public static class ExtendToString
    {
      

        public static string ToXMLString(this GridLength length)
        {
            switch (length.GridUnitType)
            {
                case GridUnitType.Auto:
                    return "Auto";
                case GridUnitType.Pixel:
                    return length.Value.ToString();
                case GridUnitType.Star:
                    return length.Value + "*";
            }
            throw new Exception("未定义的GridLength当作了类型");
        }

        public static string ToXMLString(this DataGridLength length)
        {
            switch (length.UnitType)
            {
                case DataGridLengthUnitType.Auto:
                    return "Auto";
                case DataGridLengthUnitType.Pixel:
                    return length.Value.ToString();
                case DataGridLengthUnitType.Star:
                    return length.Value + "*";
                case DataGridLengthUnitType.SizeToCells:
                    return "SizeToCells";
                case DataGridLengthUnitType.SizeToHeader:
                    return "SizeToHeader";
            }
            throw new Exception("未定义的GridLength当作了类型");
        }

        public static string ToXMLString(this Thickness width)
        {
            if (width == null)
                return null;
            else if (width.Left == width.Right && width.Left == width.Top && width.Top == width.Bottom)
                return width.Left.ToString();
            else if (width.Left == width.Right && width.Left != width.Top && width.Top == width.Bottom)
            {
                return width.Left + "," + width.Top;
            }
            else
            {
                return width.Left + "," + width.Top + "," + width.Right + "," + width.Bottom;
            }
        }
    }
}
