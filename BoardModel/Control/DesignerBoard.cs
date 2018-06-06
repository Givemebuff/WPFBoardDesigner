using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDesigner.Model
{
    [Serializable]
    public class DesignerBoard:DesignerControl
    {
        
        public DesignerBoard() 
        {
            this.Type = DesignerElementType.Control;
            this.Children = new ObservableCollection<DesignerControl>();
        } 
        [ReadOnly(true)]
        [Category("内容")]
        [DisplayName("子元素集合")]
        [Description("子元素集合")]
        public ObservableCollection<DesignerControl> Children
        {
            get;
            set;
        }
    }
}
