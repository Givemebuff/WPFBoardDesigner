using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Board.DesignerModel
{   
    [XmlType("Board")]    
    public class DesignerBoard:DesignerControl
    {       
        public DesignerBoard() 
        {
            this.Type = DesignerElementType.Control;
            this.Children = new ObservableCollection<DesignerControl>();
            this.BackWorkers = new ObservableCollection<DesignerDataSource>();
        } 
        [ReadOnly(true)]
        [Category("前台控件")]
        [DisplayName("可视元素集合")]
        [Description("可视元素集合")]   
        [XmlArray("Children"),XmlArrayItem("Control")]
        public ObservableCollection<DesignerControl> Children
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [Category("后台控件")]
        [DisplayName("后台元素集合")]
        [Description("后台元素集合")]
        [XmlArray("BackChildren"), XmlArrayItem("BackControl")]
        public ObservableCollection<DesignerDataSource> BackWorkers
        {
            get;
            set;
        }

        public void AddBackWorker(DesignerDataSource ds)
        {
            if (BackWorkers.Contains(ds))
                return;
            else 
            {
                BackWorkers.Add(ds);
            }
        }
    }


}
