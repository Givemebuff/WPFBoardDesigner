using Board.Converter;
using Board.DesignerModel;
using Board.Interface;
using BoardDesigner.BaseAdorner;
using BoardDesigner.BoardControl;
using BoardDesigner.Factory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace BoardDesigner.Base
{
    public class DesignerCanvas : Canvas
    {
        private Point? dragStartPoint = null;


        #region 选中项
        public object SelectItem
        {
            get { return (object)GetValue(SelectItemProperty); }
            set
            {
                if (value == null)
                    return;
                if (value is DesignerItem)
                    value = ((IDesigner)(value as DesignerItem).Content).GetDesignerItem();                
                SetValue(SelectItemProperty, value);
            }
        }
        public static readonly DependencyProperty SelectItemProperty =
            DependencyProperty.Register("SelectItem", typeof(object), typeof(DesignerCanvas), new PropertyMetadata(null, new PropertyChangedCallback(SelectItemPropertyChanged)));

        private static void SelectItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        public DesignerBoard Board
        {
            get { return (DesignerBoard)GetValue(BoardProperty); }
            set
            {
                SetValue(BoardProperty, value);
            }
        }
        public static readonly DependencyProperty BoardProperty =
          DependencyProperty.Register("Board", typeof(DesignerBoard), typeof(DesignerCanvas), new PropertyMetadata(
              new DesignerBoard()
              {
                  Size = new DesignerSize(1366, 768),
                  Background = new DesignerBrush(Color.FromRgb(255, 255, 255))//new DesignerBrush(Color.FromRgb(255, 255, 255))//new SolidColorBrush(Color.FromRgb(255, 255, 255))//
              },
              new PropertyChangedCallback(BoardPropertyChanged)));

        private static void BoardPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //TODO新建看板
            ((DesignerCanvas)d).SelectItem = e.NewValue;
        }

        public IEnumerable<DesignerItem> SelectedItems
        {
            get
            {
                var selectedItems = from item in this.Children.OfType<DesignerItem>()
                                    where item.IsSelected == true
                                    select item;

                return selectedItems;
            }
        }

        public DesignerCanvas()
        {           
            InitAndBinding();
        }
        public DesignerCanvas(DesignerBoard db)
        {
            this.Board = db;            
            InitAndBinding();
        }

        void InitAndBinding()
        {
            this.DataContext = Board;
            this.AllowDrop = true;
            this.LayoutTransform = new ScaleTransform();

            this.SetBinding(Canvas.WidthProperty, new Binding("Size.Width") { Source = DataContext });
            this.SetBinding(Canvas.HeightProperty, new Binding("Size.Height") { Source = DataContext });
            this.SetBinding(Canvas.OpacityProperty, new Binding("Opacity") { Source = DataContext });
            this.SetBinding(Canvas.VisibilityProperty, new Binding("Visibility") { Source = DataContext });
            MultiBinding bkBind = new MultiBinding();
            bkBind.Bindings.Add(new Binding("Background.BackgoundImage") { Source = DataContext });
            bkBind.Bindings.Add(new Binding("Background.ColorBrush") { Source = DataContext });
            bkBind.Converter = (IMultiValueConverter)(new DesignerBrushToBrushConverter());
            this.SetBinding(Canvas.BackgroundProperty, bkBind);           
        }


        public void DeselectAll()
        {
            foreach (DesignerItem item in this.SelectedItems)
            {
                item.IsSelected = false;
            }
            this.SelectItem = null;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source == this)
            {
                this.DeselectAll();
                this.dragStartPoint = new Point?(e.GetPosition(this));
                this.SelectItem = Board;
                e.Handled = true;
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            if ((Keyboard.Modifiers & (ModifierKeys.Control)) != ModifierKeys.None)
            {
                if (this.LayoutTransform == null)
                {
                    this.LayoutTransform = new ScaleTransform();
                }
                ScaleTransform stf = this.LayoutTransform as ScaleTransform;
                if (e.Delta > 0 && stf.ScaleX <1.5)
                {
                    stf.ScaleX += 0.1;
                    stf.ScaleY += 0.1;

                }
                else if (e.Delta < 0 && stf.ScaleX>0.5)
                {

                    stf.ScaleX -= 0.1;
                    stf.ScaleY -= 0.1;
                }
                e.Handled = true;            
            }

        }


        void Delete()
        {
            foreach (DesignerItem item in this.SelectedItems)
            {
                this.Children.Remove(item);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                this.dragStartPoint = null;
            }

            if (this.dragStartPoint.HasValue)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    RubberbandAdorner adorner = new RubberbandAdorner(this, this.dragStartPoint);
                    if (adorner != null)
                    {
                        adornerLayer.Add(adorner);
                    }
                }

                e.Handled = true;
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            string controlName = e.Data.GetData("NewItem") as string;
            if (!String.IsNullOrEmpty(controlName))
            {
                try
                {
                    ControlWrapper wrapper = CandyFactory.CreateCandyByName(controlName);
                    object candy = wrapper.Tear();
                    if (candy != null)
                    {
                        Point location = e.GetPosition(this);
                        GetCandy(candy, location);
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }


                e.Handled = true;
            }
        }

        void GetCandy(object c, Point location)
        {
            DesignerItem designerItem = new DesignerItem(c);
            this.Children.Add(designerItem);

            IDesigner idr = designerItem.Content as IDesigner;
            DesignerVisualElement dc = idr.GetDesignerItem() as DesignerVisualElement;


            Binding canvasLeft = new Binding("Position.Location.X") { Source = dc };
            designerItem.SetBinding(Canvas.LeftProperty, canvasLeft);
            Binding canvasTop = new Binding("Position.Location.Y") { Source = dc };
            designerItem.SetBinding(Canvas.TopProperty, canvasTop);
            Binding canvasZIndex = new Binding("Position.ZIndex") { Source = dc };
            designerItem.SetBinding(Canvas.ZIndexProperty, canvasZIndex);

            dc.Position.MoveTo(location.X, location.Y);
          

            this.DeselectAll();
            designerItem.IsSelected = true;
            this.SelectItem = designerItem;
        }

        public DesignerBoard Warp()
        {
            foreach (object obj in this.Children)
            {
                if (obj is DesignerItem)
                {

                    DesignerControl clonedChild = (DesignerControl)((obj as DesignerItem).Content as IDesigner).GetDesignerItem();
                    this.Board.Children.Add(clonedChild);
                }
            }
            return this.Board;
        }

        //protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
        //{
        //    System.Windows.Size size = new System.Windows.Size();
        //    foreach (UIElement element in Children)
        //    {
        //        double left = Canvas.GetLeft(element);
        //        double top = Canvas.GetTop(element);
        //        left = double.IsNaN(left) ? 0 : left;
        //        top = double.IsNaN(top) ? 0 : top;

        //        element.Measure(constraint);

        //        System.Windows.Size desiredSize = element.DesiredSize;
        //        if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
        //        {
        //            size.Width = Math.Max(size.Width, left + desiredSize.Width);
        //            size.Height = Math.Max(size.Height, top + desiredSize.Height);
        //        }
        //    }

        //    // 随超出的边界增长
        //    //size.Width += 5;
        //    //size.Height += 5;
        //    return size;
        //}
    }
}
