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
    [XmlType("Label")]
    [XmlInclude(typeof(DesignerDataGridColumn))]
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
}
