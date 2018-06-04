using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace BoardDesigner.Base
{
    public class OverViewMap:Control
    {
        private Thumb zoomThumb;
        private Canvas zoomCanvas;
        private Grid scrollViewGrid;
       // private Slider zoomSlider;
        private ScaleTransform scaleTransform;
        private DesignerCanvas designerCanvas;
        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(OverViewMap));
        public OverViewMap() 
        {

        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.ScrollViewer == null)
                return;

            this.scrollViewGrid = this.ScrollViewer.Content as Grid;
            if (this.scrollViewGrid == null)
                throw new Exception("ContentGrid Not Founded!");

            this.designerCanvas = this.scrollViewGrid.FindName("MainPanel") as DesignerCanvas;
            if (this.designerCanvas == null)
                throw new Exception("DesignerCanvas must not be null!");

            this.zoomThumb = Template.FindName("ViewMoveThumb", this) as Thumb;
            if (this.zoomThumb == null)
                throw new Exception("ViewMoveThumb template is missing!");

            this.zoomCanvas = Template.FindName("Map", this) as Canvas;
            if (this.zoomCanvas == null)
                throw new Exception("Map template is missing!");

            //this.zoomSlider = Template.FindName("PART_ZoomSlider", this) as Slider;
            //if (this.zoomSlider == null)
            //    throw new Exception("PART_ZoomSlider template is missing!");

            this.designerCanvas.LayoutUpdated += new EventHandler(this.DesignerCanvas_LayoutUpdated);

            this.zoomThumb.DragDelta += new DragDeltaEventHandler(this.Thumb_DragDelta);

            //this.zoomSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.ZoomSlider_ValueChanged);

            this.scaleTransform = new ScaleTransform();
            this.scrollViewGrid.LayoutTransform = this.scaleTransform;
        }

        private void DesignerCanvas_LayoutUpdated(object sender, EventArgs e)
        {
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
