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
    /// BoardImage.xaml 的交互逻辑
    /// </summary>
    public partial class BoardImage : UserControl, IDesigner
    {
        public DesignerImage DesignerItem { get; set; }
        public BoardImage()
        {
            InitializeComponent();
            DesignerItem = new DesignerImage();
            DesignerItem.Size.Width = 200;
            DesignerItem.Size.Height = 200;
            InitBinding();
          
        }
        public BoardImage(DesignerImage di)
        {
            InitializeComponent();
            DesignerItem = di;
            InitBinding();          
        }

        public object GetDesignerItem()
        {
            return this.DesignerItem;
        }


        public void InitBinding()
        {
            this.DataContext = DesignerItem;           
        }
    }
}
