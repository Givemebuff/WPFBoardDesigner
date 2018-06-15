using Board.Controls.BoardControl;
using Board.DesignerModel;
using Board.Factory;
using Board.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Board.BoardControl
{
    public class TableCandy:ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            DesignerTable tb = new DesignerTable();
            tb.Size.Width = 400;
            tb.Size.Height = 300;
            BoardDataGrid bf = new BoardDataGrid(tb);
            cw.Protect(bf);
            return cw;
        }
        public UserControl GetCandy(DesignerControl control)
        {
            BoardDataGrid bf = new BoardDataGrid(control as DesignerTable);
            return bf;
        }
    }
}
