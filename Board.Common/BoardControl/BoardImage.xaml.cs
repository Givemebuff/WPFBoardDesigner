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
    /// BoardImage.xaml 的交互逻辑
    /// </summary>
    public partial class BoardImage : UserControl, IDesigner
    {
        public DesignerImage DesignerModel { get; set; }
        public BoardImage()
        {
            InitializeComponent();
            DesignerModel = new DesignerImage();
            DesignerModel.Size.Width = 200;
            DesignerModel.Size.Height = 200;
            InitBinding();

        }
        public BoardImage(DesignerImage di)
        {
            InitializeComponent();
            DesignerModel = di;
            InitBinding();
        }

        public object GetDesignerModel()
        {
            return this.DesignerModel;
        }


        public void InitBinding()
        {
            this.DataContext = DesignerModel;

        }
    }
}
