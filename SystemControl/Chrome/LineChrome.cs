using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BoardDesigner.BaseChrome
{
   public class LineChrome:Control
    {
       static LineChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(LineChrome), new FrameworkPropertyMetadata(typeof(LineChrome)));
        }
    }
}
