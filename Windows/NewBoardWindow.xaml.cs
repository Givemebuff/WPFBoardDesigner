using Board.DesignerModel;
using System;
using System.Windows;
using System.Windows.Media;

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
            Result = new DesignerBoard() { Background = new DesignerBrush() { ColorBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255)) } };

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
