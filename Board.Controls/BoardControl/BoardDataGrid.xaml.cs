using Board.DesignerModel;
using Board.Interface;
using Indusfo.Common;
using Indusfo.Data.DataAccessBaseLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Board.Controls.BoardControl
{
    /// <summary>
    /// BoardDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class BoardDataGrid : UserControl, IDesigner, IWorker
    {
        public DesignerTable DesignerModel { get; set; }


        #region 数据源

        public DesignerDataSource DataSource
        {
            get { return (DesignerDataSource)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(DesignerDataSource), typeof(BoardDataGrid), new PropertyMetadata(null));

        public DataTable ItemSource
        {
            get { return (DataTable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(DataTable), typeof(BoardDataGrid), new PropertyMetadata(null, new PropertyChangedCallback(ItemSourcePropertyChanged)));

        private static void ItemSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoardDataGrid bdg = d as BoardDataGrid;
            bdg.StartBind();
        }

        #endregion

        #region 显示行数
        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RowCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", typeof(int), typeof(BoardDataGrid), new PropertyMetadata(0, new PropertyChangedCallback(RowCountPropertyChanged)));

        private static void RowCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoardDataGrid bdg = d as BoardDataGrid;
            int newCount = (int)e.NewValue;
            int currnentCount = bdg.ContentGrid.RowDefinitions.Count;

            if (currnentCount > newCount)
            {
                //减少
                int diff = currnentCount - newCount;
                for (int i = 0; i < diff; i++)
                {
                    bdg.ContentGrid.RowDefinitions.Remove(bdg.ContentGrid.RowDefinitions.Last());
                }
            }
            else if (currnentCount < newCount)
            {
                //增加
                int diff = newCount - currnentCount;
                for (int i = 0; i < diff; i++)
                {
                    bdg.ContentGrid.RowDefinitions.Add(new RowDefinition());
                }
            }
        }
        #endregion

        #region 列标题集合
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
            bdg.InitColumns(columns);
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

        #endregion

        #region 筛选行

        public ObservableCollection<DesignerAlterRow> AlterRows
        {
            get { return (ObservableCollection<DesignerAlterRow>)GetValue(AlterRowsProperty); }
            set { SetValue(AlterRowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlterRows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlterRowsProperty =
            DependencyProperty.Register("AlterRows", typeof(ObservableCollection<DesignerAlterRow>), typeof(BoardDataGrid), new PropertyMetadata(null));
        #endregion

        #region 时间

        #region 数据访问时间

        public DispatcherTimer DataAccrssTimer { get; set; }
        public int DataAccessTimeSpan
        {
            get { return (int)GetValue(DataAccessTimeSpanProperty); }
            set { SetValue(DataAccessTimeSpanProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataAccessTimeSpan.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataAccessTimeSpanProperty =
            DependencyProperty.Register("DataAccessTimeSpan", typeof(int), typeof(BoardDataGrid), new PropertyMetadata(60000, new PropertyChangedCallback(DataAccessTimeSpanPropertyChanged)));

        private static void DataAccessTimeSpanPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoardDataGrid bdg = d as BoardDataGrid;
            if (bdg.DataAccrssTimer != null)
                bdg.DataAccrssTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)e.NewValue);
        }

        #endregion

        #region 分页刷新时间
        public DispatcherTimer PaggingTimer { get; set; }

        public int PaggingTimeSpan
        {
            get { return (int)GetValue(PaggingTimeSpanProperty); }
            set { SetValue(PaggingTimeSpanProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PaggingTimeSpan.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaggingTimeSpanProperty =
            DependencyProperty.Register("PaggingTimeSpan", typeof(int), typeof(BoardDataGrid), new PropertyMetadata(1000, new PropertyChangedCallback(PaggingTimeSpanPropertyChanged)));


        private static void PaggingTimeSpanPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoardDataGrid bdg = d as BoardDataGrid;
            if (bdg.PaggingTimer != null)
                bdg.PaggingTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)e.NewValue);
        }

        #endregion

        #endregion

        public BoardDataGrid(DesignerTable dt)
        {
            InitializeComponent();
            DesignerModel = dt;
            InitBinding();
            InitTimer();

        }
        public BoardDataGrid()
        {
            InitializeComponent();
            DesignerModel = new DesignerTable()
            {
                Size = new DesignerSize(300, 180),
                Border = new DesignerBorder(),
                DisplayRowCount = 10,
                HeaderHeight = 40,
                HeaderBorder = new DesignerBorder(),
                ContentBorder = new DesignerBorder(),
                Position = new DesignerPosition() { Padding = new Thickness(5) },
                HeaderPadding = new Thickness(5),
                ContentMargin = new Thickness(2),
                HeaderMargin = new Thickness(2),
                HeaderFont = new DesignerFont(),
                HeaderHorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                HeaderVerticalAlignment = System.Windows.VerticalAlignment.Center,
                Columns = new ObservableCollection<DesignerDataGridColumn>(),
                PaggingTimeSpan = 10000,
                DataAccrssTimeSpan = 60000,
                CellHorizontalAlignment = HorizontalAlignment.Center,
                CellVerticalAlignment = VerticalAlignment.Center,
                AlterRows = new ObservableCollection<DesignerAlterRow>()
            };

            InitBinding();
            InitTimer();

        }

        public object GetDesignerModel()
        {
            return this.DesignerModel;
        }

        public void InitTimer()
        {

            DataAccrssTimer = new DispatcherTimer();
            DataAccrssTimer.Tick += DataAccrssTimer_Tick;
            DataAccrssTimer.Interval = new TimeSpan(0, 0, 0, 0, DesignerModel.DataAccrssTimeSpan);

            PaggingTimer = new DispatcherTimer();
            PaggingTimer.Tick += PaggingTimer_Tick;
            PaggingTimer.Interval = new TimeSpan(0, 0, 0, 0, DesignerModel.PaggingTimeSpan);
        }

        public void InitBinding()
        {
            this.DataContext = DesignerModel;

            this.SetBinding(ColumnsProperty, new Binding("Columns") { Source = DataContext });
            this.SetBinding(RowCountProperty, new Binding("DisplayRowCount") { Source = DataContext });
            this.SetBinding(AlterRowsProperty, new Binding("AlterRows") { Source = DataContext });
            this.SetBinding(PaggingTimeSpanProperty, new Binding("PaggingTimeSpan") { Source = DataContext });
            this.SetBinding(DataSourceProperty, new Binding("DataSource") { Source = DataContext });
            Columns.CollectionChanged += Columns_CollectionChanged;
        }


        #region 数据

        public void GetData()
        {
            
        }

        public void StartBind()
        {
            if (ItemSource != null && ItemSource.Rows.Count > 0)
            {
                int rowCount = ItemSource.Rows.Count;
                int yu = rowCount % RowCount;
                PageCount = rowCount / RowCount;
                if (yu > 0)
                    PageCount++;
                PageIndex = 0;
            }

        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount { get; set; }

        public void StartWork()
        {
            DataAccrssTimer.Start();
            GetData();
            PaggingTimer.Start();
        }

        private void PaggingTimer_Tick(object sender, EventArgs e)
        {
            ContentGrid.Children.Clear();
            BindPage(GetNextPageData());

            DoubleAnimation opacityAnimation2 = new DoubleAnimation();
            opacityAnimation2.From = 0;//透明度初始值
            opacityAnimation2.To = 1;//透明度值
            opacityAnimation2.Duration = new Duration(TimeSpan.FromSeconds(2));
            ContentGrid.BeginAnimation(Grid.OpacityProperty, opacityAnimation2);
        }
        void DataAccrssTimer_Tick(object sender, EventArgs e)
        {
            GetData();
        }

        public DataTable GetNextPageData()
        {

            DataTable data = new DataTable();
            if (ItemSource == null || ItemSource.Rows.Count <= 0)
                return data;
            //int index = 0;
            //新DataTable按列表头顺序集合有效数据

            for (int i = 0; i < Columns.Count; i++)
            {
                data.Columns.Add(Columns[i].BindingName);
            }

            int beginRowIndex = PageIndex * RowCount;
            int endRowIndex = beginRowIndex + RowCount;
            for (int i = beginRowIndex;
                i < (endRowIndex > (ItemSource.Rows.Count - 1) ? ItemSource.Rows.Count : endRowIndex); i++)
            {
                DataRow newRow = data.NewRow();
                foreach (DataColumn col in data.Columns)
                {
                    if (ItemSource.Columns.Contains(col.ColumnName))
                        newRow[col.ColumnName] = ItemSource.Rows[i][col.ColumnName];
                }
                data.Rows.Add(newRow);
            }

            PageIndex++;

            if (PageIndex >= PageCount)
                PageIndex = 0;

            return data;
        }

        public void BindPage(DataTable data)
        {
            //将一页内容显示到表格中
            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    BoardDataGridContentTexkBlock tb = new BoardDataGridContentTexkBlock(this.DesignerModel, data.Rows[i][j].ToString());

                    foreach (DesignerAlterRow ar in AlterRows)
                    {
                        if (ar.AlternationRowIndex <= 0)
                            continue;
                        else if ((i + 1) % ar.AlternationRowIndex == 0)
                        {
                            if (ar.AlternatingRowBackground != null)
                                tb.uBorder.Background = ar.AlternatingRowBackground;
                        }

                    }
                    tb.SetValue(Grid.RowProperty, i);
                    tb.SetValue(Grid.ColumnProperty, j);
                    ContentGrid.Children.Add(tb);
                }
            }
        }

        #endregion

        #region 列变化
        public int HeaderColumnsCount
        {
            get
            {
                return HeaderGrid.ColumnDefinitions.Count;
            }
        }

        public void ClearColumn()
        {
            HeaderGrid.ColumnDefinitions.Clear();
            HeaderGrid.Children.Clear();
            ContentGrid.ColumnDefinitions.Clear();
            ContentGrid.Children.Clear();
        }

        public void InitColumns(ObservableCollection<DesignerDataGridColumn> columns)
        {
            ClearColumn();
            foreach (DesignerDataGridColumn column in columns)
            {
                AddColumn(column);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="headerText"></param>    
        /// <returns>索引</returns>
        public void AddColumn(DesignerDataGridColumn column)
        {
            //添加列，获取坐标
            ColumnDefinition cd = new ColumnDefinition();
            HeaderGrid.ColumnDefinitions.Add(cd);
            int lastIndex = HeaderColumnsCount - 1;
            column.Position._columnIndex = lastIndex;
            //设置宽度
            cd.SetBinding(ColumnDefinition.WidthProperty, new Binding("ColumnWidth") { Source = column });
            BoardDataGridColumn bd = new BoardDataGridColumn(column);
            HeaderGrid.Children.Add(bd);
            bd.Uid = column.Name;

            //内容
            ColumnDefinition cd2 = new ColumnDefinition();
            cd2.SetBinding(ColumnDefinition.WidthProperty, new Binding("ColumnWidth") { Source = column });
            ContentGrid.ColumnDefinitions.Add(cd2);
        }

        public void RemoveColumn(DesignerDataGridColumn column)
        {
            //删除列
            HeaderGrid.ColumnDefinitions.Remove(HeaderGrid.ColumnDefinitions[column.Position.ColumnIndex]);
            ContentGrid.ColumnDefinitions.Remove(ContentGrid.ColumnDefinitions[column.Position.ColumnIndex]);
            //删除元素
            for (int i = 0; i < HeaderGrid.Children.Count; i++)
            {
                BoardDataGridColumn bd = HeaderGrid.Children[i] as BoardDataGridColumn;
                if (bd.Uid == column.Name)
                {
                    HeaderGrid.Children.Remove(bd);
                    break;
                }
            }
            ReorderColumns(column.Position.ColumnIndex);


        }

        public void ReorderColumns(int index)
        {
            foreach (DesignerDataGridColumn column in Columns)
            {
                if (column.Position.ColumnIndex > index)
                    column.Position.ColumnIndex--;
            }
        }

        #endregion
    }
}
