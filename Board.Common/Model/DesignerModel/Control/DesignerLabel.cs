using Board.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Board.DesignerModel
{
    [Serializable]
    [XmlType("Label")]
    [XmlInclude(typeof(DesignerDataGridColumn))]
    [XmlInclude(typeof(DesignerClock))]
    [XmlInclude(typeof(DesignerDynamicLabel))]
    public class DesignerLabel:DesignerControl
    {
        public DesignerLabel() 
        {
            this.Type = DesignerElementType.Label;
            Text = "请输入文本";
            Font = new DesignerFont();
            this.Size = new DesignerSize(150, 50);
         
        }
        [Browsable(false)]
        [XmlIgnore]
        public string _text { get; set; }
        [Category("文本")]
        [DisplayName("文本内容")]
        [Description("设置文本的内容")]
        [XmlElement("Text")]
        public string Text 
        {
            get { return this._text; }
            set 
            {
                this._text = value;
                OnPropertyChanged("Text");
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public DesignerFont _font { get; set; }

        [Category("字体")]
        [DisplayName("字体")]
        [Description("设置文本的字体")]
        [XmlElement("Font")]
        public DesignerFont Font
        {
            get {
                return this._font; }
            set 
            {
                this._font = value;
                OnPropertyChanged("Font");
            }
        }



    }
    [Serializable]
    [XmlType("Clock")]
    public class DesignerClock : DesignerLabel
    {
         public DesignerClock() 
         {
             this.Type = DesignerElementType.Clock;
             this.Text = "YYYY-MM-DD hh-mm-ss";
         }
    }
    [Serializable]
    [XmlType("DynamicLabel")]
     public class DesignerDynamicLabel : DesignerLabel,IDynamicData
     {
        public DesignerDynamicLabel() 
        {
            this.Type = DesignerElementType.DynamicLabel;           
            this.BindName = "Value";
            this.FormatString = "[#Value]";
        }
         [Browsable(false)]
         [XmlIgnore]
         public string _dataSourceKey { get; set; }
        
         [Category("数据")]
         [DisplayName("数据源")]
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

         [Browsable(false)]
         [XmlIgnore]
         public string _bindName { get; set; }        
         [Category("数据")]
         [DisplayName("数据源绑定")]
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

         [Browsable(false)]
         [XmlIgnore]        
         public string _formatString { get; set; }         
         [Category("数据")]
         [DisplayName("格式化")]
         [XmlAttribute("FormatString")]
         public string FormatString 
         {
             get 
             { 
                 return this._formatString;
             }
             set
             {                                
                 this._formatString = value;
                 OnPropertyChanged("FormatString");
             }
         }
         [Browsable(false)]
         [XmlIgnore]
         public int _timeSpan { get; set; }
         [Category("数据")]
         [DisplayName("刷新时间")]
         [XmlAttribute("DataAccessTimeSpan")]
         public int DataAccessTimeSpan 
         {
             get { return this._timeSpan; }
             set 
             {
                 this._timeSpan = value;
                 OnPropertyChanged("DataAccessTimeSpan");
             }
         }
     }
}
