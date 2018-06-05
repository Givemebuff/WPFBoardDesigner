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
    public class ColumnChartCandy:ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            BoardChart chart = new BoardChart();         
         
            return cw;
        }
    }
}
