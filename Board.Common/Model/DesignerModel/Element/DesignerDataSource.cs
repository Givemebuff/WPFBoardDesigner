using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Board.DesignerModel
{
    [Serializable]
    public enum DesignerDataSourceType
    {
        Default = 0,
        DataBase = 1,
        LocalFile = 2,
        RemoteURL = 3,
        StaticText = 4
    }
    [XmlType("DataSource")]
    [XmlInclude(typeof(DesignerDataBaseDataSource))]
    [XmlInclude(typeof(DesignerLocalFileDataSource))]
    [XmlInclude(typeof(DesignerRemoteURIDataSource))]
    [XmlInclude(typeof(DesignerStaticTextDataSource))]
    public class DesignerDataSource : DesignerElement
    {
        [Browsable(false)]
        [XmlIgnore]
        public DesignerDataSourceType _dataSourceType { get; set; }
        [ReadOnly(true)]
        [Category("类别")]
        [DisplayName("数据源类别")]
        [XmlAttribute("DataSourceType")]
        public DesignerDataSourceType DataSourceType
        {
            get { return this._dataSourceType; }
        }
        [Browsable(false)]
        [XmlIgnore]
        public int _timeSpan { get; set; }
        [ReadOnly(true)]
        [Category("时间")]
        [DisplayName("访问时间间隔")]
        [Description("0表示访问一次。单位毫秒")]
        [XmlAttribute("TimeSpan")]
        public int TimeSpan 
        {
            get { return this._timeSpan; }
            set
            {
                if (value < 0)
                    value = 0;
                this._timeSpan = value;
                OnPropertyChanged("TimeSpan");
            }
        }

    }
    [XmlType("DBDataSource")]
    public class DesignerDataBaseDataSource : DesignerDataSource
    {
        public DesignerDataBaseDataSource()
        {
            this._dataSourceType = DesignerDataSourceType.DataBase;
        }
        [Browsable(false)]
        [XmlIgnore]
        public string _address
        {
            get;
            set;
        }
        [Category("数据源")]
        [DisplayName("IPV4地址")]
        [Description("IP地址")]
        [XmlAttribute("Address")]
        public string Address
        {
            get { return this._address; }
            set
            {
                this._address = value;
                OnPropertyChanged("Address");
                OnPropertyChanged("ConnectionString");
            }
        }
        [Browsable(false)]
        [XmlIgnore]
        public string _dataBaseName { get; set; }
        [Category("数据源")]
        [DisplayName("数据库实例名")]
        [Description("实例名")]
        [XmlAttribute("DataBaseName")]
        public string DataBaseName
        {
            get { return this._dataBaseName; }
            set
            {
                this._dataBaseName = value;
                OnPropertyChanged("DataBaseName");
                OnPropertyChanged("ConnectionString");

            }
        }
        [Browsable(false)]
        [XmlIgnore]
        public string _userName { get; set; }

        [Category("数据源")]
        [DisplayName("用户名")]
        [Description("用户名UID")]
        [XmlAttribute("UID")]
        public string UserName
        {
            get { return this._userName; }
            set
            {
                this._userName = value;
                OnPropertyChanged("UserName");
                OnPropertyChanged("ConnectionString");

            }
        }
        [Browsable(false)]
        [XmlIgnore]
        public string _passWord
        {
            get;
            set;
        }

        [Category("数据源")]
        [DisplayName("密码")]
        [Description("数据源密码")]
        [XmlAttribute("PassWord")]
        public string PassWord
        {
            get
            {
                return this._passWord;
            }
            set
            {
                this._passWord = value;
                OnPropertyChanged("PassWord");
                OnPropertyChanged("ConnectionString");

            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public string _sqlString
        {
            get;
            set;
        }

        [Category("数据源")]
        [DisplayName("SQL语句")]
        [Description("SQL语句")]
        [XmlElement("SqlString")]
        public string SqlString
        {
            get
            {
                return this._sqlString;
            }
            set
            {
                this._sqlString = value;
                OnPropertyChanged("SqlString");
            }
        }



        [ReadOnly(true)]
        [Category("数据源")]
        [DisplayName("连接字符串")]
        [Description("连接字符串")]
        [XmlElement("ConnectionString")]
        public string ConnectionString
        {
            get
            {
                return string.Format("Server={0};database={1};uid={2};pwd={3}", this.Address, this.DataBaseName, this.UserName, this.PassWord);
            }
        }
    }
    [XmlType("LFDataSource")]
    public class DesignerLocalFileDataSource : DesignerDataSource
    {
        public DesignerLocalFileDataSource()
        {
            this._dataSourceType = DesignerDataSourceType.LocalFile;
        }
    }
    [XmlType("RUDataSource")]
    public class DesignerRemoteURIDataSource : DesignerDataSource
    {
        public DesignerRemoteURIDataSource()
        {
            this._dataSourceType = DesignerDataSourceType.RemoteURL;
        }
    }
    [XmlType("STDataSource")]
    public class DesignerStaticTextDataSource : DesignerDataSource
    {
        public DesignerStaticTextDataSource()
        {
            this._dataSourceType = DesignerDataSourceType.StaticText;
        }
        [Browsable(false)]
        [XmlIgnore]
        public string _text
        {
            get;
            set;
        }
        [Category("数据源")]
        [DisplayName("静态文本内容")]
        [XmlAttribute("Text")]
        public string Text
        {
            get { return this._text; }
            set
            {
                this._text = value;
                OnPropertyChanged("Text");
            }
        }
    }
}
