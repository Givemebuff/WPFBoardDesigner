using Board.Controls.BoardControl;
using Board.DesignerModel;

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
