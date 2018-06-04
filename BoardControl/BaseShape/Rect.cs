using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BoardDesigner.BoardControl.BaseShape
{
    public class RectCandy : ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();

            cw.Protect(new Rectangle() { Width = 80, Height = 60, Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)), StrokeThickness = 1 });
            return cw;
        }
    }
}
