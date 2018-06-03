using BoardDesigner.CustomPage;
using BoardDesigner.Model;
using BoardDesigner.Model.Data;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace BoardDesigner
{
    /// <summary>
    /// Test.xaml 的交互逻辑
    /// </summary>
    public partial class Test : Window
    {
     
        public Test()
        {
            InitializeComponent();
            
            xamPropertyGrid.SelectedObject = Img.DesignerItem;
            string path = System.IO.Directory.GetCurrentDirectory();
            Img.uImage.Source = new BitmapImage(new Uri(path+@"\BgImg.jpg", UriKind.Absolute));
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
           
        }
    }
}
