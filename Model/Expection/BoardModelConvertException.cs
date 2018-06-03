using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDesigner.Model.Data
{
    public class BoardModelConvertException : ApplicationException
    {
        private string error;
        private Exception innerException;
        //无参数构造函数
        public BoardModelConvertException()
        {

        }

        public BoardModelConvertException(string className,string value,string propertityName):base("初始化类对象[" + className + "]时出错，输入了错误的[" + value + "]赋予给属性[" + propertityName+"]")        
        {
            this.error = base.Message;            
        }
      

        public string GetError()
        {
            return error;
        }
    }
}
