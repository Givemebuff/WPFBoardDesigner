using Board.Controls.BoardControl;
using Board.DesignerModel;
using Board.Factory;
using Board.Interface;
using System.Windows.Controls;



namespace Board.BoardControl
{
    public class GIFCandy :  ICandy
    {   
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            DesignerGif dg = new DesignerGif();
            dg.Size.Width = 200;
            dg.Size.Height = 200;
            dg.GIFImageSource = "GifDemo.gif";
            dg.AutoPlay = true;

            BoardGif bf = new BoardGif(dg);
            cw.Protect(bf);
            return cw;
        }
        public UserControl GetCandy(DesignerControl control)
        {
            BoardGif bf = new BoardGif(control as DesignerGif);
            return bf;
        }
    }


}
