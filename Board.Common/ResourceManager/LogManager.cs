using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Resource
{
    public class LogManager
    {
        static string DateStr
        {
            get
            {
                return DateTime.Now.ToShortDateString();
            }
        }
        static string FileName
        {
            get { return "Log" + DateStr + ".txt"; }
        }
        static string FloderPath
        {
            get
            {
                return Directory.GetCurrentDirectory() + "\\Logs\\";
            }
        }
        static string FilePath
        {
            get
            {
                return FloderPath + FileName;
            }
        }
        public static void WillWrite()
        {
            if (!Directory.Exists(FloderPath))
            {
                Directory.CreateDirectory(FloderPath);
            }
            if (!File.Exists(FilePath))
            {
                FileStream fs1 = new FileStream(FilePath, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine("创建于" + DateTime.Now.ToShortTimeString());//开始写入值
                sw.Close();
                fs1.Close();
            }
        }
        public static void WriteLog(string message)
        {
            try
            {
                WillWrite();
                string inMessage = "[" + DateTime.Now.ToLongTimeString() + "]发生异常：\n" + message;
                StreamWriter sr = File.AppendText(FilePath);
                sr.WriteLine(inMessage);//开始写入值
                sr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("写入日志时出错：" + e.Message);
            }
        }
    }
}
