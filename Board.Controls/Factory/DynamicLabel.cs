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
    public class DynamicLabelCandy : ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            DesignerDynamicLabel dg = new DesignerDynamicLabel();
            dg.Size.Width = 150;
            dg.Size.Height = 50;
            dg.Text = "动态文本";            
            BoardDynamicLabel bf = new BoardDynamicLabel(dg);
            cw.Protect(bf);
            return cw;
        }
        public UserControl GetCandy(DesignerControl control)
        {
            BoardDynamicLabel bf = new BoardDynamicLabel(control as DesignerDynamicLabel);
            return bf;
        }
    }
}
