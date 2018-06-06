using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BoardDesigner.Model
{
    public class RImage
    {
        public string ImageName { get; set; }

        public string Path { get; set; }
        public string FileSize { get; set; }
        public Uri ImageUri { get; set; }
        public ImageSource Source{get;set;}

        public override string ToString()
        {
            return Path;
        }
       
    }
}
