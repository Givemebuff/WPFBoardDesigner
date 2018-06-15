using BoardDesigner.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace BoardDesigner.BaseThumb
{
    public class CenterPointThumb : Thumb
    {

        public Point OriginPoint
        {
            get { return (Point)GetValue(OriginPointProperty); }
            set { SetValue(OriginPointProperty, value); }
        }

        public static readonly DependencyProperty OriginPointProperty =
            DependencyProperty.Register("OriginPoint", typeof(Point), typeof(CenterPointThumb), new FrameworkPropertyMetadata(new Point(), new PropertyChangedCallback(OriginPointProperty_Changed)));

        private RotateTransform _transform;       
        private Point _renderTranformOriginPoint;
        private DesignerItem designerItem;
        private DesignerCanvas _parent;
        public CenterPointThumb()
        {           
            DragDelta += new DragDeltaEventHandler(this.CenterPointThumb_DragDelta);
            DragStarted += new DragStartedEventHandler(this.CenterPointThumb_DragStarted);
            this.Loaded += new RoutedEventHandler(OnLoaded);
         

        }
        private static void OriginPointProperty_Changed
           (DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void CenterPointThumb_DragStarted(object sender, DragStartedEventArgs e)
        {           

            if (this.designerItem != null)
            {
                this._parent = VisualTreeHelper.GetParent(this.designerItem) as DesignerCanvas;
                this._transform = this.designerItem.RenderTransform as RotateTransform;
             //   this.OriginPoint = 
                _renderTranformOriginPoint = this.designerItem.RenderTransformOrigin;                     
            }
        }

        private void CenterPointThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Point dragDelta = new Point(e.HorizontalChange, e.VerticalChange);
            if (this.designerItem != null && this._parent != null)
            {
                if (this._transform != null) 
                {
                  dragDelta = _transform.Transform(dragDelta);                
                }

                DesignerCanvas.SetLeft(this, DesignerCanvas.GetLeft(this) + dragDelta.X);
                DesignerCanvas.SetTop(this, DesignerCanvas.GetTop(this) + dragDelta.Y);
                e.Handled = true; 
            }
        }

        private void  OnLoaded(object sender, RoutedEventArgs e) 
        {
            this.designerItem = DataContext as DesignerItem;
            this.OriginPoint = this.designerItem.TranslatePoint(
                      new Point(this.designerItem.ActualWidth * this.designerItem.RenderTransformOrigin.X,
                                this.designerItem.ActualHeight * this.designerItem.RenderTransformOrigin.Y),
                                this._parent);
            //Binding bd = new Binding()
            //{
            //    Path = new PropertyPath("RenderTransformOrigin"),
            //    Source = this.designerItem,
            //    Converter = (IValueConverter)(new PointMarginConverter()),
            //    ConverterParameter = new Size((designerItem.Content as FrameworkElement).ActualWidth, (designerItem.Content as FrameworkElement).ActualHeight)
            //};
            //BindingOperations.SetBinding(this, Thumb.MarginProperty, bd);
        }
    }
}
