using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Board.BaseControl
{
    public class ResizeableChrome:Control
    {
        static ResizeableChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeableChrome), new FrameworkPropertyMetadata(typeof(ResizeableChrome)));
        }
    }
}
