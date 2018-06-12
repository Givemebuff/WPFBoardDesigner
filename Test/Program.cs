using Board.DesignerModel;
using Board.Resource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            DesignerDataBaseDataSource ds = new DesignerDataBaseDataSource()
            {
                SqlString = "select * from Chart",
                Address = "192.168.1.103",
                DataBaseName = "test",
                UserName = "sa",
                PassWord = "123456"
            };          

            DataSourceManager.Register(ds.Name, DesignerDataSourceType.DataBase, ds);

            DataTable data = DataSourceManager.GetDataAsync(ds.Name) as DataTable;

            Console.WriteLine("我在下面");
            

            Console.Read();
        }
    }
}
