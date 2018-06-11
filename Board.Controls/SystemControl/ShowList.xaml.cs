using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace Board.Controls.SystemControl
{
    public enum OpenPageTypes
    {
        UnKnow = 0,
        DesignerPage = 1,
        DocPage = 2,
        WebPage = 3,
        HelpPage = 4
    }
    /// <summary>
    /// ShowList.xaml 的交互逻辑
    /// </summary>
    public partial class ShowList : UserControl
    {
        #region 数据源
        public ObservableCollection<string> ItemsSource
        {
            get;
            set;
        }
        private void ItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object obj in e.NewItems)
                    {
                        AddItem(obj as string);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (object obj in e.OldItems)
                    {
                        RemoveItem(obj as string);
                    }
                    break;

                default: break;
            }
        }

        void InitItems()
        {

        }

        void AddItem(string item)
        {
            if (ContentGrid.Children.Count >= 10)
                return;
            Button bt = new Button();
            bt.Click += ItemClick;
            bt.Style = this.Resources["ShowListButtonStyle"] as Style;
            bt.Content = item;
            ContentGrid.Children.Add(bt);
            bt.SetValue(Grid.RowProperty, ContentGrid.Children.Count);
        }

        void RemoveItem(string item)
        {

        }
        #endregion

        #region 标题

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(ShowList), new PropertyMetadata("标题"));

        #endregion

        #region 打开页面类型

        public OpenPageTypes OpenPageType
        {
            get { return (OpenPageTypes)GetValue(OpenPageTypeProperty); }
            set { SetValue(OpenPageTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OpenPageType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenPageTypeProperty =
            DependencyProperty.Register("OpenPageType", typeof(OpenPageTypes), typeof(ShowList), new PropertyMetadata(OpenPageTypes.UnKnow));



        #endregion


        #region 控件属性


        #endregion

        public ShowList()
        {
            InitializeComponent();
            HeaderTextBlock.SetBinding(TextBlock.TextProperty, new Binding("HeaderText") { Source = this });
            ItemsSource = new ObservableCollection<string>();
            ItemsSource.CollectionChanged += ItemsSource_CollectionChanged;
        }

        public void ItemClick(object sender, RoutedEventArgs e)
        {
            string bc = (sender as Button).Content.ToString();
            switch (this.OpenPageType)
            {
                case OpenPageTypes.DesignerPage:
                    //根据关键词获取DesignerBoard对象

                    //初始化DesignerPage并打开
                    break;
                case OpenPageTypes.DocPage:
                    //根据关键词获取Doc路径
                    //打开Doc
                    break;
                case OpenPageTypes.HelpPage:
                    //根据关键词获取Help内容对象
                    //初始化Help也，并使用内容初始化它
                    break;
                case OpenPageTypes.WebPage:
                    //根据关键词获取地址
                    //打开Web浏览页，并使用地址初始化URL
                    break;
                default:
                    throw new Exception("未知的页面类型");

            }
            e.Handled = true;
        }

    }
}
