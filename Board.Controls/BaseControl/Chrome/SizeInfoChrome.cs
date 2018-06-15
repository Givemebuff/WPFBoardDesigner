using System.Windows;
using System.Windows.Controls;

namespace Board.BaseControl
{
    public class SizeInfoChrome : System.Windows.Controls.Control
    {
        static SizeInfoChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SizeInfoChrome), new FrameworkPropertyMetadata(typeof(SizeInfoChrome)));
        }
    }
}
