using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Board.DesignerModel
{
    [Serializable]
    public class DesignerTable : DesignerControl
    {
        public DesignerTable()
        {
            this.Type = DesignerElementType.Table;
            this.AlterRows = new ObservableCollection<DesignerAlterRow>();
            this.Columns = new ObservableCollection<DesignerDataGridColumn>();
        }


        #region 筛选行

        [Browsable(false)]
        public ObservableCollection<DesignerAlterRow> _alterRows
        {
            get;set;
        }

        [Category("行相关")]
        [DisplayName("筛选条件集合")]
        [Description("筛选条件集合")]
        public ObservableCollection<DesignerAlterRow> AlterRows
        {
            get { return this._alterRows; }
            set
            {
                this._alterRows = value;
                OnPropertyChanged("AlterRows");
            }
        }

        #endregion

        #region 标题

        [Browsable(false)]
        public Thickness _headerPadding
        {
            get;
            set;
        }

        [Category("列标题相关")]
        [DisplayName("列标题内边距")]
        [Description("列标题内边距")]
        public Thickness HeaderPadding
        {
            get { return this._headerPadding; }
            set
            {
                this._headerPadding = value;
                OnPropertyChanged("HeaderPadding");
            }
        }

        [Browsable(false)]
        public Thickness _headerMargin
        {
            get;
            set;
        }

        [Category("列标题相关")]
        [DisplayName("列标题外边距")]
        [Description("列标题外边距")]
        public Thickness HeaderMargin
        {
            get { return this._headerMargin; }
            set
            {
                this._headerMargin = value;
                OnPropertyChanged("HeaderMargin");
            }
        }


        [Browsable(false)]
        public Brush _headerBackground
        {
            get;
            set;
        }

        [Category("列标题相关")]
        [DisplayName("列标题背景")]
        [Description("列标题背景色")]
        public Brush HeaderBackground
        {
            get { return this._headerBackground; }
            set
            {
                this._headerBackground = value;
                OnPropertyChanged("HeaderBackground");
            }
        }

        [Browsable(false)]
        public DesignerFont _headerFont
        {
            get;
            set;
        }

        [Category("列标题相关")]
        [DisplayName("列标题字体")]
        [Description("列标题字体")]
        public DesignerFont HeaderFont
        {
            get { return this._headerFont; }
            set
            {
                this._headerFont = value;
                OnPropertyChanged("HeaderFont");
            }
        }

        [Browsable(false)]
        public double _headerHeight
        {
            get;
            set;
        }

        [Category("列标题相关")]
        [DisplayName("列标题高度")]
        [Description("列标题高度")]
        public double HeaderHeight
        {
            get { return this._headerHeight; }
            set
            {
                this._headerHeight = value;
                OnPropertyChanged("HeaderHeight");
            }
        }

        [Browsable(false)]
        public DesignerBorder _headerBorder
        {
            get;
            set;
        }

        [Category("列标题相关")]
        [DisplayName("列标题边框")]
        [Description("列标题边框")]
        public DesignerBorder HeaderBorder
        {
            get { return this._headerBorder; }
            set
            {
                this._headerBorder = value;
                OnPropertyChanged("HeaderBorder");
            }
        }

        [Browsable(false)]
        public HorizontalAlignment _headerHorizontalAlignment
        {
            get;
            set;
        }

        [Category("列标题相关")]
        [DisplayName("列标题单元格水平排版")]
        [Description("列标题单元格水平排版")]
        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get { return this._headerHorizontalAlignment; }
            set
            {
                this._headerHorizontalAlignment = value;
                OnPropertyChanged("HeaderHorizontalAlignment");
            }
        }

        [Browsable(false)]
        public VerticalAlignment _headerVerticalAlignment
        {
            get;
            set;
        }

        [Category("列标题相关")]
        [DisplayName("列标题单元格垂直排版")]
        [Description("列标题单元格垂直排版")]
        public VerticalAlignment HeaderVerticalAlignment
        {
            get { return this._headerVerticalAlignment; }
            set
            {
                this._headerVerticalAlignment = value;
                OnPropertyChanged("HeaderVerticalAlignment");
            }
        }




        #endregion

        #region 内容

        [Browsable(false)]
        public Thickness _contentPadding
        {
            get;
            set;
        }

        [Category("内容")]
        [DisplayName("内容内边距")]
        [Description("内容内边距")]
        public Thickness ContentPadding
        {
            get { return this._contentPadding; }
            set
            {
                this._contentPadding = value;
                OnPropertyChanged("ContentPadding");
            }
        }

        [Browsable(false)]
        public Thickness _contentMargin
        {
            get;
            set;
        }

        [Category("内容")]
        [DisplayName("内容外边距")]
        [Description("内容外边距")]
        public Thickness ContentMargin
        {
            get { return this._contentMargin; }
            set
            {
                this._contentMargin = value;
                OnPropertyChanged("ContentMargin");
            }
        }


        [Browsable(false)]
        public Brush _contentBackground
        {
            get;
            set;
        }

        [Category("内容")]
        [DisplayName("内容背景色")]
        [Description("内容背景色")]
        public Brush ContentBackground
        {
            get { return this._contentBackground; }
            set
            {
                this._contentBackground = value;
                OnPropertyChanged("ContentBackground");
            }
        }

        [Browsable(false)]
        public DesignerBorder _contentBorder
        {
            get;
            set;
        }

        [Category("内容")]
        [DisplayName("内容边框")]
        [Description("内容边框")]
        public DesignerBorder ContentBorder
        {
            get { return this._contentBorder; }
            set
            {
                this._contentBorder = value;
                OnPropertyChanged("ContentBorder");
            }
        }

        [Browsable(false)]
        public DesignerFont _contentFont
        {
            get;
            set;
        }

        [Category("内容")]
        [DisplayName("内容字体")]
        [Description("内容字体")]
        public DesignerFont ContentFont
        {
            get { return this._contentFont; }
            set
            {
                this._contentFont = value;
                OnPropertyChanged("RowFont");
            }
        }



        #endregion

        #region 单元格

        [Browsable(false)]
        public VerticalAlignment _cellVerticalAlignment
        {
            get;
            set;
        }

        [Category("单元格相关")]
        [DisplayName("单元格垂直排版")]
        [Description("单元格垂直排版")]
        public VerticalAlignment CellVerticalAlignment
        {
            get { return this._cellVerticalAlignment; }
            set
            {
                this._cellVerticalAlignment = value;
                OnPropertyChanged("CellVerticalAlignment");
            }
        }
        [Browsable(false)]
        public HorizontalAlignment _cellHorizontalAlignment
        {
            get;
            set;
        }

        [Category("单元格相关")]
        [DisplayName("单元格水平排版")]
        [Description("单元格水平排版")]
        public HorizontalAlignment CellHorizontalAlignment
        {
            get { return this._cellHorizontalAlignment; }
            set
            {
                this._cellHorizontalAlignment = value;
                OnPropertyChanged("CellHorizontalAlignment");
            }
        }

        [Browsable(false)]
        public Brush _cellBackground
        {
            get;
            set;
        }

        [Category("单元格相关")]
        [DisplayName("单元格背景色")]
        [Description("单元格背景色")]
        public Brush CellBackground
        {
            get { return this._cellBackground; }
            set
            {
                this._cellBackground = value;
                OnPropertyChanged("CellBackground");
            }
        }

        [Browsable(false)]
        public DesignerBorder _cellBorder
        {
            get;
            set;
        }

        [Category("单元格相关")]
        [DisplayName("单元格边框")]
        [Description("单元格边框")]
        public DesignerBorder CellBorder
        {
            get { return this._cellBorder; }
            set
            {
                this._cellBorder = value;
                OnPropertyChanged("CellBorder");
            }
        }

        [Browsable(false)]
        public Thickness _cellPadding
        {
            get;
            set;
        }

        [Category("单元格相关")]
        [DisplayName("单元格内边距")]
        [Description("单元格内边距")]
        public Thickness CellPadding
        {
            get { return this._cellPadding; }
            set
            {
                this._cellPadding = value;
                OnPropertyChanged("CellPadding");
            }
        }

        [Browsable(false)]
        public Thickness _cellMargin
        {
            get;
            set;
        }

        [Category("单元格相关")]
        [DisplayName("单元格外边距")]
        [Description("单元格外边距")]
        public Thickness CellMargin
        {
            get { return this._cellMargin; }
            set
            {
                this._cellMargin = value;
                OnPropertyChanged("CellMargin");
            }
        }


        #endregion

        #region 数据
        [Browsable(false)]
        public DesignerDataSource _dataSource { get; set; }
        [Category("数据")]
        [DisplayName("数据源")]
        [Description("数据源设置")]
        public DesignerDataSource DataSource
        {
            get
            {
                return this._dataSource;
            }
            set
            {
                this._dataSource = value;
                OnPropertyChanged("DataSource");
            }
        }

            [Browsable(false)]
        public bool _autoGenerateColumns
        {
            get;
            set;
        }

        [Category("数据")]
        [DisplayName("数据源加载")]
        [Description("是否由数据源呈现数据")]
        public bool AutoGenerateColumns
        {
            get { return this._autoGenerateColumns; }
            set
            {
                this._autoGenerateColumns = value;
                OnPropertyChanged("AutoGenerateColumns");
            }
        }

        [Browsable(false)]
        public int _displayRowCount
        {
            get;
            set;
        }

        [Category("行相关")]
        [DisplayName("显示行数")]
        [Description("单页呈现行数")]
        public int DisplayRowCount
        {
            get { return this._displayRowCount; }
            set
            {
                if (value <= 0)
                    value = 1;
                this._displayRowCount = value;
                OnPropertyChanged("DisplayRowCount");
            }
        }

        [ReadOnly(true)]
        [Category("列相关")]
        [DisplayName("显示列数")]
        [Description("列数")]
        public int ColumnCount
        {
            get
            {
                if (this._columns == null)
                    return 0;
                else
                    return this._columns.Count;
            }
        }



        #region 列标题容器

        [Browsable(false)]
        public ObservableCollection<DesignerDataGridColumn> _columns { get; set; }

        [Category("列相关")]
        [DisplayName("列标题集合")]
        [Description("列标题集合")]
        public ObservableCollection<DesignerDataGridColumn> Columns
        {
            get { return this._columns; }
            set
            {
                this._columns = value;
                OnPropertyChanged("Columns");
            }
        }

        #endregion

        #region 时间

        [Browsable(false)]
        public int _paggingTimeSpan
        {
            get;
            set;
        }

        [Category("计时器")]
        [DisplayName("换页时间间隔")]
        [Description("当前页到下一页时间间隔")]
        public int PaggingTimeSpan
        {
            get { return this._paggingTimeSpan; }
            set
            {
                if (value < 1)
                    value = 50;
                this._paggingTimeSpan = value;
                OnPropertyChanged("PaggingTimeSpan");
            }
        }

        [Browsable(false)]
        public int _dataAccessTimeSpan
        {
            get;
            set;
        }

        [Category("计时器")]
        [DisplayName("数据访问时间间隔")]
        [Description("获取新数据时间间隔")]
        public int DataAccrssTimeSpan
        {
            get { return this._dataAccessTimeSpan; }
            set
            {
                if (value < 1000)
                    value = 1000;
                this._dataAccessTimeSpan = value;
                OnPropertyChanged("DataAccrssTimeSpan");
            }
        }

        #endregion

        #endregion


        #region 滚动条

        [Browsable(false)]
        public ScrollBarVisibility _horizontalScrollBarVisibility
        {
            get;
            set;
        }

        [Category("图表")]
        [DisplayName("水平滚动条可见性")]
        [Description("水平滚动条可见性")]
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get { return this._horizontalScrollBarVisibility; }
            set
            {
                this._horizontalScrollBarVisibility = value;
                OnPropertyChanged("HorizontalScrollBarVisibility");
            }
        }

        [Browsable(false)]
        public ScrollBarVisibility _verticalScrollBarVisibility
        {
            get;
            set;
        }

        [Category("图表")]
        [DisplayName("垂直滚动条可见性")]
        [Description("垂直滚动条可见性")]
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return this._verticalScrollBarVisibility; }
            set
            {
                this._verticalScrollBarVisibility = value;
                OnPropertyChanged("VerticalScrollBarVisibility");
            }
        }

        #endregion
    }
}
