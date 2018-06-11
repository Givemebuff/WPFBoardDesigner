using Board.DesignerModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Board.Resource
{
    public class DataSourceManager
    {
        public static IEnumerable<DesignerDataSource> GetDataSources()
        {           
            ObservableCollection<DesignerDataSource> dss = new ObservableCollection<DesignerDataSource>();            
            string curDirPath = Directory.GetCurrentDirectory() + @"\DataSources";
            //若不存在文件夹则先创建
            if (!Directory.Exists(curDirPath))
            {
                Directory.CreateDirectory(curDirPath);
            }
            DirectoryInfo di = new DirectoryInfo(curDirPath);
            FileInfo[] fis = di.GetFiles();
            foreach (FileInfo fi in fis)
            {
                string ext = fi.Extension.ToLower();
                if (ext.IndexOf("bds") == -1 && ext.IndexOf("xml") == -1)
                    continue;
                else
                {
                    try
                    {
                        string xml = File.ReadAllText(fi.FullName);
                        using (StringReader sr = new StringReader(xml))
                        {
                            XmlSerializer xmldes = new XmlSerializer(typeof(DesignerDataSource));
                            DesignerDataSource ds = xmldes.Deserialize(sr) as DesignerDataSource;
                            dss.Add(ds);
                        }  
                    }
                    catch (Exception e)
                    {
                        throw new Exception("读取DataSources时出错：" + e.Message);
                    }                  
                  
                }
            }
            return dss;
        }
    }
}
