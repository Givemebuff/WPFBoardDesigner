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
        //全局唯一实例
        private static DataSourceManager uniqueInstance;
        //锁
        private static readonly object locker = new object();
        //私有构造
        public DataSourceManager()
        {


        }
        //实例接口
        //public static DataSourceManager GetInstance()
        //{
        //    if (uniqueInstance == null)
        //    {
        //        lock (locker)
        //        {
        //            // 如果类的实例不存在则创建，否则直接返回
        //            if (uniqueInstance == null)
        //            {
        //                uniqueInstance = new DataSourceManager();
        //            }
        //        }
        //    }
        //    return uniqueInstance;
        //}

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


        public static object GetDataAsync(string key) 
        {
            Func<string, object> action = GetData;//声明一个委托
            IAsyncResult ret = action.BeginInvoke(key, null, null);
            return action.EndInvoke(ret);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetData(string key)
        {
            if (DatasList.Keys.Contains(key))
            {
                Thread.Sleep(1000);
                ExcuteDataSource(key);               
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
        private static void UpdateDataSource(string key,DesignerDataSource ds)
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

    public class DataBaseDataSourceManager
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
