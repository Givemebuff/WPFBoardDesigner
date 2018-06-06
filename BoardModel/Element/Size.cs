using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BoardDesigner.Model
{
    [Serializable]
    public class DesignerSize:DesignerElement
    {
        #region 长宽
        [Browsable(false)]
        [XmlAttribute("Width")]
        public double _width
        {
            get;
            set;
        }

        [Category("尺寸")]
        [DisplayName("宽度")]
        [Description("元素的宽度")]
        public double Width
        {
            get { return this._width; }
            set
            {
                this._width = value;
                OnPropertyChanged("Width");
            }
        }

        [Browsable(false)]
        [XmlAttribute("Height")]
        public double _height { get; set; }


        [Category("尺寸")]
        [DisplayName("高度")]
        [Description("元素的高度")]
        public double Height
        {
            get { return this._height; }
            set
            {
                this._height = value;
                OnPropertyChanged("Height");
            }
        }

        #endregion


        public DesignerSize() { }
        public DesignerSize(double width, double height) 
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
