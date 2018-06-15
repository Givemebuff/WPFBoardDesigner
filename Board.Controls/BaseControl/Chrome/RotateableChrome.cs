using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Board.BaseControl
{
    public class RotateableChrome:Control
    {
        static RotateableChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(RotateableChrome), new FrameworkPropertyMetadata(typeof(RotateableChrome)));
        }
    }
}
