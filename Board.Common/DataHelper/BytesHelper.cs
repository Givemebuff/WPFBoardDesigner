using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Board.DataHelper
{
   public class BytesHelper
    {
       public static byte[] FormatterObjectBytes(object obj)
       {
           if (obj == null)
               throw new ArgumentNullException("obj");
           byte[] buff;
           try
           {
               using (var ms = new MemoryStream())
               {
                   IFormatter iFormatter = new BinaryFormatter();
                   iFormatter.Serialize(ms, obj);
                   buff = ms.GetBuffer();
               }
           }
           catch (Exception er)
           {
               throw new Exception(er.Message);
           }
           return buff;
       }

       /// <summary>  
       /// 将对象序列化为byte[]  
       /// 使用Marshal的StructureToPtr序列化  
       /// </summary>  
       /// <param name="obj">需序列化的对象</param>  
       /// <returns>序列化后的byte[]</returns>  
       public static byte[] MarshalObjectByte(object obj)
       {
           if (obj == null)
               throw new ArgumentNullException("obj");
           byte[] buff;
           try
           {
               buff = new byte[Marshal.SizeOf(obj)];
               var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buff, 0);
               Marshal.StructureToPtr(obj, ptr, true);
           }
           catch (Exception er)
           {
               throw new Exception(er.Message);
           }
           return buff;
       }  

       public static object FormatterByteObject(byte[] buff)
       {
           if (buff == null)
               throw new ArgumentNullException("buff");
           object obj;
           try
           {
               using (var ms = new MemoryStream())
               {
                   IFormatter iFormatter = new BinaryFormatter();
                   obj = iFormatter.Deserialize(ms);
               }
           }
           catch (Exception er)
           {
               throw new Exception(er.Message);
           }
           return obj;
       }

       /// <summary>  
       /// 将对象转为二进制文件，并保存到指定的文件中  
       /// </summary>  
       /// <param name="name">文件路径</param>  
       /// <param name="obj">待存的对象</param>  
       /// <returns></returns>  
       public static bool BinaryFileSave(string name, object obj)
       {
           Stream flstr = null;
           BinaryWriter binaryWriter = null;
           try
           {
               flstr = new FileStream(name, FileMode.Create);
               binaryWriter = new BinaryWriter(flstr);
               var buff = FormatterObjectBytes(obj);
               binaryWriter.Write(buff);
           }
           catch (Exception er)
           {
               throw new Exception(er.Message);
           }
           finally
           {
               if (binaryWriter != null) binaryWriter.Close();
               if (flstr != null) flstr.Close();
           }
           return true;
       }

       public static string GetMD5Hash(byte[] bytedata)
       {
           try
           {
               System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
               byte[] retVal = md5.ComputeHash(bytedata);



               StringBuilder sb = new StringBuilder();
               for (int i = 0; i < retVal.Length; i++)
               {
                   sb.Append(retVal[i].ToString("x2"));
               }
               return sb.ToString();
           }
           catch (Exception ex)
           {
               throw new Exception("GetMD5Hash() fail,error:" + ex.Message);
           }
       }
    }
}
