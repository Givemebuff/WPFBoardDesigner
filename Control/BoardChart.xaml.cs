using BoardDesigner.Interface;
using BoardDesigner.Model;
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

namespace BoardDesigner.UControl
{
    /// <summary>
    /// BoardChart.xaml 的交互逻辑
    /// </summary>
    public partial class BoardChart : UserControl, IDesigner
    {
        public DesignerChart DesignerItem { get; set; }

        public object GetDesignerItem()
        {
            return this.DesignerItem;
        }

        public BoardChart()
        {
            InitializeComponent();
            DesignerItem = new DesignerChart();
            InitBinding();

        }

        public BoardChart(DesignerChart dc)
        {
            InitializeComponent();
            DesignerItem = dc;
            InitBinding();
        }
        public void InitBinding()
        {
            this.DataContext = DesignerItem;
            Binding canvasLeft = new Binding("Position.Location.X") { Source = DataContext };
            this.SetBinding(Canvas.LeftProperty, canvasLeft);
            Binding canvasTop = new Binding("Position.Location.Y") { Source = DataContext };
            this.SetBinding(Canvas.TopProperty, canvasTop);
        }

        }
}
