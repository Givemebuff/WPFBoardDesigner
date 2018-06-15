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
using System.Windows.Threading;
using System.Xml.Serialization;


namespace Board.Resource
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="DesignerDataSource"></typeparam>
    /// <typeparam name="TimeSpan"></typeparam>
    /// <typeparam name="Data"></typeparam>
    public class DataSouceDictionary<TKey, TDesignerDataSource, TTimeSpan, TDispatcherTimer, TData> : 
        DependencyObject       
        where TDesignerDataSource : DesignerDataSource        
        where TDispatcherTimer:DispatcherTimer
       
    {

        public DataSouceDictionary() 
        {
            
        }
      
        #region 集合    

     
            

        #region Key-数据源
        Dictionary<string, DesignerDataSource> _dataSourceList { get; set; }
        public Dictionary<string, DesignerDataSource> DataSourceList
        {
            get
            {
                if (_dataSourceList == null)
                    _dataSourceList = new Dictionary<string, DesignerDataSource>();
                return _dataSourceList;
            }
        }

        #endregion  
       
        #region Key-Data

        Dictionary<string, object> _dataList { get; set; }
        public Dictionary<string, object> DataList
        {
            get
            {
                if (_dataList == null)
                    _dataList = new Dictionary<string, object>();
                return _dataList;
            }
        }

        #endregion  

        #region Key-TimeSpan

        Dictionary<string, int> _timeSpanList { get; set; }
        public Dictionary<string, int> TimeSpanList
        {
            get
            {
                if (_timeSpanList == null)
                    _timeSpanList = new Dictionary<string, int>();
                return _timeSpanList;
            }
        }

        #endregion

        #region Key-计时器

        Dictionary<string, DispatcherTimer> _timers { get; set; }
        public Dictionary<string, DispatcherTimer> Timers
        {
            get
            {
                if (_timers == null)
                    _timers = new Dictionary<string, DispatcherTimer>();
                return _timers;
            }
        }

        #endregion

        #endregion

        #region 操作

        #region DataSource

        public void AddOrUpdateDataSource(string key, DesignerDataSource ds) 
        {
            if (DataSourceList.Keys.Contains(key))
            {
                DataSourceList[key] = ds;              
            }
            else
            {
                DataSourceList.Add(key, ds);
            }
        }

        public void RemoveDataSource(string key)
        {
            if (DataSourceList.Keys.Contains(key))
            {
                DataSourceList.Remove(key);
            }
            else
            {                
                return;
            }
        }

        #endregion

        #region 计时器

        public void AddOrUpdateTimer(string key, DispatcherTimer dt)
        {
            if (Timers.Keys.Contains(key))
            {
                Timers[key] = dt;
            }
            else
            {
                Timers.Add(key, dt);
                
            }
        }

        public void RemoveTimer(string key)
        {
            if (Timers.Keys.Contains(key))
            {
                Timers.Remove(key);
            }
            else
            {
                return;
            }
        }

        #endregion

        #region 时间间隔

        public void AddOrUpdateTimeSpan(string key, int ts)
        {
            if (TimeSpanList.Keys.Contains(key))
            {
                TimeSpanList[key] = ts;
            }
            else
            {
                TimeSpanList.Add(key, ts); 
            }
        }

        public void RemoveTimeSpan(string key)
        {
            if (TimeSpanList.Keys.Contains(key))
            {
                TimeSpanList.Remove(key);
            }
            else
            {
                return;
            }
        }

        #endregion

        #region 数据

        public void AddOrUpdateData(string key, object data)
        {
            if (DataList.Keys.Contains(key))
            {
                DataList[key] = data; 
            }
            else
            {
                DataList.Add(key, data);
            }
        }

        public void RemoveData(string key)
        {
            if (DataList.Keys.Contains(key))
            {
                DataList.Remove(key);
            }
            else
            {
                return;
            }
        }

        #endregion

        #region Add
        public void AddOrUpdate(string key, DesignerDataSource ds, int timeSpan, DispatcherTimer timer, object data) 
        {
            AddOrUpdateDataSource(key, ds);
            AddOrUpdateTimeSpan(key, timeSpan);
            AddOrUpdateTimer(key, timer);
            AddOrUpdateData(key, data);
        }

        #endregion

        public void Remove(string key)
        {
            RemoveDataSource(key);
            RemoveTimeSpan(key);
            RemoveTimer(key);
            RemoveData(key);
        }


        #endregion
    }
    public class DataSourceManager 
    {  
        static DataSouceDictionary<string, DesignerDataSource, int, DispatcherTimer, object> _list { get; set; }
        public static DataSouceDictionary<string, DesignerDataSource, int, DispatcherTimer, object> List
        {
            get
            {
                if (_list == null)
                    _list = new DataSouceDictionary<string, DesignerDataSource, int, DispatcherTimer, object>();
                return _list;
            }
        }    

        /// <summary>
        /// 注册数据源
        /// </summary>
        /// <param name="dataSourceKey"></param>
        /// <param name="type"></param>
        /// <param name="dataSource"></param>
        public static void Register(string dataSourceKey, DesignerDataSourceType type, DesignerDataSource dataSource)
        {
            if (string.IsNullOrEmpty(dataSourceKey))
                dataSourceKey = dataSource.Name;
            List.AddOrUpdate(dataSourceKey, dataSource, dataSource.TimeSpan < 0 ? 0 : dataSource.TimeSpan,new DispatcherTimer(),null);
        }

        /// <summary>
        /// 注销数据源
        /// </summary>
        /// <param name="dataSourceKey"></param>
        public static void Cancel(string dataSourceKey) 
        {
            if (string.IsNullOrEmpty(dataSourceKey))
                return;
            List.Remove(dataSourceKey);
        }

        /// <summary>
        /// 根据Key找到数据源
        /// </summary>
        /// <param name="dataSourceKey"></param>
        /// <returns></returns>
        public static DesignerDataSource GetDataSource(string dataSourceKey)
        {
            if (List.DataSourceList.Keys.Contains(dataSourceKey))
            {
                return List.DataSourceList[dataSourceKey];
            }
            else
                throw new NotImplementedException();
        }

        public static void InitTimer(string key)
        {
            DispatcherTimer timer = List.Timers[key];
            timer.Interval = new TimeSpan(0,0,0,0,List.TimeSpanList[key]);
            timer.Tick += new EventHandler((s, e) =>
            {
                 ExcuteDataSource(key);
                 if (List.TimeSpanList[key] <= 0)
                     timer.Stop();
            });
        }

        public static void BeginTimer(string key)
        {
            DispatcherTimer timer = List.Timers[key];
            timer.Start();
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        private async static void ExcuteDataSource(string key)
        {
            DesignerDataSource ds = GetDataSource(key);
            try
            {
                switch (ds.DataSourceType)
                {
                    case DesignerDataSourceType.DataBase:
                        DataTable dbData = await ExcuteDataBaseDataSource(ds as DesignerDataBaseDataSource);
                        List.AddOrUpdateData(key, dbData);
                        break;

                    case DesignerDataSourceType.LocalFile:
                        object lfData = await ExcuteLocalFileDataSource(ds as DesignerLocalFileDataSource);
                        List.AddOrUpdateData(key, lfData);
                        break;

                    case DesignerDataSourceType.RemoteURL:
                        object ruData = await ExcuteRemoteURIDataSource(ds as DesignerRemoteURIDataSource);
                        List.AddOrUpdateData(key, ruData);
                        break;
                    case DesignerDataSourceType.StaticText:
                        object stData = ExcuteStaticTextDataSource(ds as DesignerStaticTextDataSource);
                        List.AddOrUpdateData(key, stData);
                        break;

                    default:
                        throw new Exception("未知的数据源执行方法");
                }
            }
            catch (Exception e)
            {
                LogManager.WriteLog("执行数据源:" + ds.Name + "时发生异常，详细信息：" + e.Message);
            }

        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetData(string key)
        {
            if (List.DataList.Keys.Contains(key))
            {
                return List.DataList[key];
            }
            else
                throw new NotImplementedException();
        }


        #region 执行方法
        /// <summary>
        /// 使用Indusfo.DAL 返回DataTable
        /// </summary>
        /// <param name="designerDataBaseDataSource"></param>
        /// <returns></returns>
        private static Task<DataTable> ExcuteDataBaseDataSource(DesignerDataBaseDataSource ds)
        {
            return Task<DataTable>.Run(() =>
            {
                using (SqlExcuter se = new SqlExcuter(DataBaseType.SqlServer, ds.ConnectionString))
                {
                    return se.ExecuteSelectSql(ds.SqlString);
                }
            });
           
        }
        /// <summary>
        /// 直接使用URI读取远程数据,返回二进制流数据
        /// </summary>
        /// <param name="designerRemoteURIDataSource"></param>
        /// <returns></returns>
        private static Task<object> ExcuteRemoteURIDataSource(DesignerRemoteURIDataSource designerRemoteURIDataSource)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 文件流读取文件，返回二进制流数据
        /// </summary>
        /// <param name="designerLocalFileDataSource"></param>
        /// <returns></returns>
        private static Task<object> ExcuteLocalFileDataSource(DesignerLocalFileDataSource designerLocalFileDataSource)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 执行静态文本数据源，返回字符串
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private static string ExcuteStaticTextDataSource(DesignerStaticTextDataSource ds)
        {
            return ds.Text;
        }

        #endregion

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
