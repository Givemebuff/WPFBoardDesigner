using BoardDesigner.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BoardDesigner.BoardControl.BaseControl
{
    public class LabelCandy:ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            cw.Protect(new BoardLabel());
            return cw;
        }
    }
   
}
