using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BoardDesigner.BaseChrome
{
   public class TransformOriginPointChrome:Control
    {
       static TransformOriginPointChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(TransformOriginPointChrome), new FrameworkPropertyMetadata(typeof(TransformOriginPointChrome)));
        }
    }
}
