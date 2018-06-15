using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Board.DesignerModel
{
    [XmlType("Column")]
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
        [XmlIgnore]
        public GridLength _columnWidth { get; set; }

        [Category("列基础")]
        [DisplayName("宽（占比）")]
        [Description("设置宽度")]
        [XmlElement("Width")]
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
        [XmlIgnore]
        public string _bindingName { get; set; }

        [Category("列基础")]
        [DisplayName("数据源列名")]
        [Description("设置绑定数据源名")]
        [XmlElement("BindingName")]
        public string BindingName
        {
            get { return this._bindingName; }
            set
            {
                this._bindingName = value;
                OnPropertyChanged("BindingName");
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Brush _columnBackground { get; set; }

        [Category("列标题相关")]
        [DisplayName("列标题背景")]
        [Description("设置列标题背景")]
  [XmlIgnore]
        public Brush ColumnBackground
        {
            get { return this._columnBackground; }
            set
            {
                this._columnBackground = value;
                OnPropertyChanged("ColumnBackground");
            }
        }
         [Browsable(false)]
        [XmlAttribute("ColumnBackground")]
        public string XmlColumnBackground
        {
            get
            {
                if (this._columnBackground == null)
                    return null;
                return this._columnBackground.ToString();
            }
            set
            {
                this.ColumnBackground = Board.XmlConverter.ColorConverter.XmlToBrush(value) as Brush;
            }
        }

        #region 排版
        [Browsable(false)]
        [XmlIgnore]
        public HorizontalAlignment _horizontalContentAlignment
        {
            get;
            set;
        }

        [Category("图表")]
        [DisplayName("列标题单元格水平排版")]
        [Description("列标题单元格水平排版")]
        [XmlAttribute("HorizontalContentAlignment")]
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
        [XmlIgnore]
        public VerticalAlignment _verticalContentAlignment
        {
            get;
            set;
        }

        [Category("图表")]
        [DisplayName("列标题单元格垂直排版")]
        [Description("列标题单元格垂直排版")]
        [XmlAttribute("VerticalContentAlignment")]
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
