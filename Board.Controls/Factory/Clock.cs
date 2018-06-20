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
    public class ClockCandy : ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            DesignerClock clock = new DesignerClock();
            clock.Size.Width = 200;
            clock.Size.Height = 50;

            BoardClock bf = new BoardClock(clock);
            cw.Protect(bf);
            return cw;
        }


        public UserControl GetCandy(DesignerControl control)
        {
            BoardClock bf = new BoardClock(control as DesignerClock);
            return bf;
        }
    }
}
