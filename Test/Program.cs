using Board.DesignerModel;
using Board.Resource;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //    SqlString = "select * from Chart",
            //    Address = "192.168.1.103",
            //    DataBaseName = "test",
            //    UserName = "sa",
            //    PassWord = "123456"
            //};          

            //DataSourceManager.Register(ds.Name, DesignerDataSourceType.DataBase, ds);

            //DataTable data = DataSourceManager.GetDataAsync(ds.Name) as DataTable;

            //Console.WriteLine("我在下面");

            DesignerBoard b = new DesignerBoard();
            DesignerLabel l = new DesignerLabel();
            b.Children.Add(l);
            DesignerBrush db = new DesignerBrush() { ColorBrush = new SolidColorBrush(Color.FromArgb(10, 23, 45, 67)) };
            string ss = db.ToString();
            string s = XmlSerialize(db);
            
            Console.Read();
        }

        static string XmlSerialize<T>(T obj) 
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(typeof(T));
            //序列化对象  
            xml.Serialize(Stream, obj);
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();
            sr.Dispose();
            return str;
        }
    }
}
