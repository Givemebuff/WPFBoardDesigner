using BoardDesigner.Interface;
using BoardDesigner.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoardDesigner.UControl
{
    /// <summary>
    /// BoardDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class BoardDataGrid : UserControl, IDesigner
    {
        public DesignerTable DesignerItem { get; set; }



        public ObservableCollection<DesignerDataGridColumn> Columns
        {
            get { return (ObservableCollection<DesignerDataGridColumn>)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns",
            typeof(ObservableCollection<DesignerDataGridColumn>),
            typeof(BoardDataGrid),
            new PropertyMetadata(new ObservableCollection<DesignerDataGridColumn>(), new PropertyChangedCallback(ColumnsPropertyChanged)));

        private static void ColumnsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoardDataGrid bdg = d as BoardDataGrid;
            ObservableCollection<DesignerDataGridColumn> columns = e.NewValue as ObservableCollection<DesignerDataGridColumn>;
            bdg.ClearHeaderColumn();
            foreach (DesignerDataGridColumn column in columns)
            {
                bdg.AddColumn(column);
            }
        }

        void Columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object obj in e.NewItems)
                    {
                        AddColumn(obj as DesignerDataGridColumn);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (object obj in e.OldItems)
                    {
                        RemoveColumn(obj as DesignerDataGridColumn);
                    }
                    break;
                default: break;
            }
        }


        public BoardDataGrid()
        {
            InitializeComponent();
            DesignerItem = new DesignerTable()
            {
                Size = new DesignerSize(300, 180),
                Border = new DesignerBorder(),
                HeaderHeight = 40,
                HeaderBorder = new DesignerBorder(),
                ContentBorder = new DesignerBorder(),
                Position = new DesignerPosition() { Padding = new Thickness(5) },
                HeaderPadding = new Thickness(5),
                ContentPadding = new Thickness(5),
                ContentMargin = new Thickness(2),
                HeaderMargin = new Thickness(2),
                HeaderFont = new DesignerFont(),
                HeaderHorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                HeaderVerticalAlignment = System.Windows.VerticalAlignment.Center,
                Columns = new ObservableCollection<DesignerDataGridColumn>()
            };

            InitBinding();

        }
        public object GetDesignerItem()
        {
            return this.DesignerItem;
        }

        public void InitBinding()
        {
            this.DataContext = DesignerItem;
            Binding canvasLeft = new Binding("Position.Location.X") { Source = DataContext };
            this.SetBinding(Canvas.LeftProperty, canvasLeft);
            Binding canvasTop = new Binding("Position.Location.Y") { Source = DataContext };
            this.SetBinding(Canvas.TopProperty, canvasTop);
            this.SetBinding(ColumnsProperty, new Binding("Columns") { Source = DataContext });
            Columns.CollectionChanged += Columns_CollectionChanged;
        }

        #region 标题
        public int HeaderColumnsCount
        {
            get
            {
                return HeaderGrid.ColumnDefinitions.Count;
            }
        }

        public void ClearHeaderColumn()
        {
            HeaderGrid.ColumnDefinitions.Clear();
            HeaderGrid.Children.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="headerText"></param>    
        /// <returns>索引</returns>
        public int AddColumn(DesignerDataGridColumn column)
        {
            ColumnDefinition cd = new ColumnDefinition();
            cd.SetBinding(WidthProperty, new Binding("ColumnWidth") { Source = column });

            HeaderGrid.ColumnDefinitions.Add(cd);

            TextBlock tb = new TextBlock();
            HeaderGrid.Children.Add(tb);

            tb.SetBinding(TextBlock.HorizontalAlignmentProperty, new Binding("HorizontalContentAlignment") { Source = column });
            tb.SetBinding(TextBlock.VerticalAlignmentProperty, new Binding("VerticalContentAlignment") { Source = column });
            tb.SetBinding(TextBlock.FontFamilyProperty, new Binding("Font.FontFamily") { Source = column });
            tb.SetBinding(TextBlock.FontSizeProperty, new Binding("Font.FontSize") { Source = column });
            tb.SetBinding(TextBlock.ForegroundProperty, new Binding("Font.FontColor") { Source = column });
            tb.SetBinding(TextBlock.FontStyleProperty, new Binding("Font.FontStyle") { Source = column });
            tb.SetBinding(TextBlock.TextProperty, new Binding("Text") { Source = column });
            tb.Uid = column.Name;
            int lastIndex = HeaderColumnsCount - 1;
            tb.SetValue(Grid.ColumnProperty, lastIndex);

            return lastIndex;
        }

        public void RemoveColumn(DesignerDataGridColumn column)
        {
            int index = 0;
            //删除元素
            for (int j = 0; j < HeaderGrid.Children.Count; j++)
            {
                TextBlock tb = HeaderGrid.Children[j] as TextBlock;
                if ((HeaderGrid.Children[j] as TextBlock).Uid == column.Name)
                {

                    index = (int)tb.GetValue(Grid.ColumnProperty);
                    HeaderGrid.Children.Remove(HeaderGrid.Children[j]);
                    break;
                }
            }
            //再遍历，若当前列还有元素则退出
            foreach (object obj in HeaderGrid.Children) 
            {
                TextBlock tb = obj as TextBlock;
                int columnN = (int)tb.GetValue(Grid.ColumnProperty);
                if (columnN == index)
                    return;
            }
            //若没有则前移
            foreach (object obj in HeaderGrid.Children)
            {
                TextBlock tb = obj as TextBlock;
                int columnN = (int)tb.GetValue(Grid.ColumnProperty);
                if (columnN > index)
                {
                    columnN--;
                    tb.SetValue(Grid.ColumnProperty, columnN);
                }
            }
            HeaderGrid.ColumnDefinitions.Remove(HeaderGrid.ColumnDefinitions[index]);   
        }

        #endregion
    }
}
