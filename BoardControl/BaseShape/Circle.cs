using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BoardDesigner.BoardControl.BaseShape
{
   public class CircleCandy:ICandy
    {
       public ControlWrapper GetCandy()
       {
           ControlWrapper cw = new ControlWrapper();

           cw.Protect(new Ellipse() { Width = 100, Height = 100, Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)), StrokeThickness=1 });
           return cw;
       }
    }
}
