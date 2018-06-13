using Board.DesignerModel;
using Board.Resource;
using Indusfo.Common;
using Indusfo.Data.DataAccessBaseLayer;
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



        public ObservableCollection<DesignerDataSource> DataSourceList
        {
            get { return (ObservableCollection<DesignerDataSource>)GetValue(DataSourceListProperty); }
            set { SetValue(DataSourceListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataSourceList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceListProperty =
            DependencyProperty.Register("DataSourceList", typeof(ObservableCollection<DesignerDataSource>), typeof(DataSourceSettingWindow), new PropertyMetadata(DataBaseDataSourceManager.GetDataSources()));

        

        public DesignerDataBaseDataSource SelectedItem
        {
            get { return (DesignerDataBaseDataSource)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(DesignerDataBaseDataSource), typeof(DataSourceSettingWindow), new PropertyMetadata(null));


        

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
            DataSourceList.CollectionChanged += DataSourceList_CollectionChanged;
            this.DSList.SetBinding(ListBox.ItemsSourceProperty, new Binding("DataSourceList") { Source = this });
            this.DSList.SetBinding(ListBox.SelectedItemProperty, new Binding("SelectedItem") { Source = this });
           
        }

        void Select(string name) 
        {
            foreach (DesignerDataBaseDataSource ds in DataSourceList) 
            {
                if (ds.Name == name)
                {
                    this.SelectedItem = ds;
                    break; 
                }
            }
        }

        void DataSourceList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action) 
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    break;
            }
        }

        private void TestConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            using(SqlExcuter se =new SqlExcuter(DataBaseType.SqlServer,this.SelectedItem.ConnectionString))
            {
                DataTable data = se.ExecuteSelectSql("select 1");
                if (data == null)
                    MessageBox.Show("连接失败");
                else
                    MessageBox.Show("连接成功");
            }
        }

        private void DataViewButton_Click(object sender, RoutedEventArgs e)
        {

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

            DesignerDataBaseDataSource ds = new DesignerDataBaseDataSource();
            DataSourceList.Add(ds);
            this.SelectedItem = ds;           
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + "\\DataSources\\" + this.SelectedItem.Name + ".bds";
            File.Delete(path);
            this.DataSourceList.Remove(this.SelectedItem);
            this.SelectedItem = null;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (DesignerDataSource ds in DataSourceList) 
            {
                MemoryStream Stream = new MemoryStream();
                XmlSerializer xml = new XmlSerializer(typeof(DesignerDataSource));
                //序列化对象  
                xml.Serialize(Stream, this.SelectedItem);
                Stream.Position = 0;
                StreamReader sr = new StreamReader(Stream);
                string str = sr.ReadToEnd();
                //存到文件
                string path = Directory.GetCurrentDirectory() + "\\DataSources\\";
                FileStream fs = new FileStream(path+ds.Name+".bds", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs); // 创建写入流
                sw.Write(str);
                sw.Close(); //关闭文件
                sr.Dispose();
                Stream.Dispose();
            }           
            this.DialogResult = true; 
        }

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void DSDBCB_DropDownOpened(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SelectedItem.Address) && !string.IsNullOrEmpty(this.SelectedItem.UserName) && !string.IsNullOrEmpty(this.SelectedItem.PassWord)) 
            {
                string constr = string.Format("Server={0};database={1};uid={2};pwd={3}", this.SelectedItem.Address, "master", this.SelectedItem.UserName, this.SelectedItem.PassWord);
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
    }
}
