using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BoardDesigner.Model
{
    [Serializable]
    public class DesignerImage : DesignerControl
    {
        public DesignerImage() 
        {
            this.Type = DesignerElementType.Image;            
        }
        public DesignerImage(string path)
        {
            this.Type = DesignerElementType.Image;
            ImageSource = path;
        }

        [Browsable(false)]
        public string _imageSource
        {
            get;
            set;
        }

        [Category("图片")]
        [DisplayName("图片名")]
        [Description("相对路径下文件名")]
        [XmlAttribute("ImageSource")]
        public string ImageSource 
        {
            get { return this._imageSource; }
            set 
            {
                this._imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }
    }
}
