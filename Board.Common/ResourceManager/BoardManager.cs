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

        public static List<string> GetRecents() 
        {
            //记录文件中读取
            string path = Directory.GetCurrentDirectory() + "\\DataSources\\RecentBoards.xml";
            if (!File.Exists(path))
            {
               StreamWriter sw =  File.CreateText(path);
               sw.Close();
            }

            return DataHelper.XmlHelper.XmlDerialize<List<string>>(File.ReadAllText(path));
        }

        public static void AddRecent(string newPath)
        {
            string path = Directory.GetCurrentDirectory() + "\\DataSources\\RecentBoards.xml";
            List<string> list=  DataHelper.XmlHelper.XmlDerialize<List<string>>(File.ReadAllText(path));
            list.Add(newPath);

            File.WriteAllText(path, DataHelper.XmlHelper.XmlSerialize(list));
        }


    }
}
