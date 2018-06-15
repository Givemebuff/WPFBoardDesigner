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
    [XmlType("AlterRow")]
    public class DesignerAlterRow: DesignerElement
    {
        [Browsable(false)]
        [XmlIgnore]
        public Brush _alternatingRowBackground
        {
            get;
            set;
        }

        [Category("行相关")]
        [DisplayName("筛选行的背景色")]
        [Description("筛选行的背景色")]
         [XmlIgnore]
        public Brush AlternatingRowBackground
        {
            get { return this._alternatingRowBackground; }
            set
            {
                this._alternatingRowBackground = value;
                OnPropertyChanged("AlternatingRowBackground");
            }
        }
         [Browsable(false)]
        [XmlAttribute("AlternatingRowBackground")]
        public string XmlAlternatingRowBackground
        {
            get
            {
                if (this._alternatingRowBackground == null)
                    return null;
                return this._alternatingRowBackground.ToString();
            }
            set
            {
                this.AlternatingRowBackground = Board.XmlConverter.ColorConverter.XmlToBrush(value);
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public int _alternationRowIndex
        {
            get;
            set;
        }

        [Category("行相关")]
        [DisplayName("筛选行条件")]
        [Description("筛选行条件")]
        [XmlAttribute("AlternationRowIndex")]
        public int AlternationRowIndex
        {
            get { return this._alternationRowIndex; }
            set
            {                
                this._alternationRowIndex = value;
                OnPropertyChanged("AlternationRowIndex");
            }
        }
    }
}
