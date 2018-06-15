using Board.DesignerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;


namespace Board.BaseControl
{
    public class ResizeableThumb : Thumb
    {
        private Adorner _infoAdorner;      
        private ContentControl _designerItem;
        private RotateTransform rotateTransform;
        private Point transformOrigin;
        private Canvas _parent;
        private double angle;

        public ResizeableThumb()
        {
            DragStarted += ResizeableThumb_DragStarted;
            DragDelta += ResizeableThumb_DragDelta;
            DragCompleted += ResizeableThumb_DragCompleted;

             this._designerItem = this.DataContext as ContentControl;
           
        }

        void ResizeableThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            //缩放完成隐藏缩放信息
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
        private void ResizeableThumb_DragStarted(object sender,DragStartedEventArgs e)
        {
            this._designerItem = this.DataContext as ContentControl;
            //开始缩放显示缩放信息          

            if (this._designerItem != null)
            {
                this._parent = VisualTreeHelper.GetParent(this._designerItem) as Canvas;

                if (this._parent != null)
                {
                    this._designerItem.Height = this._designerItem.ActualHeight;
                    this._designerItem.Width = this._designerItem.ActualWidth;
                    this.transformOrigin = this._designerItem.RenderTransformOrigin;
                    this.rotateTransform = this._designerItem.RenderTransform as RotateTransform;
                    if (this.rotateTransform != null)
                    {
                        this.angle = this.rotateTransform.Angle * Math.PI / 180.0;
                    }
                    else
                    {
                        this.angle = 0.0d;
                    }

                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this._parent);
                    if (adornerLayer != null)
                    {
                        this._infoAdorner = new SizeInfoAdorner(this._designerItem);
                        adornerLayer.Add(this._infoAdorner);
                    }
                    double ag =this.rotateTransform.Angle;
                    //根据旋转角度改变光标
                    //if (ag < 0 && ag > -90) //右上
                    //{
                        
                    //}
                    //else if (ag == -90) //超正上
                    //{

                    //}

                }
            }
        }
        private void ResizeableThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {     
            if (this._designerItem != null)
            {
                double deltaVertical, deltaHorizontal;
                DesignerVisualElement element = (this._designerItem.Content as ContentControl).DataContext as DesignerVisualElement;
                
                switch (VerticalAlignment)
                {
                    case System.Windows.VerticalAlignment.Bottom:

                        deltaVertical = Math.Min(-e.VerticalChange, this._designerItem.ActualHeight - this._designerItem.MinHeight);
                        element.Position.MoveOffset(-deltaVertical * this.transformOrigin.Y * Math.Sin(-this.angle), (this.transformOrigin.Y * deltaVertical * (1 - Math.Cos(-this.angle))));   
                        this._designerItem.Height -= deltaVertical;
                        //element.Size.Height -= deltaVertical;
                        break;
                    case System.Windows.VerticalAlignment.Top:
                        deltaVertical = Math.Min(e.VerticalChange, this._designerItem.ActualHeight - this._designerItem.MinHeight);
                        element.Position.MoveOffset( deltaVertical * Math.Sin(-this.angle) - (this.transformOrigin.Y * deltaVertical * Math.Sin(-this.angle)), deltaVertical * Math.Cos(-this.angle) + (this.transformOrigin.Y * deltaVertical * (1 - Math.Cos(-this.angle))));   
                        this._designerItem.Height -= deltaVertical;
                        //element.Size.Height -= deltaVertical;
                        break;
                    default:
                        break;
                }

                switch (HorizontalAlignment)
                {
                    case System.Windows.HorizontalAlignment.Left:
                        deltaHorizontal = Math.Min(e.HorizontalChange, this._designerItem.ActualWidth - this._designerItem.MinWidth);
                        element.Position.MoveOffset(deltaHorizontal * Math.Cos(this.angle) + (this.transformOrigin.X * deltaHorizontal * (1 - Math.Cos(this.angle))), deltaHorizontal * Math.Sin(this.angle) - this.transformOrigin.X * deltaHorizontal * Math.Sin(this.angle));                                           
                        this._designerItem.Width -= deltaHorizontal;
                        //element.Size.Width -= deltaHorizontal;
                        break;
                    case System.Windows.HorizontalAlignment.Right:
                        deltaHorizontal = Math.Min(-e.HorizontalChange, this._designerItem.ActualWidth - this._designerItem.MinWidth);
                        element.Position.MoveOffset(- this.transformOrigin.X * deltaHorizontal * Math.Sin(this.angle), -this.transformOrigin.X * deltaHorizontal * Math.Sin(this.angle));                                           
                        this._designerItem.Width -= deltaHorizontal;
                        //element.Size.Width -= deltaHorizontal;
                        break;
                    default:
                        break;
                }
            }

            e.Handled = true;
        }
    }
}
