using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Board.DesignerModel
{
    [Serializable]
    public class DesignerBrush : DesignerProperty
    {
        public DesignerBrush()
        {
            this.Type = DesignerElementType.Property;
        }
        public DesignerBrush(Color color)
        {
            this.Type = DesignerElementType.Property;
            ColorBrush = new SolidColorBrush(color);
        }
        [Browsable(false)]
        [XmlIgnore]
        public Brush _colorBrush
        {
            get;
            set;
        }
        [XmlIgnore]
        [Category("视图")]
        [DisplayName("背景颜色")]
        [Description("背景颜色设置")]
        
        public Brush ColorBrush
        {
            get { return this._colorBrush; }
            set
            {
                this._colorBrush = value;
                OnPropertyChanged("ColorBrush");
                OnPropertyChanged("Background");
            }
        }
        [Browsable(false)]
        [XmlAttribute("Color")]
        public string XmlColorBrush
        {
            get {
                if (this._colorBrush == null)
                    return null;
                return this._colorBrush.ToString();
            }
            set 
            {
                this.ColorBrush = Board.XmlConverter.ColorConverter.XmlToBrush(value) as Brush;
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public string _backgoundImage
        {
            get;
            set;
        }
        [Category("视图")]
        [DisplayName("背景图片")]
        [Description("背景图片路径设置")]
        [XmlAttribute("Path")]
        public string BackgoundImage
        {
            get { return this._backgoundImage; }
            set
            {               
                this._backgoundImage = value;
                OnPropertyChanged("BackgoundImage");
                OnPropertyChanged("Background");
            }
        }

        public override string ToString()
        {
            if (this._backgoundImage != null) 
            {
                return this._backgoundImage;
            }
            else if (this._colorBrush != null)
            {
                return _colorBrush.ToString();
            }
            return null;
        }
    }
}
