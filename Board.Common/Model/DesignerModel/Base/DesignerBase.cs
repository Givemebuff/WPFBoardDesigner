using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Board.DesignerModel
{
    public enum DesignerElementType
    {
        Element = 1,
        Back = 2,
        Control = 3,
        Container = 4,
        Property = 6
    }
    [Serializable]

    public class PropertyChangeBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        
    }

    public class DesignerObject : PropertyChangeBase
    {
        [XmlIgnore]
        [Browsable(false)]
        public string _name
        {
            get;
            set;
        }

        [Category("基础")]
        [DisplayName("名称")]
        [Description("元素的名字")]
        [XmlAttribute("Name")]

        public string Name
        {
            get { return this._name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        [ReadOnly(true)]
        [Category("基础")]
        [DisplayName("元素种类")]
        [Description("设计元素的种类")]
        [XmlAttribute("DesignerType")]
        public DesignerElementType Type
        {
            get;
            set;
        }
    }

    public class DesignerProperty : DesignerObject
    {
        public DesignerProperty()
        {
            this.Type = DesignerElementType.Property;
        }
    }

    [Serializable]
    public class DesignerElement : DesignerObject
    {

        public DesignerElement()
        {
            this.Type = DesignerElementType.Element;
            New();
        }
        [ReadOnly(true)]
        [XmlAttribute("GUID")]
        public Guid ID { get; set; }
        public void New()
        {
            this.ID = Guid.NewGuid();
            System.Type tp = this.GetType();
            Name = tp.Name + "_" + this.ID.ToString();
        }
    }

    [Serializable]

    public class DesignerVisualElement : DesignerElement
    {
        public DesignerVisualElement()
        {
            Size = new DesignerSize(100, 50);
            Visibility = Visibility.Visible;
            Opacity = 1;
            Position = new DesignerPosition();
        }

        #region 定位

        [Browsable(false)]
        [XmlIgnore]
        public DesignerPosition _position
        {
            get;
            set;
        }

        [Category("视图")]
        [DisplayName("定位")]
        [Description("定位属性")]
        [XmlElement("Position")]
        public DesignerPosition Position
        {
            get { return this._position; }
            set
            {
                this._position = value;
                OnPropertyChanged("Position");
            }
        }
        #endregion

        #region 尺寸

        [XmlIgnore]
        [Browsable(false)]
        public DesignerSize _size
        {
            get;
            set;
        }

        [Category("视图")]
        [DisplayName("尺寸")]
        [Description("宽度、长度")]
        [XmlElement("Size")]
        public DesignerSize Size
        {
            get { return this._size; }
            set
            {
                this._size = value;
                OnPropertyChanged("Size");
            }
        }

        #endregion

        #region 可见性


        [XmlIgnore]
        [Browsable(false)]
        public Visibility _visibility
        {
            get;
            set;
        }

        [Category("基础")]
        [DisplayName("可见性")]
        [Description("可见、占位隐藏、完全隐藏")]
        [XmlAttribute("Visibility")]
        public Visibility Visibility
        {
            get { return this._visibility; }
            set
            {
                this._visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        #endregion

        #region 透明度

        [XmlIgnore]
        [Browsable(false)]
        public double _opacity
        {
            get;
            set;
        }

        [Category("视图")]
        [DisplayName("透明度")]
        [Description("0-1")]
        [XmlAttribute("Opacity")]
        public double Opacity
        {
            get
            {
                return this._opacity;

            }
            set
            {
                this._opacity = value;
                OnPropertyChanged("Opacity");
            }
        }

        #endregion

    }

    [Serializable]

    public class DesignerBackElement : DesignerElement
    {
        public DesignerBackElement()
        {
            this.Type = DesignerElementType.Back;
        }
    }

}
