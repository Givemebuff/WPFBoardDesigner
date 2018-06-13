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
    [Serializable]
    public class DesignerFont : DesignerElement
    {
        public DesignerFont()
        {
            this.Type = DesignerElementType.Font;
            FontColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            FontSize = 24;
            FontFamily = new FontFamily("微软雅黑");
            FontStyle = new System.Windows.FontStyle();
        }
        [Browsable(false)]
        [XmlIgnore]
        public double _fontSize
        {
            get;
            set;
        }
        [Category("字体")]
        [DisplayName("字体大小")]
        [Description("字体大小")]
        [XmlAttribute("FontSize")]
        public double FontSize
        {
            get { return this._fontSize; }
            set
            {
                this._fontSize = value;
                OnPropertyChanged("FontSize");
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Brush _fontColor
        {
            get;
            set;
        }

        [Category("字体")]
        [DisplayName("字体颜色")]
        [Description("字体的颜色")]
        [XmlIgnore]
        public Brush FontColor
        {
            get { return this._fontColor; }
            set
            {
                this._fontColor = value;
                OnPropertyChanged("FontColor");
            }
        }
        [XmlAttribute]
        public string XmlFontColor
        {
            get
            {
                if (this._fontColor == null)
                    return null;
                else
                    return this._fontColor.ToString();
            }
            set
            {
                this.FontColor = Board.XmlConverter.ColorConverter.XmlToBrush(value);
            }

        }

        [Browsable(false)]
        [XmlIgnore]
        public FontFamily _fontFamily
        {
            get;
            set;
        }
        [Category("字体")]
        [DisplayName("字体种类")]
        [Description("选用字体")]
        [XmlIgnore]
        public FontFamily FontFamily
        {
            get { return this._fontFamily; }
            set
            {
                this._fontFamily = value;
                OnPropertyChanged("FontFamily");
            }
        }
        [XmlAttribute("FontFamily")]
        public string XmlFontFamily 
        {
            get 
            {
                if (_fontFamily == null)
                    return null;
                else return  this._fontFamily.ToString();
            }
            set
            {
                this.FontFamily = new FontFamily(value);        
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public FontStyle _fontStyle
        {
            get;
            set;
        }
        [Category("字体")]
        [DisplayName("字体风格")]
        [Description("风格")]
        [XmlElement("FontStyle")]
        public FontStyle FontStyle
        {
            get { return this._fontStyle; }
            set
            {
                this._fontStyle = value;
                OnPropertyChanged("FontStyle");
            }
        }
        [Browsable(false)]
        [XmlIgnore]
        public FontWeight _fontWeight
        {
            get;
            set;
        }
        [Category("字体")]
        [DisplayName("字体粗细")]
        [Description("粗细")]
        [XmlElement("FontWeight")]
        public FontWeight FontWeight
        {
            get { return this._fontWeight; }
            set
            {
                this._fontWeight = value;
                OnPropertyChanged("FontWeight");
            }
        }
    }
}
