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
    /// BoardLabel.xaml 的交互逻辑
    /// </summary>
    public partial class BoardLabel : UserControl,IDesigner
    {
        public DesignerLabel DesignerModel
        {
            get;
            set;
        }

        public BoardLabel()
        {
            InitializeComponent();
            DesignerModel = new DesignerLabel();
            InitBinding();
        }

        public BoardLabel(DesignerLabel tb)
        {
            InitializeComponent();
            DesignerModel = tb;
            InitBinding();
        }

        public void InitBinding()
        {
            this.DataContext = DesignerModel;

        }

        public object GetDesignerModel()
        {
            return this.DesignerModel;
        }
    }
}
