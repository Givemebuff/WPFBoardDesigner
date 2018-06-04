using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BoardDesigner.Model
{
    [Serializable]
    public class DesignerFont : DesignerElement
    {
        public DesignerFont()
        {
            this.Type = DesignerElementType.Font;
            FontColor = new SolidColorBrush(Color.FromRgb(0, 0, 0)) ;
            FontSize = 24;
            FontFamily = new FontFamily("微软雅黑");
            FontStyle = new System.Windows.FontStyle();
        }
        [Browsable(false)]
        public double _fontSize
        {
            get;
            set;
        }
        [Category("字体")]
        [DisplayName("字体大小")]
        [Description("字体大小")]
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
        public Brush _fontColor
        {
            get;
            set;
        }

        [Category("字体")]
        [DisplayName("字体颜色")]
        [Description("字体的颜色")]
        public Brush FontColor
        {
            get { return this._fontColor; }
            set
            {
                this._fontColor = value;
                OnPropertyChanged("FontColor");
            }
        }

        [Browsable(false)]
        public FontFamily _fontFamily
        {
            get;
            set;
        }
        [Category("字体")]
        [DisplayName("字体种类")]
        [Description("选用字体")]
        public FontFamily FontFamily
        {
            get { return this._fontFamily; }
            set
            {
                this._fontFamily = value;
                OnPropertyChanged("FontFamily");
            }
        }

        [Browsable(false)]
        public FontStyle _fontStyle
        {
            get;
            set;
        }
        [Category("字体")]
        [DisplayName("字体风格")]
        [Description("风格")]
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
        public FontWeight _fontWeight
        {
            get;
            set;
        }
        [Category("字体")]
        [DisplayName("字体粗细")]
        [Description("粗细")]
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
