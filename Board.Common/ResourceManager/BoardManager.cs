using Board.DataHelper;
using Board.DesignerModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Board.Resource
{
    public class BoardManager
    {
        public static void SaveBoard(DesignerBoard board,string path) 
        {
            try 
            {
                string xml = XmlHelper.XmlSerialize<DesignerBoard>(board);
                File.WriteAllText(path, xml);
            }
            catch (Exception e)
            {
                LogManager.WriteLog("保存看板文件时出错," + e.Message);
            }
           
           
        }
    }
}
