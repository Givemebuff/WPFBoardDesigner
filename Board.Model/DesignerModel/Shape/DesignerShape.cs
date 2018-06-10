using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Board.DesignerModel
{ [Serializable]
    public class DesignerShape : DesignerVisualContentElement
    {
       
        public DesignerShape() { this.Type = DesignerElementType.Shape; }
        [Browsable(false)]
        public Path _data
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [Category("图形")]
        [DisplayName("路径")]
        [Description("图形元素绘制路径")]
        [XmlAttribute("Path")]
        public Path Path
        {
            get
            {
                return this._data;
            }
            set
            {
                this._data = value;
                OnPropertyChanged("Data");
            }
        }
    }
}
