using BoardDesigner.Base;
using BoardDesigner.BaseAdorner;
using BoardDesigner.Interface;
using BoardDesigner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace BoardDesigner.BaseThumb
{
    public class MoveableThumb:Thumb
    {
        private Adorner _infoAdorner;
        /// <summary>
        /// 将进行操作的内容控件
        /// </summary>
        private DesignerItem _beMovedItem;      
        /// <summary>
        /// 变换信息
        /// </summary>
        private RotateTransform _transform;

        private DesignerCanvas _parent;

        Point sp;
        Point cp;
        public MoveableThumb()
        {
            DragStarted += new DragStartedEventHandler(this.MoveThumb_DragStarted);
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
            DragCompleted += new DragCompletedEventHandler(this.MoveThumb_DragCompleted);
        }
        /// <summary>
        /// 已经开始拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this._beMovedItem = DataContext as DesignerItem;
            sp = Mouse.GetPosition(this._parent);
            if (this._beMovedItem != null)
            {
                this._parent = VisualTreeHelper.GetParent(this._beMovedItem) as DesignerCanvas;
                this._transform = this._beMovedItem.RenderTransform as RotateTransform;

                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this._parent);
                if (adornerLayer != null)
                {
                    this._infoAdorner = new LocationInfoAdorner(this._beMovedItem);
                    adornerLayer.Add(this._infoAdorner);
                }
            }
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e) 
        {
            if (this._beMovedItem != null && this._parent != null && this._beMovedItem.IsSelected)
            {
                Point dragDelta = new Point(e.HorizontalChange, e.VerticalChange);
                Point offsetPoint = new Point(dragDelta.X, dragDelta.Y);  
                if (_transform != null)
                {
                    offsetPoint = _transform.Transform(offsetPoint);                 
                }
                foreach (DesignerItem item in this._parent.SelectedItems)
                {
                    DesignerVisualElement dc = (item.Content  as IDesigner).GetDesignerItem() as DesignerVisualElement;
                  
                    dc.Position.MoveOffset(offsetPoint.X,offsetPoint.Y);                   
                   // DesignerCanvas.SetLeft(item, DesignerCanvas.GetLeft(item) + );
                   //DesignerCanvas.SetTop(item, DesignerCanvas.GetTop() + );          
                }
                this._parent.InvalidateMeasure();
                e.Handled = true;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }

        void Move(DesignerItem item,double x,double y) 
        {
            RotateTransform rotateTransform = item.RenderTransform as RotateTransform;

            if (rotateTransform != null)
            {
                Point p = rotateTransform.Transform(new Point(x,y));

            }
        }

        private void MoveThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (this._infoAdorner != null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this._parent);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this._infoAdorner);
                }

                this._infoAdorner = null;
            }
        }
    }
}
