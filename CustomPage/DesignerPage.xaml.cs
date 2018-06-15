using Board.BaseControl;
using Board.Controls.BoardControl;
using Board.DataHelper;
using Board.DesignerModel;
using Board.Interface;
using Board.Resource;
using BoardDesigner.Windows;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Maps;
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

namespace BoardDesigner.CustomPage
{
    /// <summary>
    /// DesignerPage.xaml 的交互逻辑
    /// </summary>
    public partial class DesignerPage : Page
    {      
        public string FilePath { get; set; }


        #region 

        public object SelectItem
        {
            get { return (object)GetValue(SelectItemProperty); }
            set
            {              
                SetValue(SelectItemProperty, value);
            }
        }
        public static readonly DependencyProperty SelectItemProperty =
            DependencyProperty.Register("SelectItem", typeof(object), typeof(DesignerPage), new PropertyMetadata(null, new PropertyChangedCallback(SelectItemPropertyChanged)));

        private static void SelectItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }


        #endregion
        public DesignerBoard Board
        {
            get { return (DesignerBoard)GetValue(BoardProperty); }
            set
            {
                SetValue(BoardProperty, value);
            }
        }
        public static readonly DependencyProperty BoardProperty =
          DependencyProperty.Register("Board", typeof(DesignerBoard), typeof(DesignerPage), new PropertyMetadata(
              new DesignerBoard()
              {
                  Size = new DesignerSize(1366, 768),
                  Background = new DesignerBrush(Color.FromRgb(255, 255, 255))
              }));

        #region 

        #endregion
       
        public DesignerPage()
        {
            InitializeComponent();
            CreateBoardDesignerPanel(Board);
            InitCustomCommand();

        }
        public DesignerPage(DesignerBoard db)
        {
           
            InitializeComponent();
            CreateBoardDesignerPanel(db);
            InitCustomCommand();          
        }
        #region 自定义命令
        private void InitCustomCommand()
        {
            //把命令赋值给命令源,并定义快捷键
            this.DataSourceSelectMenuItem.Command = DesignDataSourceCommand;
            //定义快捷键 gesture 手势
            //this.DesignDataSourceCommand.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
            //指定命令目标
            this.DataSourceSelectMenuItem.CommandTarget = this;

            //创建命令关联
            CommandBinding dsCommandBinding = new CommandBinding();
            //把命令rouutedCommand和具体的逻辑事件绑定起来。
            dsCommandBinding.Command = DesignDataSourceCommand;//只关注与rouutedCommand相关的命令
            dsCommandBinding.CanExecute += new CanExecuteRoutedEventHandler(DesignDataSource_CanExecute);
            dsCommandBinding.Executed += new ExecutedRoutedEventHandler(DesignDataSource_Execute);
            //把命令关联安置在外围控件上
            //MainWindow main = ControlHelper.GetParentObject<MainWindow>(this,"ThisWindow");
            this.CommandBindings.Add(dsCommandBinding);
        }

        private RoutedCommand DesignDataSourceCommand = new RoutedCommand("DataSource", typeof(MainWindow));
        void DesignDataSource_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.SelectItem == null)
                e.CanExecute = false;          
            else if (this.SelectItem  is IDynamicData)
            {
                e.CanExecute = true;
            }
            else
                e.CanExecute = false;
            e.Handled = true;
        }
        private void DesignDataSource_Execute(object sender, ExecutedRoutedEventArgs e)
        {           
            string key = null;
            if (this.SelectItem is DesignerChart) 
            {

            }
            else if (this.SelectItem is IDynamicData) 
            {
                key = (this.SelectItem as IDynamicData).DataSourceKey;
            }           

            DataSourceSettingWindow win = new DataSourceSettingWindow(key);
            if (win.ShowDialog() == true)
            {
                //更改Key，并通过管理器获取对象添加至数据源集合
                key = win.SelectedItem.Name;
                (this.SelectItem as IDynamicData).DataSourceKey = key;
                this.Board.AddBackControl(DataSourceManager.GetDataSource(key));
            }         
        }

        #endregion
        void CreateBoardDesignerPanel(DesignerBoard db)
        {
            DesignerCanvas dc = new DesignerCanvas(db);
            this.SetBinding(DesignerPage.BoardProperty, new Binding("Board") { Source = dc });
            this.SetBinding(DesignerPage.SelectItemProperty, new Binding("SelectItem") { Source = dc });
            DesignerGrid.Children.Add(dc);
            this.DataContext = Board;

            NameTB.SetBinding(TextBlock.TextProperty, new Binding("SelectItem.Name") { Source = dc });
           
        }      
      
    }
}
