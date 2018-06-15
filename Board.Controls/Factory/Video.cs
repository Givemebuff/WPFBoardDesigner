

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
    public class VideoCandy : ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();        
            BoardMediaPlayer p = new BoardMediaPlayer();

            cw.Protect(p);
            return cw;
        }
        public UserControl GetCandy(DesignerControl control)
        {
            BoardMediaPlayer bf = new BoardMediaPlayer(control as DesignerMedia);
            return bf;
        }
    }
}
