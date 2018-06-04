using BoardDesigner.Interface;
using BoardDesigner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoardDesigner.UControl
{
    /// <summary>
    /// BoardTable.xaml 的交互逻辑
    /// </summary>
    public partial class BoardTable : UserControl, IDesigner
    {

        public DesignerTable DesignerItem { get; set; }
        public BoardTable()
        {
            InitializeComponent();
            DesignerItem = new DesignerTable() { HeaderFont = new DesignerFont() { FontSize = 30 } };
         
            InitBinding();
        }
        public object GetDesignerItem()
        {
            return this.DesignerItem;
        }

        public void InitBinding()
        {
            this.DataContext = DesignerItem;
            Binding canvasLeft = new Binding("Position.Location.X") { Source = DataContext };
            this.SetBinding(Canvas.LeftProperty, canvasLeft);
            Binding canvasTop = new Binding("Position.Location.Y") { Source = DataContext };
            this.SetBinding(Canvas.TopProperty, canvasTop);
          
            //uGrid.ColumnHeaderStyle.Setters[0].
            Style headerStyle = new Style(typeof(DataGridColumnHeader));
            Setter bgSet = new Setter(DataGridColumnHeader.BackgroundProperty, DesignerItem.HeaderBackground);
            headerStyle.Setters.Add(bgSet);
            uGrid.ColumnHeaderStyle = headerStyle;
        }
    }
}
