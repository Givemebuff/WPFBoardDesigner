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
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            InitLastOpen();
            InitDocExm();
            InitResources();
            InitHelp();
        }

        private void InitHelp()
        {
            
        }

        private void InitResources()
        {
            
        }

        private void InitDocExm()
        {
            //加载文档资源库
           DocExampleList.ItemsSource.Add("新建看板");           
        }

        private void InitLastOpen()
        {
            //获取最近打开的记录
            //遍历记录，获取前十条有效记录
            //加载
            LastOpenList.ItemsSource.Add("新建看板");
            
        }
    }
}
