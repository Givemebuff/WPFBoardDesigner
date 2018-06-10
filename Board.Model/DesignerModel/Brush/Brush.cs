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
    public class DesignerBrush : DesignerElement
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
        public DesignerBrush(string imagePath)
        {
            this.Type = DesignerElementType.Property;
            if (System.IO.File.Exists(imagePath))
                _backgoundImage = imagePath;
        }

        [Browsable(false)]
        public Brush _colorBrush
        {
            get;
            set;
        }
        [Category("视图")]
        [DisplayName("背景颜色")]
        [Description("背景颜色设置")]
        [XmlAttribute("BackgroundColor")]
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
        public string _backgoundImage
        {
            get;
            set;
        }
        [Category("视图")]
        [DisplayName("背景图片")]
        [Description("背景图片路径设置")]
        [XmlAttribute("BackgroundImage")]
        public string BackgoundImage
        {
            get { return this._backgoundImage; }
            set
            {
                if (value == this._backgoundImage)
                    return;
                this._backgoundImage = value;
                OnPropertyChanged("BackgoundImage");
                OnPropertyChanged("Background");
            }
        }
    }
}
