using Infragistics.Windows.DataPresenter;
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

namespace BoardDesigner.Model
{
    public class DesignerTable : DesignerControl
    {
        public DesignerTable()
        {
            this.Type = DesignerElementType.Table;
        }


        #region 筛选行

        [Browsable(false)]
        public Brush _alternatingRowBackground
        {
            get;
            set;
        }

        [Category("图表")]
        [DisplayName("筛选行的背景色")]
        [Description("筛选行的背景色")]
        public Brush AlternatingRowBackground
        {
            get { return this._alternatingRowBackground; }
            set
            {
                this._alternatingRowBackground = value;
                OnPropertyChanged("AlternatingRowBackground");
            }
        }

        [Browsable(false)]
        public int _alternationCount
        {
            get;
            set;
        }

        [Category("图表")]
        [DisplayName("筛选行条件")]
        [Description("筛选行条件")]
        public int AlternationCount
        {
            get { return this._alternationCount; }
            set
            {
                this._alternationCount = value;
                OnPropertyChanged("AlternationCount");
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

        [Category("图表")]
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

        [Category("图表")]
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

        [Category("图表")]
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

        [Category("图表")]
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

        [Category("图表")]
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

        [Category("图表")]
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

        [Category("图表")]
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

        [Category("图表")]
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

        [Category("图表")]
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

        [Category("图表")]
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

        [Category("图表")]
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

        [Category("图表")]
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

        [Category("图表")]
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

        [Browsable(false)]
        public double _rowHeight
        {
            get;
            set;
        }

        [Category("图表")]
        [DisplayName("内容行高")]
        [Description("内容行高")]
        public double RowHeight
        {
            get { return this._rowHeight; }
            set
            {
                this._rowHeight = value;
                OnPropertyChanged("RowHeight");
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

        [Category("图表")]
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

        #endregion

        #region 数据

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

        #region 列标题容器
        public ObservableCollection<DesignerDataGridColumn> _columns { get; set; }
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
