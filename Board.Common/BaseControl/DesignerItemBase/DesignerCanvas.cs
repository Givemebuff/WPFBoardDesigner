using Board.Converter;
using Board.DesignerModel;
using Board.Interface;
using Board.Resource;
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
    public class DesignerCanvas : Canvas,IDesigner
    {
        private Point? dragStartPoint = null;
        public object GetDesignerModel()
        {
            return this.Board;
        }

        #region 选中项
        public object SelectItem
        {
            get { return (object)GetValue(SelectItemProperty); }
            set
            {            
                if(value==null)
                    value= this.Board;
                if (value is IDesigner)
                    value = (value as IDesigner).GetDesignerModel();
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
            (d as DesignerCanvas).Children.Clear();
            //初始化可视元素

            foreach (DesignerControl obj in (e.NewValue as DesignerBoard).VisualChildren)
            {
                UserControl u = BoardControlFactory.CreateBoardControlByDesignerType(obj.GetType(), obj);

                (d as DesignerCanvas).AddDesignerItem(u, new Point((obj as DesignerControl).Position.Location.X, (obj as DesignerControl).Position.Location.Y));
            }

            //注册后台元素
            foreach (DesignerDataSource ds in (e.NewValue as DesignerBoard).BackChildren) 
            {
                DataSourceManager.Register(ds.Name, ds.DataSourceType, ds);
                switch (ds.DataSourceType) 
                {
                    case DesignerDataSourceType.DataBase:
                        DataBaseDataSourceManager.UpdateDataBaseDataSource(ds as DesignerDataBaseDataSource);
                        break;
                    case DesignerDataSourceType.StaticText:
                        StaticTextDataSourceManager.UpdateStaticTextDataSource(ds as DesignerStaticTextDataSource);
                        break;
                }
            }

            ((DesignerCanvas)d).SelectItem = e.NewValue as DesignerBoard;
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


            this.Board.VisualChildren.CollectionChanged += VisualChildren_CollectionChanged;
        }

        void VisualChildren_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action) 
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach(object obj in e.NewItems)
                    {
                        this.AddDesignerItem(obj, new Point((obj as DesignerControl).Position.Location.X, (obj as DesignerControl).Position.Location.Y));
                    }
                    

                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach(object obj in e.NewItems)
                    for(int i =  this.Children.Count-1;i>=0;i++)
                    {
                        if (this.Children[i] == e.NewItems) 
                        {
                            this.Children.Remove(obj as UIElement);
                            break;
                        }
                         
                    }
                   
                  

                    break;
            }
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
                    ControlWrapper wrapper = BoardControlFactory.CreateCandyByName(controlName);
                    object candy = wrapper.Tear();
                    if (candy != null)
                    {
                        Point location = e.GetPosition(this);
                        AddDesignerItem(candy, location);
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }


                e.Handled = true;
            }
        }


     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">UserControl</param>
        /// <param name="location"></param>
        void AddDesignerItem(object c, Point location)
        {
           
            DesignerItem designerItem = new DesignerItem(c);
            this.Children.Add(designerItem);

            IDesigner idr = designerItem.Content as IDesigner;
            DesignerVisualElement dc = idr.GetDesignerModel() as DesignerVisualElement;


            Binding canvasLeft = new Binding("Position.Location.X") { Source = dc };
            designerItem.SetBinding(Canvas.LeftProperty, canvasLeft);
            Binding canvasTop = new Binding("Position.Location.Y") { Source = dc };
            designerItem.SetBinding(Canvas.TopProperty, canvasTop);
            Binding canvasZIndex = new Binding("Position.ZIndex") { Source = dc };
            designerItem.SetBinding(Canvas.ZIndexProperty, canvasZIndex);
            Binding widthBind = new Binding("Size.Width") { Source = dc,Mode= BindingMode.TwoWay };
            designerItem.SetBinding(WidthProperty, widthBind);
            Binding heightBind = new Binding("Size.Height") { Source = dc, Mode = BindingMode.TwoWay };
            designerItem.SetBinding(HeightProperty, heightBind);

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
                    //获取DesignerItem中直接包含的BoardControl
                    DesignerControl clonedChild = (DesignerControl)((obj as DesignerItem).Content as IDesigner).GetDesignerModel();
                    this.Board.AddVisualControl(clonedChild);
                    if (clonedChild is DesignerChart) 
                    {
                        foreach (DesignerChartDataSerie dcd in (clonedChild as DesignerChart).Series) 
                        {
                            IDynamicData idd = dcd as IDynamicData;
                            if (!string.IsNullOrEmpty(idd.DataSourceKey))
                            {
                                DesignerDataSource ds = DataSourceManager.GetDataSource(idd.DataSourceKey);
                                this.Board.AddBackControl(ds);
                            }
                        }
                    }
                    //若该设计模型是动态数据类型，则将该数据接口添加至Board的后台元素集合中
                    if (clonedChild is IDynamicData) 
                    {
                        IDynamicData idd = clonedChild as IDynamicData;
                        if (!string.IsNullOrEmpty(idd.DataSourceKey)) 
                        {
                            DesignerDataSource ds = DataSourceManager.GetDataSource(idd.DataSourceKey);
                            this.Board.AddBackControl(ds);
                        }
                    }
                }
            }
            return this.Board;
        }

       
    }
}
