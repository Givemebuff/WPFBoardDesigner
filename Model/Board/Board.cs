using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BoardDesigner.Model
{
    public class Board:DesignerVisualElement
    {
        [Browsable(false)]
        public object _value { get; set; }

        [ReadOnly(false)]
        [Category("内容")]
        [DisplayName("内容")]
        [Description("内容元素包含的值")]
       
        public object Content
        {
            get { return this._value; }
            set
            {
                this._value = value;
                OnPropertyChanged("Value");
            }
        }
    }
}
