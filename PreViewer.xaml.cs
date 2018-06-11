using Board.DesignerModel;
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
using System.Windows.Shapes;

namespace BoardDesigner
{
    /// <summary>
    /// PreViewer.xaml 的交互逻辑
    /// </summary>
    public partial class PreViewer : Window
    {
        public PreViewer()
        {
            InitializeComponent();
        }

        public PreViewer(DesignerBoard bd)
        {
            InitializeComponent();
            this.Width = bd.Size.Width;
            this.Height = bd.Size.Height;
            MainContentControl.Content = new Frame() { Content = new Board.Controls.SystemControl.BoardViewer(bd) };
        }

    }
}
