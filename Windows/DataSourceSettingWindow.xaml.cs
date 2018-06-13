using Board.DesignerModel;
using Board.Resource;
using Indusfo.Common;
using Indusfo.Data.DataAccessBaseLayer;
using Infragistics.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace BoardDesigner.Windows
{
    /// <summary>
    /// DataSourceSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataSourceSettingWindow : Window
    {



        public ObservableCollection<DesignerDataBaseDataSource> DataBaseDataSourceList
        {
            get { return (ObservableCollection<DesignerDataBaseDataSource>)GetValue(DataBaseDataSourceListProperty); }
            set { SetValue(DataBaseDataSourceListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataSourceList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataBaseDataSourceListProperty =
            DependencyProperty.Register("DataBaseDataSourceList", typeof(ObservableCollection<DesignerDataBaseDataSource>), typeof(DataSourceSettingWindow), new PropertyMetadata(DataBaseDataSourceManager.GetDataSources()));



        public DesignerDataSource SelectedItem
        {
            get { return (DesignerDataSource)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(DesignerDataSource), typeof(DataSourceSettingWindow), new PropertyMetadata(null));




        public DataSourceSettingWindow()
        {
            InitializeComponent();
            Init();
        }

        public DataSourceSettingWindow(string dsName)
        {
            InitializeComponent();
            Init();
            Select(dsName);
        }

        void Init()
        {          

        }

        void Select(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;
            foreach (DesignerDataBaseDataSource ds in DataBaseDataSourceList)
            {
                if (ds.Name == name)
                {
                    this.SelectedItem = ds;
                    break;
                }
            }
        }



        private void TestConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedItem == null)
                return;
            if (this.SelectedItem is DesignerDataBaseDataSource)
            {
                using (SqlExcuter se = new SqlExcuter(DataBaseType.SqlServer, (this.SelectedItem as DesignerDataBaseDataSource).ConnectionString))
                {
                    DataTable data = se.ExecuteSelectSql("select 1");
                    if (data == null)
                        MessageBox.Show("连接失败");
                    else
                        MessageBox.Show("连接成功");
                }
            }
        }

        private void DataViewButton_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                using (SqlExcuter se = new SqlExcuter(DataBaseType.SqlServer, (this.SelectedItem as DesignerDataBaseDataSource).ConnectionString))
                {
                    DataTable data = se.ExecuteSelectSql((this.SelectedItem as DesignerDataBaseDataSource).SqlString);
                    DataTablePreViewWindow win = new DataTablePreViewWindow();
                    win.Grid.ItemsSource = data.DefaultView;
                    win.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SQLDeButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO设计
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetDataObject(DSSQLTB.Text);
        }

        private void SQLClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.DSSQLTB.Clear();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DesignerDataSource ds = null;
            TabItemEx tabItem = MainTab.SelectedItem as TabItemEx;
            switch (tabItem.Header.ToString())
            {
                case "数据库":
                    ds = new DesignerDataBaseDataSource();
                    DataBaseDataSourceList.Add(ds as DesignerDataBaseDataSource);
                    break;
                case "本地文件":
                    throw new NotImplementedException();
                    break;
                case "网络资源":
                    throw new NotImplementedException();
                    break;
                case "静态文本":
                    throw new NotImplementedException();
                    break;
                default:
                    throw new NotImplementedException();  
            }       
            this.SelectedItem = ds;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //string path = Directory.GetCurrentDirectory() + "\\DataSources\\" + this.SelectedItem.Name + ".bds";
            //File.Delete(path);
            //this.DataBaseDataSourceList.Remove(this.SelectedItem);
            //this.SelectedItem = null;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (DesignerDataBaseDataSource ds in DataBaseDataSourceList)
            {
                DataBaseDataSourceManager.AddDataBaseDataSource(ds);
            }
            this.DialogResult = true;
        }

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void DSDBCB_DropDownOpened(object sender, EventArgs e)
        {
            if (this.SelectedItem == null)
                return;
            if (!(this.SelectedItem is DesignerDataBaseDataSource))
                return;
            DesignerDataBaseDataSource ds = SelectedItem as DesignerDataBaseDataSource;
            if (!string.IsNullOrEmpty(ds.Address) && !string.IsNullOrEmpty(ds.UserName) && !string.IsNullOrEmpty(ds.PassWord))
            {
                string constr = string.Format("Server={0};database={1};uid={2};pwd={3}", ds.Address, "master", ds.UserName, ds.PassWord);
                using (SqlExcuter se = new SqlExcuter(DataBaseType.SqlServer, constr))
                {
                    DataTable data = se.ExecuteSelectSql("select name from master..sysdatabases");
                    DSDBCB.Items.Clear();
                    foreach (DataRow r in data.Rows)
                    {
                        string n = r["name"].ToString();
                        DSDBCB.Items.Add(n);
                    }
                }
            }
            else
            {
                MessageBox.Show("请输入完整信息");
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DSList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                this.SelectedItem = e.AddedItems[0] as DesignerDataBaseDataSource;
            }
        }
    }
}
