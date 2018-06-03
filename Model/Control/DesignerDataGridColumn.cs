using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoardDesigner.Model
{
    public class DesignerDataGridColumn : DesignerLabel
    {
        public DesignerDataGridColumn() 
        {
            this.Font = new DesignerFont() { FontSize = 18 };
            ColumnWidth = new GridLength(1, GridUnitType.Star);
            Text = "Column";
            HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
        }


        [Browsable(false)]
        public GridLength _columnWidth { get; set; }

        [Category("列基础")]
        [DisplayName("宽（占比）")]
        [Description("设置宽度")]
        public GridLength ColumnWidth
        {
            get { return this._columnWidth; }
            set
            {
                this._columnWidth = value;
                OnPropertyChanged("ColumnWidth");
            }
        }


        [Browsable(false)]
        public string _bindingName { get; set; }

        [Category("列基础")]
        [DisplayName("数据源列名")]
        [Description("设置绑定数据源名")]
        public string BindingName
        {
            get { return this._bindingName; }
            set
            {
                this._bindingName = value;
                OnPropertyChanged("BindingName");
            }
        }

        #region 内容



        #endregion

        #region 排版
        [Browsable(false)]
        public HorizontalAlignment _horizontalContentAlignment
        {
            get;
            set;
        }

        [Category("图表")]
        [DisplayName("列标题单元格水平排版")]
        [Description("列标题单元格水平排版")]
        public HorizontalAlignment HorizontalContentAlignment
        {
            get { return this._horizontalContentAlignment; }
            set
            {
                this._horizontalContentAlignment = value;
                OnPropertyChanged("HorizontalContentAlignment");
            }
        }

        [Browsable(false)]
        public VerticalAlignment _verticalContentAlignment
        {
            get;
            set;
        }

        [Category("图表")]
        [DisplayName("列标题单元格垂直排版")]
        [Description("列标题单元格垂直排版")]
        public VerticalAlignment VerticalContentAlignment
        {
            get { return this._verticalContentAlignment; }
            set
            {
                this._verticalContentAlignment = value;
                OnPropertyChanged("VerticalContentAlignment");
            }
        }

        #endregion
    }
}
