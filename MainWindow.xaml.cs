using BoardDesigner.Base;
using BoardDesigner.CustomPage;
using BoardDesigner.Model;
using BoardDesigner.Windows;
using Infragistics.Controls.Editors;
using Infragistics.Windows.DockManager;
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
    public partial class MainWindow : XamRibbonWindow
    {


        #region 当前关注的设计页面的设计容器
        public DesignerPage CurrentDesignerPage
        {
            get { return (DesignerPage)GetValue(CurrentDesignerPageProperty); }
            set { SetValue(CurrentDesignerPageProperty, value); }
        }

        public static readonly DependencyProperty CurrentDesignerPageProperty =
          DependencyProperty.Register("CurrentDesignerPage", typeof(DesignerPage),
          typeof(MainWindow),
          new PropertyMetadata(null, new PropertyChangedCallback(CurrentDesignerPagePropertyChanged)));

        private static void CurrentDesignerPagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow mw = d as MainWindow;
            if (mw.CurrentDesignerPage != null)
            {
                DesignerCanvas dc = mw.CurrentDesignerPage.MainPanel;
                mw.xamPropertyGrid.SetBinding(XamPropertyGrid.SelectedObjectProperty, new Binding("SelectItem") { Source = dc });
            }
        }
        #endregion


        public MainWindow()
        {
            InitializeComponent();
            InitPage();
            InitCommands();

        }



        private void InitPage()
        {

            this.MainPage.Content = new Frame() { Content = new MainPage() };
            this.helpPage.Content = new Frame() { Content = new HelpPage() };
        }

        void CreateNewBoardDesignerPage(DesignerBoard db, string fileName, string filePath)
        {
            //新建一个设计页
            ContentPane newDesignerContentPanel = new ContentPane();
            newDesignerContentPanel.Header = fileName;
            newDesignerContentPanel.CloseButtonVisibility = System.Windows.Visibility.Visible;
            DesignerPage newDesignerPage = new DesignerPage(db);
            newDesignerPage.FilePath = filePath;
            Frame newFrame = new Frame();
            newFrame.Content = newDesignerPage;
            newDesignerContentPanel.Content = newFrame;
            MainTabGroupPane.Items.Add(newDesignerContentPanel);
            MainTabGroupPane.SelectedItem = newDesignerContentPanel;
            this.CurrentDesignerPage = newDesignerPage;
        }

        //预览
        private void ViewButtonTool_Click(object sender, RoutedEventArgs e)
        {
            DesignerBoard board = CurrentDesignerPage.MainPanel.Warp();
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

        #region 命令

        private void InitCommands()
        {
            //新建工程命令
            this.NewProjectButton.CommandTarget = MainTabGroupPane;
            CommandBinding newProCmdBinding = new CommandBinding(ApplicationCommands.New);
            newProCmdBinding.Executed += new ExecutedRoutedEventHandler(NewProCommand_Executed);
            newProCmdBinding.CanExecute += NewProCommand_CanExecute;
            //打开工程命令
            this.OpenProjectButton.CommandTarget = this;
            CommandBinding openProCmdBinding = new CommandBinding(ApplicationCommands.Open);
            openProCmdBinding.Executed += new ExecutedRoutedEventHandler(OpenProCommand_Executed);
            openProCmdBinding.CanExecute += OpenProCommand_CanExecute;
        }

        #region 新建
        void NewProCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }
        private void NewProCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            //打开新建对话框
            NewBoardWindow nbw = new NewBoardWindow();
            if (nbw.ShowDialog() == true)
            {
                CreateNewBoardDesignerPage(nbw.Result, nbw.FileName, null);
            }
        }

        #endregion

        #region 打开工程

        void OpenProCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }
        private void OpenProCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null) //路径作为参数传入
            {
                //打开文件对话框
                Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                ofd.DefaultExt = ".*";
                ofd.Filter = "所有文件|*.*";
                if (ofd.ShowDialog() == true)
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    string exn = fi.Extension.ToLower();
                    //Board文件，Rdlx文件,jpg,png,gif,bmp
                    if (exn.IndexOf("board") != -1)
                    {
                        //TODO Board文件反序列化为DesignerBoard对象进行初始化
                        DesignerBoard db = new DesignerBoard();
                        CreateNewBoardDesignerPage(db, "Test", "C:\\Test.Board");
                    }
                    else if (exn.IndexOf("rdlx") != -1)
                    {
                        throw new Exception("敬请期待");
                    }
                    else if (exn.IndexOf("jpg") != -1 || exn.IndexOf("png") != -1 || exn.IndexOf("gif") != -1 || exn.IndexOf("bmp") != -1)
                    {

                    }
                    else
                    {
                        throw new Exception("不支持打开" + exn + "格式的文件");
                    }
                }
            }
            else 
            {
                //打开已经存在的文件
            }
            e.Handled = true;
        }

        #endregion

        #region 保存

        #endregion

        #region 退出



        #endregion







        #endregion

    }
}
