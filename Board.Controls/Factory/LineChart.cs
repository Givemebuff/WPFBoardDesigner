using Board.Controls.BoardControl;
using Board.DesignerModel;
using Board.Factory;
using Board.Interface;
using System.Windows.Controls;
using Visifire.Charts;

namespace Board.BoardControl
{
    public class LineChartCandy : ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            DesignerChart dc = new DesignerChart();            
            BoardChart chart = new BoardChart(dc);
            cw.Protect(chart);
            return cw;
        }
        public UserControl GetCandy(DesignerControl control)
        {
            BoardChart chart = new BoardChart(control as DesignerChart);
            return chart;
        }
    }
}
