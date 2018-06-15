using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Visifire.Charts;

namespace Board.DataHelper
{
    public class ControlHelper
    {
        public static T GetParentObject<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);

            while (parent != null)
            {
                if (parent is T && (((T)parent).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)parent;
                }

                // 在上一级父控件中没有找到指定名字的控件，就再往上一级找
                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        public static IEnumerable<DataPoint> DataTableToDataPoints(DataTable data,string xBindName,string yBindName) 
        {
            if (data == null || data.Rows.Count <= 0)
                return null;

            if (string.IsNullOrEmpty(xBindName) || string.IsNullOrEmpty(yBindName))
                return null;
            List<DataPoint> list = new List<DataPoint>();
            foreach (DataRow r in data.Rows) 
            {
                DataPoint dp = new DataPoint();              
                
                dp.XValue = r[xBindName];                
              
                dp.YValue =Convert.ToDouble(r[yBindName]);

                list.Add(dp);
            }
            return list;
        }
    }
}
