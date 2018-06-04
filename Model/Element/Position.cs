using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace BoardDesigner.Model
{
    public class DesignerPosition : DesignerElement
    {
        public DesignerPosition()
        {
            this.Type = DesignerElementType.Position;
            Location = new Point(0, 0);
        }

        public DesignerPosition(double x, double y)
        {
            this.Type = DesignerElementType.Position;
            Location = new Point(x, y);
        }

        #region 绝对定位

        public void MoveOffset(double x, double y)
        {
            Point np = new Point(this.Location.X + x, this.Location.Y + y);
            this.Location = np;
        }

        public void MoveTo(double x, double y)
        {
            Point np = new Point(x, y);
            this.Location = np;
        }

        [Browsable(false)]

        public Point _location
        {
            get;
            set;
        }
        [Category("视图")]
        [DisplayName("绝对坐标")]
        [Description("相对于左上角(0,0)点的绝对坐标")]
        [XmlAttribute("Location")]
        public Point Location
        {
            get { return this._location; }
            set
            {
                this._location = value;
                OnPropertyChanged("Location");
            }
        }

        #endregion

        #region 边距

        [Browsable(false)]
        public Thickness _margin
        {
            get;
            set;
        }
        [Category("边距")]
        [DisplayName("外边距")]
        [Description("距离父容器的边距")]
        [XmlAttribute("Margin")]
        public Thickness Margin
        {
            get { return this._margin; }
            set
            {
                this._margin = value;
                OnPropertyChanged("Margin");
            }
        }
        [Browsable(false)]

        public Thickness _padding
        {
            get;
            set;
        }

        [Category("边距")]
        [DisplayName("内边距")]
        [Description("距离子元素的边距")]
        [XmlAttribute("Padding")]
        public Thickness Padding
        {
            get { return this._padding; }
            set
            {
                this._padding = value;
                OnPropertyChanged("Padding");
            }
        }

        #endregion

        #region 网格
        [Browsable(false)]
        public int _rowIndex
        {
            get;
            set;
        }

        [Category("网格")]
        [DisplayName("行索引")]
        [Description("网格的第几行")]
        [XmlAttribute("RowIndex")]
        public int RowIndex
        {
            get { return this._rowIndex; }
            set
            {
                this._rowIndex = value;
                OnPropertyChanged("RowIndex");
            }
        }
        [Browsable(false)]
        public int _columnIndex
        {
            get;
            set;
        }
        [Category("网格")]
        [DisplayName("列索引")]
        [Description("网格的第几列")]
        [XmlAttribute("Column")]
        public int ColumnIndex
        {
            get { return this._columnIndex; }
            set
            {
                this._columnIndex = value;
                OnPropertyChanged("ColumnIndex");
            }
        }



        #region 网格跨越
        [Browsable(false)]
        public int _rowSpan
        {
            get;
            set;
        }
        [Category("网格")]
        [DisplayName("行跨越")]
        [Description("跨越几行网格")]
        [XmlAttribute("RowSpan")]
        public int RowSpan
        {
            get { return this._rowSpan; }
            set
            {
                this._rowSpan = value;
                OnPropertyChanged("RowSpan");
            }
        }
        [Browsable(false)]
        public int _columnSpan
        {
            get;
            set;
        }
        [Category("网格")]
        [DisplayName("列跨越")]
        [Description("跨越几列网格")]
        [XmlAttribute("ColumnSpan")]
        public int ColumnSpan
        {
            get { return this._columnSpan; }
            set
            {
                this._columnSpan = value;
                OnPropertyChanged("ColumnSpan");
            }
        }

        #endregion

        #endregion
    }
}
