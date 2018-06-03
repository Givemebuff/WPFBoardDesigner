using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BoardDesigner.BoardControl.BaseShape
{
    public class SpLineCandy : ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            Path path = new Path();
            path.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            path.StrokeThickness = 1;
            List<PathSegment> pslist= new List<PathSegment>();
            BezierSegment ps = new BezierSegment(new Point(50,50),new Point(70,50),new Point(100,0),true);
            pslist.Add(ps);
            PathFigure pf = new PathFigure(new Point(0,0),pslist,false);
            List<PathFigure> pflist = new List<PathFigure>();
            pflist.Add(pf);
            PathGeometry fg = new PathGeometry(pflist);

            path.Data = fg;

            cw.Protect(path);
            return cw;
        }

    }
}
