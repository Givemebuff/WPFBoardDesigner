using BoardDesigner.Base;
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
    /// BoardLabel.xaml 的交互逻辑
    /// </summary>
    public partial class BoardLabel : UserControl, IDesigner
    {
        public DesignerLabel DesignerItem
        {
            get;
            set;
        }     

        public BoardLabel()
        {
            InitializeComponent();
            DesignerItem = new DesignerLabel();
            InitBinding();
        }

        public BoardLabel(DesignerLabel tb) 
        {
            InitializeComponent();
            DesignerItem = tb;
            InitBinding();
        }

        public void InitBinding()
        {
            this.DataContext = DesignerItem;
            Binding canvasLeft = new Binding("Position.Location.X") { Source = DataContext};
            this.SetBinding(DesignerCanvas.LeftProperty, canvasLeft);
            Binding canvasTop = new Binding("Position.Location.Y") { Source = DataContext };
            this.SetBinding(DesignerCanvas.TopProperty, canvasTop);

        }

        public object GetDesignerItem()
        {
            return this.DesignerItem;
        }
    }
}
