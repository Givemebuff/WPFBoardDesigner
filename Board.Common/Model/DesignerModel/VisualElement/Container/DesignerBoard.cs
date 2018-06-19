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
    [Serializable]
    [XmlType("Board")]    
    public class DesignerBoard:DesignerControl
    {       
        public DesignerBoard() 
        {
            this.Type = DesignerElementType.Control;
            this.VisualChildren = new ObservableCollection<DesignerControl>();
            this.BackChildren = new ObservableCollection<DesignerDataSource>();
        } 
        [ReadOnly(true)]
        [Category("前台控件")]
        [DisplayName("可视元素集合")]
        [Description("可视元素集合")]
        [XmlArray("VisualChildren"), XmlArrayItem("VisualControl")]
        public ObservableCollection<DesignerControl> VisualChildren
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [Category("后台控件")]
        [DisplayName("后台元素集合")]
        [Description("后台元素集合")]
        [XmlArray("BackChildren"), XmlArrayItem("BackControl")]
        public ObservableCollection<DesignerDataSource> BackChildren
        {
            get;
            set;
        }

        public void AddVisualControl(DesignerControl dc) 
        {
            if (VisualChildren.Contains(dc))
                return;
            else 
            {
                VisualChildren.Add(dc);
            }
        }

        public void RemoveVisualControl(DesignerControl dc) 
        {
            if (VisualChildren.Contains(dc))
                VisualChildren.Remove(dc);
            else
            {
                return;
            }
        }

        public void AddBackControl(DesignerDataSource ds)
        {
            if (BackChildren.Contains(ds))            
                return;
            else 
            {
                BackChildren.Add(ds);
            }
        }
        public void RemoveBackWorker(DesignerDataSource ds)
        {
            if (BackChildren.Contains(ds))
                BackChildren.Remove(ds);
            else
            {
                return;
            }
        }
    }


}
