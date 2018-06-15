using Board.Controls.BoardControl;
using Board.DesignerModel;
using Board.Factory;
using Board.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Board.BoardControl
{
    public class LabelCandy:ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            cw.Protect(new BoardLabel());
            return cw;
        }
        public UserControl GetCandy(DesignerControl control)
        {
            BoardLabel bf = new BoardLabel(control as DesignerLabel);
            return bf;
        }
    }
   
}
