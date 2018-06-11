﻿using Board.Controls.BoardControl;
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
    public class PieChartCandy:ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            DesignerChart dc = new DesignerChart();
            dc.Series[0].RenderAs = RenderAs.Pie;
            BoardChart chart = new BoardChart(dc);
            cw.Protect(chart);
            return cw;
        }
    }
}
