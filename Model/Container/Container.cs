using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BoardDesigner.Model
{
    public class DesignerContainer:DesignerVisualElement
    {
        public DesignerContainer() { this.Type = DesignerElementType.Container; }
        [Browsable(false)]
        public object _content
        {
            get;
            set;
        }

        [Category("内容")]
        [DisplayName("容器内容")]
        [Description("内容元素")]
        [XmlAttribute("Content")]
        public object Content
        {
            get { return this._content; }
            set 
            {
                this._content = value;
                OnPropertyChanged("Content");
            }
        }
    }
}
