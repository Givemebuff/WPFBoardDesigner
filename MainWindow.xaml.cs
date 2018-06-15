using Board.BaseControl;
using Board.Controls.SystemControl;
using Board.DesignerModel;
using Board.Interface;
using Board.Resource;
using Board.SystemModel;
using BoardDesigner.CustomPage;
using BoardDesigner.Windows;
using Infragistics.Controls.Editors;
using Infragistics.Documents;
using Infragistics.Windows.DockManager;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

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


            mw.xamPropertyGrid.SetBinding(XamPropertyGrid.SelectedObjectProperty, new Binding("SelectItem") { Source = mw.CurrentDesignerPage });
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
            DependencyProperty.Register("ImageResourceCollection", typeof(ObservableCollection<RImage>), typeof(MainWindow), new PropertyMetadata(new ObservableCollection<RImage>(ImageResourcesManager.GetImageResources())));

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
            this.SetBinding(MainWindow.CurrentDesignerPageProperty, new Binding("Content.Content") { Source = MainContentHost.ActiveDocument });
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
            newDesignerPage.Board.Name = fileName;
            this.CurrentDesignerPage = newDesignerPage;
            ToolGroupPane.IsPinned = true;
            propertiesDockPane.IsPinned = true;
            this.Logout("Create Page:\nMode:Design for Board\nBoard :" + filePath + "," + fileName + "," + db.Size.Width + "," + db.Size.Height);
        }

        //预览
        private void ViewButtonTool_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentDesignerPage == null)
                return;
            this.Logout("Previewing...");
            DesignerBoard board = (CurrentDesignerPage.DesignerGrid.Children[0] as DesignerCanvas).Warp();
            PreViewer pv = new PreViewer(board);
            pv.ShowDialog();
        }

        private void SettingImageButton_Click(object sender, RoutedEventArgs e)
        {
            //打开文件对话框，选择图片，复制到程序图片文件夹
            string imgFolderPath = Directory.GetCurrentDirectory() + @"\Images\";
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
                this.Logout("Image FullPath:" + newImagePath);
            }

        }

        private void ImageXamComboEditor_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as XamComboEditor).SetBinding(XamComboEditor.ItemsSourceProperty, new Binding("ImageResourceCollection") { Source = this });
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
            //打开数据库数据源窗口
            InitCustomCommand();
        }



        #region 系统命令

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
            this.Logout("Opened " + nbw.Title);
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

                        try
                        {
                            string xml = File.ReadAllText(fi.FullName);
                            DesignerBoard db = Board.DataHelper.XmlHelper.XmlDerialize<DesignerBoard>(xml);
                            CreateNewBoardDesignerPage(db, fi.Name, fi.FullName);
                            this.Logout("Loaded Board " + db.Name);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("打开看板文件出错：" + ex.Message);
                        }


                    }
                    else if (exn.IndexOf("rdlx") != -1)
                    {
                        throw new Exception("敬请期待");
                    }
                    else if (exn.IndexOf("jpg") != -1 || exn.IndexOf("png") != -1 || exn.IndexOf("gif") != -1 || exn.IndexOf("bmp") != -1)
                    {
                        throw new Exception("敬请期待");
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

        void SaveProCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (CurrentDesignerPage != null)
            {
                e.CanExecute = true;
            }

            e.Handled = true;
        }
        private void SaveProCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.DefaultExt = ".Board";
            sfd.Filter = "看板(*.Board)|*.Board|All Files|*.*";
            sfd.FileName =  CurrentDesignerPage.Board.Name+".Board";
            if (sfd.ShowDialog() == true) 
            {
                CurrentDesignerPage.Board.Name = sfd.SafeFileName;
                (MainTabGroupPane.SelectedItem as ContentPane).Header = sfd.SafeFileName;
                string path = sfd.FileName;
                DesignerBoard bd =( CurrentDesignerPage.DesignerGrid.Children[0] as DesignerCanvas).Warp();
                BoardManager.SaveBoard(bd, path);
                this.Logout("Saved Board " + CurrentDesignerPage.Board.Name);
            }
            
            e.Handled = true;
        }

        #endregion

        #region 关闭


        void CloseProCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.CurrentDesignerPage != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
            e.Handled = true;
        }
        private void CloseProCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        #endregion

        #region 复制

        void CopyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.CurrentDesignerPage == null)
                e.CanExecute = false;
            else
            {
                if (this.CurrentDesignerPage.SelectItem != null && !(this.CurrentDesignerPage.SelectItem is DesignerBoard))
                    e.CanExecute = true;
                else
                    e.CanExecute = false;
            }
            e.Handled = true;
        }
        private void CopyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        #endregion

        #region 粘贴
        void PasteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.CurrentDesignerPage != null)//粘贴板不为空TODO
                e.CanExecute = true;
            else
                e.CanExecute = false;
            e.Handled = true;
        }
        private void PasteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        #endregion

        #region 剪切

        void CutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.CurrentDesignerPage == null)
                e.CanExecute = false;
            else
            {
                if (this.CurrentDesignerPage.SelectItem != null && !(this.CurrentDesignerPage.SelectItem is DesignerBoard))
                    e.CanExecute = true;
                else
                    e.CanExecute = false;
            }
            e.Handled = true;
        }
        private void CutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        #endregion

        #region 删除

        void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.CurrentDesignerPage == null)
                e.CanExecute = false;
            else
            {
                if (this.CurrentDesignerPage.SelectItem != null && !(this.CurrentDesignerPage.SelectItem is DesignerBoard))
                    e.CanExecute = true;
                else
                    e.CanExecute = false;
            }
            e.Handled = true;
        }
        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.CurrentDesignerPage.DesignerGrid.Children[0] != null)
            {
                DesignerCanvas dc = this.CurrentDesignerPage.DesignerGrid.Children[0] as DesignerCanvas;
                for (int i = dc.Children.Count - 1; i >= 0; i--)
                {
                    if ((dc.Children[i] as DesignerItem).IsSelected)
                    {
                        this.Logout("Deleted DesignerItem");
                        dc.Children.Remove(dc.Children[i]);

                    }
                }
                this.CurrentDesignerPage.SelectItem = CurrentDesignerPage.Board;
            }

            e.Handled = true;
        }

        #endregion

        #endregion

        #region 自定义命令

        private void InitCustomCommand()
        {

            this.SelectAllButton.Command = SelectAllCommand;
            this.SelectAllCommand.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
            this.SelectAllButton.CommandTarget = CurrentDesignerPage;
            CommandBinding selectAllCommandBinding = new CommandBinding();
            selectAllCommandBinding.Command = SelectAllCommand;
            selectAllCommandBinding.CanExecute += new CanExecuteRoutedEventHandler(SelectAll_CanExecute);
            selectAllCommandBinding.Executed += new ExecutedRoutedEventHandler(SelectAll_Execute);
            this.CommandBindings.Add(selectAllCommandBinding);
        }

        private RoutedCommand SelectAllCommand = new RoutedCommand("SelectAll", typeof(MainWindow));
        void SelectAll_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (CurrentDesignerPage == null)
                e.CanExecute = false;
            else
            {
                if (CurrentDesignerPage.Content == null)
                    e.CanExecute = false;
                else
                {
                    if ((this.CurrentDesignerPage.DesignerGrid.Children[0] as DesignerCanvas).Children.Count > 0)
                        e.CanExecute = true;
                    else
                        e.CanExecute = false;
                }
            }
            e.Handled = true;
        }
        private void SelectAll_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (DesignerItem di in (this.CurrentDesignerPage.DesignerGrid.Children[0] as DesignerCanvas).Children)
            {
                di.IsSelected = true;
                this.CurrentDesignerPage.SelectItem = (di as IDesigner).GetDesignerModel();
            }
            e.Handled = true;
        }

        #endregion

        #endregion

        private void ImageXamComboEditor_SelectionChanged(object sender, Infragistics.Controls.Editors.SelectionChangedEventArgs e)
        {

        }

        private void MainContentHost_ActiveDocumentChanged(object sender, RoutedPropertyChangedEventArgs<ContentPane> e)
        {
            if (e.NewValue == null)
            {
                CurrentDesignerPage = null;
                return;
            }

            Frame f = e.NewValue.Content as Frame;
            if (f.Content == null)
                return;
            else
            {
                Page p = f.Content as Page;
                if (p is DesignerPage)
                {
                    CurrentDesignerPage = p as DesignerPage;
                }
                else
                {
                    CurrentDesignerPage = null;
                }
                this.Logout("Changed Active DesignerPage");
            }
        }
        public void Logout(string str)
        {
            DebugTextBox.AppendText("\n" + "[" + DateTime.Now.ToLongTimeString() + "]" + str);
            DebugTextBox.ScrollToEnd();
        }

        private void DataSourceSettingButton_Click(object sender, RoutedEventArgs e)
        {
            string key = (((sender as Button).Parent as StackPanel).FindName("DataSourceTB") as TextBox).Text;
            DataSourceSettingWindow win = new DataSourceSettingWindow(key);
            if (win.ShowDialog() == true)
            {
                //更改Key，并通过管理器获取对象添加至数据源集合
                key = win.SelectedItem.Name;

                ((((sender as Button).Parent as StackPanel).FindName("DataSourceTB") as TextBox).DataContext as PropertyGridPropertyItem).Value = key;
                this.CurrentDesignerPage.Board.AddBackControl(DataSourceManager.GetDataSource(key));
            }
        }






    }
}
