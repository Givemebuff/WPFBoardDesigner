using BoardDesigner.Base;
using BoardDesigner.BaseAdorner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BoardDesigner.BaseThumb
{
    public class LineThumb : Thumb
    {
        private Adorner _infoAdorner;
        /// <summary>
        /// 将进行操作的内容控件
        /// </summary>
        private Line _line;
        /// <summary>
        /// 变换信息
        /// </summary>
        private RotateTransform _transform;

        private DesignerItem _parentItem;

        private DesignerCanvas _parentCanvas;
        public LineThumb()
        {
            DragStarted += new DragStartedEventHandler(this.LineThumb_DragStarted);
            DragDelta += new DragDeltaEventHandler(this.LineThumb_DragDelta);
            DragCompleted += new DragCompletedEventHandler(this.LineThumb_DragCompleted);
        }
        /// <summary>
        /// 已经开始拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LineThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this._parentItem = DataContext as DesignerItem;

            if (this._parentItem != null)
            {
                this._line = _parentItem.Content as Line;
                //初始化起点终点

                this._parentCanvas = VisualTreeHelper.GetParent(this._parentItem) as DesignerCanvas;

                this._transform = this._line.RenderTransform as RotateTransform;

                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this._parentCanvas);
                if (adornerLayer != null)
                {
                    //TODO
                    //this._infoAdorner = new LineAdorner(this._line);
                    //adornerLayer.Add(this._infoAdorner);
                }
            }
        }

        private void LineThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this._line != null && this._parentItem != null)
            {
                double offsetX = e.HorizontalChange;
                double offsetY = e.VerticalChange;

                if (this.Name == "StartPoint")
                {
                    //左上方起点 
                    Canvas.SetLeft(_parentItem, Canvas.GetLeft(_parentItem) + offsetX);
                    Canvas.SetTop(_parentItem, Canvas.GetTop(_parentItem) + offsetY);
                    this._line.X2 -= offsetX;
                    this._line.Y2 -= offsetY;
                }
                else
                {
                    //右下方终点                    
                    this._line.X2 += offsetX;
                    this._line.Y2 += offsetY;
                }

               

                this._parentCanvas.InvalidateMeasure();
                e.Handled = true;
            }
        }
        private void LineThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (this._infoAdorner != null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this._parentCanvas);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this._infoAdorner);
                }

                this._infoAdorner = null;
            }
        }
    }
}
