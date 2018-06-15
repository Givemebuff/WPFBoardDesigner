using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Board.BaseControl
{
    public class ToolBox : ItemsControl
    {
        private Size defaultItemSize = new Size(24, 24);
        public Size DefaultItemSize
        {
            get { return this.defaultItemSize; }
            set { this.defaultItemSize = value; }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ToolBoxItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is ToolBoxItem);
        }
    }
}
