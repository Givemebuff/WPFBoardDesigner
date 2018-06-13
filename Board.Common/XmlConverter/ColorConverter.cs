using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Board.Converter.XmlConverter
{
    public class ColorConverter
    {
        public Brush XmlToBrush(string color) 
        {
            if (string.IsNullOrEmpty(color))
                return new SolidColorBrush(Color.FromRgb(255, 255, 255));
            
            System.Drawing.Color c = System.Drawing.ColorTranslator.FromHtml(color);
            return new SolidColorBrush(Color.FromArgb(c.A,c.R,c.G,c.B));
        }
    }
}
