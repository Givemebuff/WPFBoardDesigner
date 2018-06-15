using System.Windows;
using System.Windows.Controls;

namespace Board.BaseControl{
    public class LocationInfoChrome : System.Windows.Controls.Control
    {
        static LocationInfoChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(LocationInfoChrome), new FrameworkPropertyMetadata(typeof(LocationInfoChrome)));
        }
    } 
}
