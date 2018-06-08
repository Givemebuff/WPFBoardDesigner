using BoardDesigner.Base;
using BoardDesigner.CustomPage;
using BoardDesigner.Model;
using BoardDesigner.Resource;
using BoardDesigner.Windows;
using Infragistics.Controls.Editors;
using Infragistics.Windows.DockManager;
using Infragistics.Windows.Ribbon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        #region 当前关注的设计页面
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

        #region 图片资源

        public ObservableCollection<RImage> ImageResourceCollection
        {
            get { return (ObservableCollection<RImage>)GetValue(ImageResourceCollectionProperty); }
            set { SetValue(ImageResourceCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageResourceCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageResourceCollectionProperty =
            DependencyProperty.Register("ImageResourceCollection", typeof(ObservableCollection<RImage>), typeof(MainWindow), new PropertyMetadata( new ObservableCollection<RImage>(ImageResourcesManeager.GetImageResources())));

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
            ToolGroupPane.IsPinned = true;
            propertiesDockPane.IsPinned = true;
        }

        //预览
        private void ViewButtonTool_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentDesignerPage == null)
                return;  
            DesignerBoard board = CurrentDesignerPage.MainPanel.Warp();
            BoardViewer bv = new BoardViewer(board);
            bv.Show();
        }

        private void SettingImageButton_Click(object sender, RoutedEventArgs e)
        {
            //打开文件对话框，选择图片，复制到程序图片文件夹
            string imgFolderPath = Directory.GetCurrentDirectory()+@"\Images\";
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.InitialDirectory = imgFolderPath;
            ofd.DefaultExt = ".*";
            ofd.Filter = "图片文件|*.bmp;*.jpg;*.png;*.gif";
         
            if (ofd.ShowDialog() == true)
            {
                
                string sourceImagePath = ofd.FileName;
                RImage newImage = new RImage(new FileInfo(sourceImagePath));
                string fileName = ofd.SafeFileName;
                string newImagePath = imgFolderPath + fileName;
                int i = 1;
                while (System.IO.File.Exists(newImagePath))//已经存在相同名字
                {
                    string[] ss = fileName.Split('.');
                    newImage.ImageName = ss[0] + "(" + i + ")." + ss[1];
                    newImagePath = imgFolderPath + newImage.ImageName;
                    newImage.Path = newImagePath;
                   i++;
                }                
               
                System.IO.File.Copy(sourceImagePath, newImagePath, true);
                ImageResourceCollection.Add(newImage);
            }

        }

        private void ImageXamComboEditor_Loaded(object sender, RoutedEventArgs e)
        {           
            (sender as XamComboEditor).SetBinding(XamComboEditor.ItemsSourceProperty,new Binding("ImageResourceCollection") { Source=this});
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

        private void ImageXamComboEditor_SelectionChanged(object sender, Infragistics.Controls.Editors.SelectionChangedEventArgs e)
        {

        }
    }
}
