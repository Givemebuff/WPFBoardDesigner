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
    [XmlType("VisualControl")]
    [XmlInclude(typeof(DesignerLabel))]  
    [XmlInclude(typeof(DesignerBoard))]   
    [XmlInclude(typeof(DesignerTable))]
    [XmlInclude(typeof(DesignerImage))]
    [XmlInclude(typeof(DesignerGif))]
    [XmlInclude(typeof(DesignerMedia))]
    [XmlInclude(typeof(DesignerChart))]  
    public class DesignerControl : DesignerVisualElement
    {
        public DesignerControl()
        {
            this.Type = DesignerElementType.Control;
            this.Background = new DesignerBrush(Color.FromArgb(0, 255, 255, 255));
            this.Border = new DesignerBorder();
        }


        #region 背景

        [Browsable(false)]
        [XmlIgnore]
        public DesignerBrush _background { get; set; }

        [Category("视图")]
        [DisplayName("背景")]
        [Description("背景设置")]
        [XmlElement("Background")]
        public DesignerBrush Background
        {
            get { return this._background; }
            set
            {
                this._background = value;
                OnPropertyChanged("Background");
            }
        }

        #endregion

        #region 排版

        [Browsable(false)]
        [XmlIgnore]
        public VerticalAlignment _verticalAlignment { get; set; }
        [Category("排版")]
        [DisplayName("垂直方向")]
        [Description("控件垂直方向排版设置")]
        [XmlAttribute("VerticalAlignment")]
        public VerticalAlignment VerticalAlignment
        {
            get { return this._verticalAlignment; }
            set
            {
                this._verticalAlignment = value;
                OnPropertyChanged("VerticalAlignment");
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public VerticalAlignment _verticalContentAlignment { get; set; }
        [Category("排版")]
        [DisplayName("内容垂直方向")]
        [Description("控件内容垂直方向排版设置")]
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

        [Browsable(false)]
        [XmlIgnore]
        public HorizontalAlignment _horizontalAlignment { get; set; }
        [Category("排版")]
        [DisplayName("垂直方向")]
        [Description("控件垂直方向排版设置")]
        [XmlAttribute("HorizontalAlignment")]
        public HorizontalAlignment HorizontalAlignment
        {
            get { return this._horizontalAlignment; }
            set
            {
                this._horizontalAlignment = value;
                OnPropertyChanged("HorizontalAlignment");
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public HorizontalAlignment _horizontalContentAlignment { get; set; }
        [Category("排版")]
        [DisplayName("内容垂直方向")]
        [Description("控件内容垂直方向排版设置")]
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

        #endregion

        #region 边框

        [Browsable(false)]
        [XmlIgnore]
        public DesignerBorder _border
        {
            get;
            set;
        }
        [Category("边框")]
        [DisplayName("边框")]
        [Description("设置边框")]
        [XmlElement("Border")]
        public DesignerBorder Border
        {
            get {               
                return this._border;
            }
            set
            {
                this._border = value;
                OnPropertyChanged("Border");
            }
        }

        #endregion


    }
}
