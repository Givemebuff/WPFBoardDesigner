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
    public class LineChartCandy : ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            Chart chart = new Chart();
            Title title = new Title();
            title.Text = "图表标题";
            Axis x = new Axis();
            Axis y = new Axis();
            chart.AxesX.Add(x);
            chart.AxesY.Add(y);

            y.Title = "Y轴";
            x.Title = "X轴";

            DataSeries serie = new DataSeries();
            foreach (DataRow r in DemoDataTableData.DataTableData1.Rows)
            {
                DataPoint point = new DataPoint();
                point.XValue = r["X"];
                point.YValue = Convert.ToDouble(r["Y"]);
                serie.DataPoints.Add(point);
            }

            serie.RenderAs = RenderAs.Line;
            chart.Series.Add(serie);
           
            cw.Protect(chart);
            chart.IsHitTestVisible = false;
            return cw;
        }
    }
}
