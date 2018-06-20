using Board.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Board.DesignerModel
{
    [Serializable]
    [XmlType("Processbar")]
    public class DesignerProcessbar : DesignerControl,IDynamicData
    {
        public DesignerProcessbar()
        {
            this.ControlType = DesignerControlType.Processbar;
        }

        private DesignerBrush _color { get; set; }
        [Category("进度条")]
        [DisplayName("进度条颜色")]
        [XmlElement("Color")]
        public DesignerBrush Color 
        {
            get { return this._color; }
            set 
            {
                this._color = value;
                OnPropertyChanged("Color");
            }
        }         

        private double _value { get; set; }
        [Category("进度条")]
        [DisplayName("进度条值")]
        [XmlElement("Value")]
        public double Value
        {
            get { return this._value; }
            set
            {
                this._value = value;
                OnPropertyChanged("Value");
            }
        }
        private string _bindName { get; set; }
        [Category("进度条")]
        [DisplayName("数据绑定名")]
        [XmlAttribute("BindName")]
        public string BindName
        {
            get { return this._bindName; }
            set
            {
                this._bindName = value;
                OnPropertyChanged("BindName");
            }
        }

        private string _dataSourceKey { get; set; }
        [Category("进度条")]
        [DisplayName("数据源名")]
        [XmlAttribute("DataSourceKey")]
        public string DataSourceKey
        {
            get
            {
                return this._dataSourceKey;
            }
            set
            {
                this._dataSourceKey = value;
                OnPropertyChanged("DataSourceKey");
            }
        }

        private int _dataAccessTimeSpan { get; set; }
        [Category("进度条")]
        [DisplayName("数据刷新时间")]
        [XmlAttribute("DataAccessTimeSpan")]
        public int DataAccessTimeSpan
        {
            get
            {
                return this._dataAccessTimeSpan;
            }
            set
            {
                this._dataAccessTimeSpan = value;
                OnPropertyChanged("DataAccessTimeSpan");
            }
        }
    }
}
