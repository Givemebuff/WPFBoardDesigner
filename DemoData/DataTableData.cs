using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDesigner.DemoData
{
    public class DemoDataTableData
    {
        public static DataTable DataTableData1
        {
            get
            {
                DataTable data = new DataTable();
                data.Columns.Add("X");
                data.Columns.Add("Y");
                for (int i = 0; i < 10; i++)
                {
                    DataRow r = data.NewRow();
                    r["X"] = i + 1;
                    r["Y"] = 10 * (i + 1);
                    data.Rows.Add(r);
                }
                return data;
            }
        }
        public static DataTable DataTableData2
        {
            get
            {
                DataTable data = new DataTable();
                data.Columns.Add("Month");
                data.Columns.Add("Money");
                DataRow r1 = data.NewRow();
                r1["Month"] = 1;
                r1["Money"] = 1000;
                DataRow r2 = data.NewRow();
                r2["Month"] = 2;
                r2["Money"] = 3000;
                DataRow r3 = data.NewRow();
                r3["Month"] = 3;
                r3["Money"] = 8000;
                DataRow r4 = data.NewRow();
                r4["Month"] = 4;
                r4["Money"] = 6000;
                DataRow r5 = data.NewRow();
                r5["Month"] = 5;
                r5["Money"] = 1350;
                DataRow r6 = data.NewRow();
                r6["Month"] = 6;
                r6["Money"] = 5000;
                data.Rows.Add(r1);
                data.Rows.Add(r2);
                data.Rows.Add(r3);
                data.Rows.Add(r4);
                data.Rows.Add(r5);
                data.Rows.Add(r6);
                return data;
            }
        }
        public static DataTable DataTableData3
        {
           get{            
                DataTable data = new DataTable();
                data.Columns.Add("Column1");
                data.Columns.Add("Column2");
                data.Columns.Add("Column3");
                data.Columns.Add("Column4");
                data.Columns.Add("Column5");
                Random rd = new Random();
                for (int i = 0; i < 50;i++ )
                {
                    DataRow nr = data.NewRow();
                    
                    for (int j = 1; j <= 5; j++) 
                    {   
                        nr["Column" + j] = rd.Next(1, 10000);
                    }
                    data.Rows.Add(nr);

                }
                    return data;
           }
           
        }
    }
}
