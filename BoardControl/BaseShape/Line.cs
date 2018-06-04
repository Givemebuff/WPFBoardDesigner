using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BoardDesigner.BoardControl.BaseShape
{
    public class LineCandy:ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            cw.Protect(new Line() { X1 = 0, X2 = 100, Y1 = 0, Y2 = 100, StrokeThickness = 1, Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)) });
            return cw;
        }
        
    }
}
