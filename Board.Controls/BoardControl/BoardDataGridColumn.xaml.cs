using Board.DesignerModel;
using Board.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Board.Controls.BoardControl
{
    /// <summary>
    /// BoardDataGridColumn.xaml 的交互逻辑
    /// </summary>
    public partial class BoardDataGridColumn : UserControl, IDesigner
    {
        public DesignerDataGridColumn DesignerItem
        {
            get;
            set;
        }
        public BoardDataGridColumn()
        {
            InitializeComponent();
            DesignerItem = new DesignerDataGridColumn();
            InitBinding();
        }
        public BoardDataGridColumn(DesignerDataGridColumn column)
        {
            InitializeComponent();
            DesignerItem = column;
            InitBinding();
        }
        void InitBinding()
        {
            this.DataContext = DesignerItem;

            this.SetBinding(Grid.ColumnProperty, new Binding("Position.ColumnIndex") { Source = DataContext });

        }
        public object GetDesignerItem()
        {
            return this.DesignerItem;
        }
    }
}
