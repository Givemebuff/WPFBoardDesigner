﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDesigner.Model
{
    public class DesignerDataSource:DesignerElement
    {
        [Browsable(false)]
        public string _address
        {
            get;set;
        }
        [Category("数据源")]
        [DisplayName("IPV4地址")]
        [Description("IP地址")]
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
        public string _dataBaseName { get; set; }
        [Category("数据源")]
        [DisplayName("数据库实例名")]
        [Description("实例名")]
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
        public string _userName { get; set; }
        [Category("数据源")]
        [DisplayName("用户名")]
        [Description("用户名UID")]
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
        public string _passWord
        {
            get;set;
        }

        [Category("数据源")]
        [DisplayName("密码")]
        [Description("数据源密码")]
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
        public string _sqlString
        {
            get; set;
        }

        [Category("数据源")]
        [DisplayName("SQL语句")]
        [Description("SQL语句")]
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
        [DisplayName("密码")]
        [Description("数据源密码")]
        public string ConnectionString
        {
            get
            {
                return string.Format("Server={0};database={1};uid={2};pwd={3}", this.Address, this.DataBaseName, this.UserName, this.PassWord);
            }
        }
    }
}
