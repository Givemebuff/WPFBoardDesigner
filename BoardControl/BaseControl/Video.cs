using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BoardDesigner.BoardControl.BaseControl
{
    public class VideoCandy : ICandy
    {
        public ControlWrapper GetCandy()
        {
            ControlWrapper cw = new ControlWrapper();
            MediaElement MediaPlayer = new MediaElement();
            MediaPlayer.Stretch =  System.Windows.Media.Stretch.Fill;
            MediaPlayer.ScrubbingEnabled = false;
            MediaPlayer.Volume = 0.5;
            MediaPlayer.LoadedBehavior = MediaState.Manual;
            string path = System.IO.Directory.GetCurrentDirectory() + "\\VideoDemo.wmv";
            if (System.IO.File.Exists(path))
                MediaPlayer.Source = new Uri(path, UriKind.Absolute);
            //MediaPlayer.Play();
            cw.Protect(MediaPlayer);
            return cw;
        }
    }
}
