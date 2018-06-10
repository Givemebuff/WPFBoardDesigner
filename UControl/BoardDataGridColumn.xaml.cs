using Board.DesignerModel;
using Board.Interface;

using System.Windows.Controls;
using System.Windows.Data;

namespace BoardDesigner.UControl
{
    /// <summary>
    /// BoardDataGridColumn.xaml 的交互逻辑
    /// </summary>
    public partial class BoardDataGridColumn : UserControl,IDesigner
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
