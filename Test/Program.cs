using Board.DesignerModel;
using Board.Resource;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //DesignerDataBaseDataSource ds = new DesignerDataBaseDataSource()
            //{
            //    SqlString = "select * from Money",
            //    UserName = "Indusfo",
            //    PassWord = "Indusfo123456",
            //    DataBaseName = "Test",
            //    Address = "Factory.indusfo.club"
            //};

            //DataSourceManager.Register(ds.Name, DesignerDataSourceType.DataBase, ds);

            //DataTable data = DataSourceManager.GetDataAsync(ds.Name).Result as DataTable;

            //Console.WriteLine("我在下面");
          

            //while (true) 
            //{
            //    string s = Console.ReadLine();
            //    Console.WriteLine("异步的同时输入了：" + s);
            //}
            Console.Read();
        }

        public static async Task<int> Methond1()
        {
            int i = 0;
            while (i < 10)
            {
                i++;
                await Task.Delay(1000);
                Console.WriteLine("M1：" + i.ToString());
            }
            return i;
        }
        public static async Task<int> Methond2()
        {
            int i = 0;
            while (i < 20)
            {
                i++;
                await Task.Delay(500);
                Console.WriteLine("M2：" + i.ToString());
            }
            return i;
        }

        private static async void Cal()
        {
            Task<int> a = Methond1();
            //此处可继续执行其他代码
            await a;//等待任务a完成
            Task<int> b = Methond2();
            //此处可继续执行其他代码
            int c = await b;//等待任务b完成，且可以拿到任务b的返回值
            Console.Write("Result:" + c);
        }


       
    }



}
