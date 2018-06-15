
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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Board.BoardControl
{
    public class ImageCandy : ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            BoardImage image = new BoardImage();
            cw.Protect(image);
            return cw;
        }

        public UserControl GetCandy(DesignerControl control)
        {
            BoardImage bf = new BoardImage(control as DesignerImage);
            return bf;
        }       
    }
}
