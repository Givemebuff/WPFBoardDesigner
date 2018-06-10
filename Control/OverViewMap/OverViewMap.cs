using BoardDesigner.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace BoardDesigner.Base
{
    public class OverViewMap : Control
    {
        private Thumb zoomThumb;
        private Canvas zoomCanvas;
        private Grid scrollViewGrid;
        private Slider zoomSlider;
        private ScaleTransform scaleTransform;
        private Canvas designerCanvas;

        #region 小地图画布



        public Canvas ZoomCanvas
        {
            get { return (Canvas)GetValue(ZoomCanvasProperty); }
            set { SetValue(ZoomCanvasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ZoomCanvas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZoomCanvasProperty =
            DependencyProperty.Register("ZoomCanvas", typeof(Canvas), typeof(OverViewMap), new PropertyMetadata(null));

        #endregion

        #region 滚动条
        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(OverViewMap));

        #endregion

        #region 缩放倍数

        //操作缩放
        public double Rate
        {
            get { return (double)GetValue(RateProperty); }
            set { SetValue(RateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RateProperty =
            DependencyProperty.Register("Rate", typeof(double), typeof(OverViewMap), new PropertyMetadata(1.0, RatePropertyChanged));

        private static void RatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            double r = (double)e.NewValue / (double)e.OldValue;
            OverViewMap map = d as OverViewMap;
            map.scaleTransform.ScaleX *= r;
            map.scaleTransform.ScaleY *= r;
        }

        #region 显示上的倍数

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(double), typeof(OverViewMap), new PropertyMetadata(double.NaN, ScalePropertyChanged));

        private static void ScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //重新计算大小并显示设计视图
           
        }

        #endregion

        #endregion

        public OverViewMap()
        {
            //MultiBinding scaleMB = new MultiBinding();
            //Binding wb = new Binding("ScrollViewer.ActualWidth") { Source = this };
            //Binding vwb = new Binding("ScrollViewer.ViewportWidth") { Source = this };
            //scaleMB.Converter = (IMultiValueConverter)(new ScrollScaleConverter());
            //this.SetBinding(ScaleProperty, scaleMB);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.ScrollViewer == null)
                return;

            this.scrollViewGrid = this.ScrollViewer.Content as Grid;
            if (this.scrollViewGrid == null)
                throw new Exception("ContentGrid Not Founded!");

            this.designerCanvas = this.scrollViewGrid.FindName("DesignerPanel") as Canvas;
            if (this.designerCanvas == null)
                throw new Exception("DesignerPanel must not be null!");
            else
            {
                 this.designerCanvas.LayoutUpdated += new EventHandler(this.DesignerCanvas_LayoutUpdated);
            }

            this.zoomThumb = Template.FindName("ViewMoveThumb", this) as Thumb;
            if (this.zoomThumb == null)
                throw new Exception("ViewMoveThumb template is missing!");
            else 
            { 
                this.zoomThumb.DragDelta += new DragDeltaEventHandler(this.Thumb_DragDelta);
                //zoomThumb.SetBinding(WidthProperty,new Binding("")
            }

            this.zoomCanvas = Template.FindName("PART_Map", this) as Canvas;
            if (this.zoomCanvas == null)
                throw new Exception("PART_Map template is missing!");

            this.zoomSlider = Template.FindName("PART_Slider", this) as Slider;
            if (this.zoomSlider == null)
                throw new Exception("PART_Slider template is missing!");
            else
            {
                this.zoomSlider.SetBinding(Slider.ValueProperty,new Binding("Rate"){Source= this});
            }            

            this.scaleTransform = new ScaleTransform();
            this.scrollViewGrid.LayoutTransform = this.scaleTransform;
        }

        private void DesignerCanvas_LayoutUpdated(object sender, EventArgs e)
        {
            //布局更新
            double scale, xOffset, yOffset;
            this.InvalidateScale(out scale, out xOffset, out yOffset);

            this.zoomThumb.Width = this.ScrollViewer.ViewportWidth * scale;
            this.zoomThumb.Height = this.ScrollViewer.ViewportHeight * scale;

            Canvas.SetLeft(this.zoomThumb, xOffset + this.ScrollViewer.HorizontalOffset * scale);
            Canvas.SetTop(this.zoomThumb, yOffset + this.ScrollViewer.VerticalOffset * scale);
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double scale, xOffset, yOffset;
            this.InvalidateScale(out scale, out xOffset, out yOffset);

            this.ScrollViewer.ScrollToHorizontalOffset(this.ScrollViewer.HorizontalOffset + e.HorizontalChange / scale);
            this.ScrollViewer.ScrollToVerticalOffset(this.ScrollViewer.VerticalOffset + e.VerticalChange / scale);
        }
        private void InvalidateScale(out double scale, out double xOffset, out double yOffset)
        {
            //ScrollView的实际大小
            double gridW = this.scrollViewGrid.ActualWidth * this.scaleTransform.ScaleX;
            double gridH = this.scrollViewGrid.ActualHeight * this.scaleTransform.ScaleY;

            //缩放后的小地图实际大小
            double x = this.zoomCanvas.ActualWidth;
            double y = this.zoomCanvas.ActualHeight;

            double scaleX = x / gridW;
            double scaleY = y / gridH;

            scale = (scaleX < scaleY) ? scaleX : scaleY;

            xOffset = (x - scale * gridW) / 2;
            yOffset = (y - scale * gridH) / 2;
        }
    }
}
