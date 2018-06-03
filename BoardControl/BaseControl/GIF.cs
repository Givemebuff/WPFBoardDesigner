using BoardDesigner.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using XamlAnimatedGif;


namespace BoardDesigner.BoardControl.BaseControl
{
    public class GIFCandy :  ICandy
    {
        #region 依赖属性
        //#region 长宽
        //public double Width 
        //{
        //    get 
        //    {
        //       return  (double)this.GetValue(WidthProperty);
        //    }
        //    set 
        //    {
        //        this.SetValue(WidthProperty, value);

        //    }
        //}

        //public static readonly DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(double), typeof(GIFCandy), new PropertyMetadata(0, OnWidthPropertyChanged));

        //public static void OnWidthPropertyChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        //{
        //    d.SetValue(GIFCandy.WidthProperty, e.NewValue);
        //}

        //public double Height
        //{
        //    get
        //    {
        //        return (double)this.GetValue(HeightProperty);
        //    }
        //    set
        //    {
        //        this.SetValue(HeightProperty, value);

        //    }
        //}

        //public static readonly DependencyProperty HeightProperty = DependencyProperty.Register("Height", typeof(double), typeof(GIFCandy), new PropertyMetadata(0, OnHeightPropertyChanged));

        //public static void OnHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    d.SetValue(GIFCandy.HeightProperty, e.NewValue);
        //}
        //#endregion
        
        //#region 图片源
        //public Uri ImageSourece
        //{
        //    get { return (Uri)GetValue(ImageSoureceProperty); }
        //    set { SetValue(ImageSoureceProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for ImageSourece.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ImageSoureceProperty =
        //    DependencyProperty.Register("ImageSourece", typeof(Uri), typeof(GIFCandy), new PropertyMetadata(null,OnImageSourcePropertyChanged));

        //public static void OnImageSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    d.SetValue(GIFCandy.ImageSoureceProperty, e.NewValue);
        //}

        


        //#endregion 

        #endregion
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            Image gif = new Image();            
            
            string path = System.IO.Directory.GetCurrentDirectory() + "\\GifDemo.gif";
            if (System.IO.File.Exists(path))
                gif.SetValue(AnimationBehavior.SourceUriProperty, new Uri(path, UriKind.Absolute));
            gif.IsHitTestVisible = false;
            cw.Protect(gif);
            return cw;
        }

        public GIFCandy()
        {

        }

    }


}
