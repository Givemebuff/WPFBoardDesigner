using BoardDesigner.Model;
using BoardDesigner.UControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BoardDesigner.BoardControl.BaseControl
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
    }
}
