using Board.DesignerModel;
using Board.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Board.Converter
{
    public class NameToDataSourceConverter : IValueConverter
    {
        //Name to DataSource
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            else
            {
                return (value as DesignerDataSource).Name;
            }

        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            List<DesignerDataSource> dss = new List<DesignerDataSource>(DataBaseDataSourceManager.GetDataSources());
            string name = value.ToString();
            foreach (DesignerDataSource ds in dss)
            {
                if (name == ds.Name)
                    return ds;
            }

            throw new Exception("根据名称转换为数据源时出错，找不到相应数据源");
           
        }
    }
}
