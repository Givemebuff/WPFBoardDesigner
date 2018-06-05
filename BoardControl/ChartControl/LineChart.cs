using BoardDesigner.DemoData;
using BoardDesigner.Model;
using BoardDesigner.UControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visifire.Charts;

namespace BoardDesigner.BoardControl.ChartControl
{
    public class LineChartCandy : ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            DesignerChart dc = new DesignerChart();
            dc.Series[0].RenderAs = RenderAs.Line;
            BoardChart chart = new BoardChart(dc);
            cw.Protect(chart);
            return cw;
        }
    }
}
