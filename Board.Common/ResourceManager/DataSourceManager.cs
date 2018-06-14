using Board.DesignerModel;
using Indusfo.Common;
using Indusfo.Data.DataAccessBaseLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Board.Resource
{
    public class DataSourceManager
    {  
        public DataSourceManager()
        {


        }

        //数据源集合
        static Dictionary<string, DesignerDataSource> _dataSourceList { get; set; }
        public static Dictionary<string, DesignerDataSource> DataSourceList
        {
            get
            {
                if (_dataSourceList == null)
                    _dataSourceList = new Dictionary<string, DesignerDataSource>();
                return _dataSourceList;
            }
        }
        static Dictionary<string, object> _datasList { get; set; }
        public static Dictionary<string, object> DatasList
        {
            get
            {
                if (_datasList == null)
                    _datasList = new Dictionary<string, object>();
                return _datasList;
            }
        }
        //注册数据源
        public static void Register(string dataSourceKey, DesignerDataSourceType type, DesignerDataSource dataSource)
        {
            if (string.IsNullOrEmpty(dataSourceKey))
                dataSourceKey = dataSource.Name;
            switch (type)
            {
                case DesignerDataSourceType.DataBase:
                    DesignerDataBaseDataSource dbds = dataSource as DesignerDataBaseDataSource;
                    UpdateDataSource(dataSourceKey, dbds);
                    break;
                case DesignerDataSourceType.LocalFile:
                    DesignerLocalFileDataSource lfds = dataSource as DesignerLocalFileDataSource;
                    UpdateDataSource(dataSourceKey, lfds);
                    break;
                case DesignerDataSourceType.RemoteURL:
                    DesignerRemoteURIDataSource ruds = dataSource as DesignerRemoteURIDataSource;
                    UpdateDataSource(dataSourceKey, ruds);
                    break;
                case DesignerDataSourceType.StaticText:
                    DesignerStaticTextDataSource stds = dataSource as DesignerStaticTextDataSource;
                    UpdateDataSource(dataSourceKey, stds);
                    break;
                case DesignerDataSourceType.Default:
                    UpdateDataSource(dataSourceKey, dataSource);
                    break;

            }
        }
        /// <summary>
        /// 根据Key找到数据源
        /// </summary>
        /// <param name="dataSourceKey"></param>
        /// <returns></returns>
        public static DesignerDataSource GetDataSourceByKey(string dataSourceKey)
        {
            if (DataSourceList.Keys.Contains(dataSourceKey))
            {
                return DataSourceList[dataSourceKey];
            }
            else
                throw new NotImplementedException();
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        private static void ExcuteDataSource(string key)
        {
            DesignerDataSource ds = GetDataSourceByKey(key);
            try
            {
                switch (ds.DataSourceType)
                {
                    case DesignerDataSourceType.DataBase:
                        object dbData = ExcuteDataBaseDataSource(ds as DesignerDataBaseDataSource);
                        UpdateData(key, dbData);
                        break;

                    case DesignerDataSourceType.LocalFile:
                        object lfData = ExcuteLocalFileDataSource(ds as DesignerLocalFileDataSource);
                        UpdateData(key, lfData);
                        break;

                    case DesignerDataSourceType.RemoteURL:
                        object ruData = ExcuteRemoteURIDataSource(ds as DesignerRemoteURIDataSource);
                        UpdateData(key, ruData);
                        break;

                    default:
                        throw new Exception("未知的数据源执行方法");
                }
            }
            catch (Exception e)
            {
                throw new Exception("执行数据源:" + ds.Name + "时发生异常，详细信息：" + e.Message);
            }

        }

        public static async Task<object> GetDataAsync(string key)
        {
            return await GetData(key);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<object> GetData(string key)
        {
            if (DatasList.Keys.Contains(key))
            {
                await Task.Run(() => ExcuteDataSource(key));//执行
                return DatasList[key];
            }
            else
                throw new NotImplementedException();
        }

        /// <summary>
        /// 数据源更新数据方法
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        private static void UpdateData(string key, object data)
        {
            if (DatasList.Keys.Contains(key))
            {
                DatasList[key] = data;
            }
            else
            {
                DatasList.Add(key, data);
            }
        }

        /// <summary>
        /// 数据源更新方法
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ds"></param>
        private static void UpdateDataSource(string key, DesignerDataSource ds)
        {
            if (DataSourceList.Keys.Contains(key))
            {
                DataSourceList[key] = ds;
            }
            else
            {
                DataSourceList.Add(key, ds);
            }
            UpdateData(key, null);
        }

        #region 执行方法
        /// <summary>
        /// 使用Indusfo.DAL 返回DataTable
        /// </summary>
        /// <param name="designerDataBaseDataSource"></param>
        /// <returns></returns>
        private static object ExcuteDataBaseDataSource(DesignerDataBaseDataSource ds)
        {
            using (SqlExcuter se = new SqlExcuter(DataBaseType.SqlServer, ds.ConnectionString))
            {
                return se.ExecuteSelectSql(ds.SqlString);
            }
        }
        /// <summary>
        /// 直接使用URI读取远程数据,返回二进制流数据
        /// </summary>
        /// <param name="designerRemoteURIDataSource"></param>
        /// <returns></returns>
        private static object ExcuteRemoteURIDataSource(DesignerRemoteURIDataSource designerRemoteURIDataSource)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 文件流读取文件，返回二进制流数据
        /// </summary>
        /// <param name="designerLocalFileDataSource"></param>
        /// <returns></returns>
        private static object ExcuteLocalFileDataSource(DesignerLocalFileDataSource designerLocalFileDataSource)
        {
            throw new NotImplementedException();
        }

        #endregion
        //public static T GetData<T>(string key)
        //{
        //    DesignerDataSource ds = GetDataSourceByKey(key);

        //}

    }
    /// <summary>
    /// 数据库数据源
    /// </summary>
    public class DataBaseDataSourceManager
    {
        #region 
        public static IEnumerable<DesignerDataBaseDataSource> GetDataBaseDataSources()
        {
            ObservableCollection<DesignerDataBaseDataSource> dss = new ObservableCollection<DesignerDataBaseDataSource>();
            string curDirPath = Directory.GetCurrentDirectory() + @"\DataSources\DataBase";
            //若不存在文件夹则先创建
            if (!Directory.Exists(curDirPath))
            {
                Directory.CreateDirectory(curDirPath);
            }
            DirectoryInfo di = new DirectoryInfo(curDirPath);
            FileInfo[] fis = di.GetFiles();
            try
            {
                foreach (FileInfo fi in fis)
                {
                    string ext = fi.Extension.ToLower();
                    if (ext.IndexOf("bds") == -1 && ext.IndexOf("xml") == -1)
                        continue;
                    else
                    {

                        string xml = File.ReadAllText(fi.FullName);
                        using (StringReader sr = new StringReader(xml))
                        {
                            XmlSerializer xmldes = new XmlSerializer(typeof(DesignerDataBaseDataSource));
                            DesignerDataBaseDataSource ds = xmldes.Deserialize(sr) as DesignerDataBaseDataSource;
                            dss.Add(ds);
                        }


                    }
                }
                return dss;
            }
            catch (Exception e)
            {
                throw new Exception("读取DataSources时出错：" + e.Message);
            }
            
        }
        public static string UpdateDataBaseDataSource(DesignerDataBaseDataSource ds) 
        {
            string path = Directory.GetCurrentDirectory() + @"\DataSources\DataBase\"+ds.Name+".bds";
            try 
            {
                MemoryStream Stream = new MemoryStream();
                XmlSerializer xml = new XmlSerializer(typeof(DesignerDataBaseDataSource));
                //序列化对象  
                xml.Serialize(Stream, ds);
                Stream.Position = 0;
                StreamReader sr = new StreamReader(Stream);
                string str = sr.ReadToEnd();
                //存到文件
               
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs); // 创建写入流
                sw.Write(str);
                sw.Close(); 
                sr.Dispose();
                Stream.Dispose();

                //注册该数据源
                DataSourceManager.Register(ds.Name, ds.DataSourceType, ds);


                return path;
            }
            catch (Exception e)
            {
                throw new Exception("增加数据库数据源时出错：" + e.Message);
            }

        }

        #endregion       
    }
    /// <summary>
    /// 静态文本数据源
    /// </summary>
    public class StaticTextDataSourceManager 
    {
        public static IEnumerable<DesignerStaticTextDataSource> GetStaticTextDataSources()
        {
            ObservableCollection<DesignerStaticTextDataSource> dss = new ObservableCollection<DesignerStaticTextDataSource>();
            string curDirPath = Directory.GetCurrentDirectory() + @"\DataSources\StaticText";
            //若不存在文件夹则先创建
            if (!Directory.Exists(curDirPath))
            {
                Directory.CreateDirectory(curDirPath);
            }
            DirectoryInfo di = new DirectoryInfo(curDirPath);
            FileInfo[] fis = di.GetFiles();
            try
            {
                foreach (FileInfo fi in fis)
                {
                    string ext = fi.Extension.ToLower();
                    if (ext.IndexOf("bds") == -1 && ext.IndexOf("xml") == -1)
                        continue;
                    else
                    {

                        string xml = File.ReadAllText(fi.FullName);
                        using (StringReader sr = new StringReader(xml))
                        {
                            XmlSerializer xmldes = new XmlSerializer(typeof(DesignerStaticTextDataSource));
                            DesignerStaticTextDataSource ds = xmldes.Deserialize(sr) as DesignerStaticTextDataSource;
                            dss.Add(ds);
                        }
                    }
                }
                return dss;
            }
            catch (Exception e)
            {
                throw new Exception("读取静态文本数据源时出错：" + e.Message);
            }

        }
        public static string UpdateStaticTextDataSource(DesignerStaticTextDataSource ds)
        {
            string path = Directory.GetCurrentDirectory() + @"\DataSources\StaticText\" + ds.Name + ".bds";
            try
            {
                MemoryStream Stream = new MemoryStream();
                XmlSerializer xml = new XmlSerializer(typeof(DesignerStaticTextDataSource));
                //序列化对象  
                xml.Serialize(Stream, ds);
                Stream.Position = 0;
                StreamReader sr = new StreamReader(Stream);
                string str = sr.ReadToEnd();
                //存到文件

                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs); // 创建写入流
                sw.Write(str);
                sw.Close();
                sr.Dispose();
                Stream.Dispose();

                //注册该数据源
                DataSourceManager.Register(ds.Name, ds.DataSourceType, ds);
                return path;
            }
            catch (Exception e)
            {
                throw new Exception("增加静态文本数据源时出错：" + e.Message);
            }

        }
    }
}
