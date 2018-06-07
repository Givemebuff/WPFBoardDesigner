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
using System.Windows.Shapes;

namespace BoardDesigner.Windows
{
    /// <summary>
    /// NewBoardWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewBoardWindow : Window
    {

        public DesignerBoard Result;
        public string FileName; 
        public NewBoardWindow()
        {
            InitializeComponent();
            Result = new DesignerBoard();

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Result.Size.Width = Convert.ToDouble(WidthTB.Text);
            Result.Size.Height = Convert.ToDouble(HeightTB.Text);
            if (string.IsNullOrEmpty(FileNameTB.Text))
                FileName = "看板" + DateTime.Now.ToLongTimeString();
            else 
            {
                FileName = FileNameTB.Text;
            }
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
