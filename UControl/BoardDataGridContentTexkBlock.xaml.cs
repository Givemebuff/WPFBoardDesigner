using Board.DesignerModel;
using Board.Interface;
using System.Windows;
using System.Windows.Controls;

namespace BoardDesigner.UControl
{
    /// <summary>
    /// BoardDataGridContentTexkBlock.xaml 的交互逻辑
    /// </summary>
    public partial class BoardDataGridContentTexkBlock : UserControl,IDesigner
    {
        public DesignerTable DesignerItem { get; set; }
        public BoardDataGridContentTexkBlock()
        {
            InitializeComponent();
            DesignerItem = new DesignerTable()
            {
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            InitBinding();
        }

        public BoardDataGridContentTexkBlock(DesignerTable dt, string text)
        {
            InitializeComponent();
            DesignerItem = dt;
            uTextBlock.Text = text;
            InitBinding();
        }

        void InitBinding()
        {
            this.DataContext = DesignerItem;
        }
        public object GetDesignerItem()
        {
            return this.DesignerItem;
        }


    }
}
