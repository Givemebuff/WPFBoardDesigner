using Board.SystemModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BoardDesigner.Resource
{
    public class ImageResourcesManeager
    {
        public static IEnumerable<RImage> GetImageResources()
        {
            //加载图片资源
            List<RImage> imgs = new List<RImage>();
            //文件夹下图片
            string curDirPath = Directory.GetCurrentDirectory() + @"\Images";
            //若不存在文件夹则先创建
            if (!Directory.Exists(curDirPath))
            {
                Directory.CreateDirectory(curDirPath);
            }
            DirectoryInfo di = new DirectoryInfo(curDirPath);
            FileInfo[] fis = di.GetFiles();
            foreach (FileInfo fi in fis)
            {
                string ext = fi.Extension.ToLower();
                if (ext.IndexOf("jpg") == -1 && ext.IndexOf("png") == -1 && ext.IndexOf("gif") == -1 && ext.IndexOf("bmp") == -1)
                    continue;
                else
                {
                    RImage image = new RImage(fi);
                    imgs.Add(image);
                }
            }
            return imgs;
        }
    }
}
