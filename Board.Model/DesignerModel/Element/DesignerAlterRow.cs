using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Board.DesignerModel
{
    [Serializable]
    public class DesignerAlterRow: DesignerElement
    {
        [Browsable(false)]
        public Brush _alternatingRowBackground
        {
            get;
            set;
        }

        [Category("行相关")]
        [DisplayName("筛选行的背景色")]
        [Description("筛选行的背景色")]
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
        public int _alternationRowIndex
        {
            get;
            set;
        }

        [Category("行相关")]
        [DisplayName("筛选行条件")]
        [Description("筛选行条件")]
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
