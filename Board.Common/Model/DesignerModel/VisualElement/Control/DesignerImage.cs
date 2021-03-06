﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Board.DesignerModel
{
    [Serializable]
    [XmlType("Image")]
    public class DesignerImage : DesignerControl
    {
        public DesignerImage() 
        {
            this.ControlType = DesignerControlType.Image;            
        }      

        [Browsable(false)]
        [XmlIgnore]
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
    [Serializable]
    [XmlType("GIF")]
    public class DesignerGif :DesignerControl
    {
        public DesignerGif() 
        {
            this.ControlType = DesignerControlType.GIF;            
        }

        [Browsable(false)]
        [XmlIgnore]
        public bool _autoPlay
        {
            get;
            set;
        }

        [Category("GIF")]
        [DisplayName("自动播放")]
        [Description("加载完成后是否自动播放")]
        [XmlAttribute("AutoPlay")]
        public bool AutoPlay
        {
            get { return this._autoPlay; }
            set
            {
                this._autoPlay = value;
                OnPropertyChanged("AutoPlay");
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public string _gifImageSource
        {
            get;
            set;
        }

        [Category("GIF")]
        [DisplayName("图片名")]
        [Description("相对路径下文件名")]
        [XmlAttribute("GIFImageSource")]
        public string GIFImageSource
        {
            get { return this._gifImageSource; }
            set
            {
                this._gifImageSource = value;
                OnPropertyChanged("GIFImageSource");
            }
        }
    }
}
