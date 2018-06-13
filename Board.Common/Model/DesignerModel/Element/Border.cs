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
    [XmlType("Border")]
    public class DesignerBorder : DesignerElement
    {
        public DesignerBorder() 
        {
            this.Type = DesignerElementType.Border;
            this.BorderThickness = new Thickness(1);
            this.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        #region 粗细
        [Browsable(false)]
        [XmlIgnore]
        public Thickness _borderThickness { get; set; }

        [Category("边框")]
        [DisplayName("粗细")]
        [Description("各边边框粗细。顺序：左上右下")]
        [XmlElement("Thickness")]
        public Thickness BorderThickness 
        {
            get 
            { 
                return this._borderThickness;
            }
            set 
            {
                this._borderThickness = value;
                OnPropertyChanged("BorderThickness");
            }
        }


        #endregion

        #region 画刷
        [Browsable(false)]
        [XmlIgnore]
        public Brush _borderBrush
        {
            get;
            set;
        }
        [Category("边框")]
        [DisplayName("颜色")]
        [Description("边框整体颜色")]
        [XmlIgnore]        
        public Brush BorderBrush 
        {
            get { return this._borderBrush; }
            set
            {
                this._borderBrush = value;
                OnPropertyChanged("BorderBrush");
            }
        }
        [XmlAttribute("BorderBrush")]
        public string XmlBorderBrush 
        {
            get { return this._borderBrush.ToString(); }
            set 
            {
                this.BorderBrush = Board.XmlConverter.ColorConverter.XmlToBrush(value);
            }
        }

        #endregion

        #region 圆角大小
        [XmlIgnore]
        [Browsable(false)]
        public double _cornerRadius
        {
            get;set;
        }

        [Category("边框")]
        [DisplayName("圆角")]
        [Description("圆角半径大小")]
        [XmlAttribute("CornerRadius")]
        public double CornerRadius 
        {
            get { return this._cornerRadius; }
            set 
            {
                this._cornerRadius = value;
                OnPropertyChanged("CornerRadius");
            }
        }

        #endregion

    }
}
