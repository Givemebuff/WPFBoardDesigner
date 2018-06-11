using Board.Controls.BoardControl;
using Board.DesignerModel;
using BoardDesigner.DemoData;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visifire.Charts;

namespace BoardDesigner.BoardControl.ChartControl
{
    public class SpLineChartCandy:ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            DesignerChart dc = new DesignerChart();
            dc.Series[0].RenderAs = RenderAs.Spline;
            dc.ChartAxesX[0].AxisMaximum = 12;
            dc.ChartAxesX[0].AxisMinimum = 1;
            dc.ChartAxesY[0].AxisMaximum = 1000;
            dc.ChartAxesY[0].AxisMinimum = 500;
            dc.ChartAxesX[0].Interval = 1;
            dc.ChartAxesY[0].Interval = 100;
            dc.ChartTitles[0].Text = "2015年销售额变化趋势";
            List<DesignerDataPoint> dps = new List<DesignerDataPoint>();          
            DataTable data = DemoDataTableData.Turnover;
            for(int i=0;i<12;i++)
            {
                DesignerDataPoint dp = new DesignerDataPoint();
                dp.XValue = data.Rows[i]["Month"];
                dp.YValue = Convert.ToDouble(data.Rows[i]["Turnover"]);
                dps.Add(dp);
            }

            dc.Series[0].DataPoints = new DesignerDataPointCollection(dps, dc.Series[0]);
            BoardChart chart = new BoardChart(dc);
            cw.Protect(chart);
            
            return cw;
        }
    }
}
