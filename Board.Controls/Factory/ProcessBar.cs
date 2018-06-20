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
    public class ProcessBar:ICandy
    {  public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            DesignerProcessbar pb = new DesignerProcessbar();
            pb.Size.Width = 200;
            pb.Size.Height = 50;
            BoardProcessbar bf = new BoardProcessbar(pb);
            cw.Protect(bf);
            return cw;
        }


        public UserControl GetCandy(DesignerControl control)
        {
            BoardProcessbar bf = new BoardProcessbar(control as DesignerProcessbar);
            return bf;
        }
    }
   
}
