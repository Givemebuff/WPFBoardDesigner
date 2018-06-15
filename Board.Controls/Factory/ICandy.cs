using Board.DesignerModel;
using Board.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;


namespace Board.Interface
{
    public interface ICandy
    {
        ControlWrapper GetCandy();
        UserControl GetCandy(DesignerControl control);
    }
}
