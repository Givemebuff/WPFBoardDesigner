using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BoardDesigner.BaseChrome
{
    public class MoveableChrome :Control
    {
        static MoveableChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(MoveableChrome), new FrameworkPropertyMetadata(typeof(MoveableChrome)));
        }
    }
}
