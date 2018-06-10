using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.DesignerModel
{
    public class DesignerMedia:DesignerControl
    {
        public DesignerMedia()
        {
            this.Type = DesignerElementType.Media;
            MediaSource = "MediaDemo.wmv";
            SpeedRatio = 1;
            Volume = 0.5;
        }

        [Browsable(false)]
        public string _mediaSource { get; set; }

        [Category("多媒体")]
        [DisplayName("多媒体文件源")]
        [Description("多媒体文件源")]

        public string MediaSource
        {
            get { return this._mediaSource; }
            set
            {
                this._mediaSource = value;
                OnPropertyChanged("MediaSource");
            }
        }

        [Browsable(false)]
        public double _speedRatio { get; set; }

        [Category("多媒体")]
        [DisplayName("播放速率")]
        [Description("播放速率")]

        public double SpeedRatio 
        {
            get { return this._speedRatio; }
            set
            {
                this._speedRatio = value;
                OnPropertyChanged("SpeedRatio");
            }
        }

        [Browsable(false)]
        public double _volume { get; set; }

        [Category("多媒体")]
        [DisplayName("音量")]
        [Description("音量")]

        public double Volume 
        {
            get { return this._volume; }
            set
            {
                this._volume = value;
                OnPropertyChanged("Volume");
            }
        }

    }
}
