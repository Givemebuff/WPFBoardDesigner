using BoardDesigner.DemoData;
using BoardDesigner.UControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BoardDesigner.BoardControl.ChartControl
{
    public class TableCandy:ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            BoardDataGrid table = new BoardDataGrid();            
          
            cw.Protect(table);
            return cw;
        }
    }
}
