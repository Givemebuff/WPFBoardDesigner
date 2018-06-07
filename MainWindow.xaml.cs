using BoardDesigner.Base;
using BoardDesigner.CustomPage;
using BoardDesigner.Model;
using Infragistics.Controls.Editors;
using Infragistics.Windows.Ribbon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoardDesigner
{


    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {


        public DesignerCanvas CurrentDesignerCanvas
        {
            get { return (DesignerCanvas)GetValue(CurrentDesignerCanvasProperty); }
            set { SetValue(CurrentDesignerCanvasProperty, value); }
        }

        public static readonly DependencyProperty CurrentDesignerCanvasProperty =
          DependencyProperty.Register("CurrentDesignerCanvas", typeof(DesignerCanvas),
          typeof(MainWindow),
          new PropertyMetadata(null, new PropertyChangedCallback(CurrentDesignerCanvasPropertyChanged)));

        private static void CurrentDesignerCanvasPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow mw = d as MainWindow;
            if (mw.CurrentDesignerCanvas != null)
            {
                Binding bd = new Binding("SelectItem") { Source = mw.CurrentDesignerCanvas };
                mw.xamPropertyGrid.SetBinding(XamPropertyGrid.SelectedObjectProperty, bd);
            }
        }



        public MainWindow()
        {
            InitializeComponent();
            this.MainPage.Content = new Frame() { Content = new MainPage() };
            helpPage.Content = new Frame() { Content = new HelpPage() };

        }





        private void DocumentContentHost_ActiveDocumentChanged(object sender, RoutedPropertyChangedEventArgs<Infragistics.Windows.DockManager.ContentPane> e)
        {
            if (e.NewValue == null)
                return;
            //NewValue时ig的ContentPane
            //下一层ContentControl
            if (e.NewValue.Content == null)
                return;
            //下一层Frame
            Frame frame = (e.NewValue.Content as ContentControl).Content as Frame;
            if (frame == null)
                return;
            //下一层 DesignerPage HelperPage等
            if (frame.Content is DesignerPage)
            {
                DesignerPage page = frame.Content as DesignerPage;
                if (page == null)
                    return;
                //下一层
                CurrentDesignerCanvas = page.MainPanel;
            }
            else
            {
                CurrentDesignerCanvas = null;
            }
        }

        //预览
        private void ViewButtonTool_Click(object sender, RoutedEventArgs e)
        {
            DesignerBoard board = CurrentDesignerCanvas.Warp();
            BoardViewer bv = new BoardViewer(board);
            bv.Show();
        }

        private void SettingImageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ImageXamComboEditor_Loaded(object sender, RoutedEventArgs e)
        {
            //加载图片资源
            List<RImage> imgs = new List<RImage>();
            //资源文件中图片
            //TODO
            //文件夹下图片
            string curDirPath = Directory.GetCurrentDirectory() + @"\Images";
            //若不存在文件夹则先创建
            if (!Directory.Exists(curDirPath))
            {
                Directory.CreateDirectory(curDirPath);
            }
            DirectoryInfo di = new DirectoryInfo(curDirPath);
            FileInfo[] fis = di.GetFiles();
            foreach (FileInfo fi in fis)
            {
                string ext = fi.Extension.ToLower();
                if (ext.IndexOf("jpg") == -1 && ext.IndexOf("png") == -1 && ext.IndexOf("gif") == -1 && ext.IndexOf("bmp") == -1)
                    continue;
                else
                {
                    RImage image = new RImage()
                    {
                        ImageName = fi.Name,
                        Path = fi.FullName,
                        ImageUri = new Uri(fi.FullName, UriKind.Absolute),
                        Source = new BitmapImage(new Uri(fi.FullName, UriKind.Absolute))//FileSize = String.Format("{0:F}", ((double)(fi.Length / 1024)).ToString()),
                    };
                    image.FileSize = (double)fi.Length < 1024 ?
                        (fi.Length.ToString() + "B") :
                        (
                        ((double)fi.Length / 1024) < 1024 ? String.Format("{0:F}", (((double)fi.Length / 1024)).ToString() + "KB") :
                    String.Format("{0:F}", (((double)fi.Length / 1024 / 1024)).ToString() + "MB"));
                    imgs.Add(image);
                }
            }
            (sender as XamComboEditor).ItemsSource = imgs;

        }



    }
}
