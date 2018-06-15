using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Board.DataHelper
{
    public class CopyHelper
    {

        /// <summary>
        /// Copy Propertys and Fileds 
        /// 拷贝属性和公共字段
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        //public static void CopyToNew<T>(this object source, T target) where T : class
        //{
        //    if (source == null)
        //    {
        //        return;
        //    }

        //    if (target == null)
        //    {
        //        throw new ApplicationException("target 未实例化！");
        //    }


        //    var properties = target.GetType().GetProperties();
        //    foreach (var targetPro in properties)
        //    {
        //        try
        //        {
        //            //判断源对象是否存在与目标属性名字对应的源属性
        //            if (source.GetType().GetProperty(targetPro.Name) == null)
        //            {
        //                continue;
        //            }
        //            //数据类型不相等
        //            if (targetPro.PropertyType.FullName != source.GetType().GetProperty(targetPro.Name).PropertyType.FullName)
        //            {
        //                continue;
        //            }
        //            var propertyValue = source.GetType().GetProperty(targetPro.Name).GetValue(source, null);
        //            if (propertyValue != null)
        //            {

        //                target.GetType().InvokeMember(targetPro.Name, BindingFlags.SetProperty, null, target, new object[] { propertyValue });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    //返回所有公共字段
        //    var targetFields = target.GetType().GetFields();
        //    foreach (var filed in targetFields)
        //    {
        //        try
        //        {
        //            var tfield = source.GetType().GetField(filed.Name);
        //            if (null == tfield)
        //            {
        //                //如果源对象中不包含这个公共字段则不处理
        //                continue;
        //            }
        //            //类型不一致不处理
        //            if (filed.FieldType.FullName != tfield.FieldType.FullName)
        //            {
        //                continue;
        //            } var fieldValue = tfield.GetValue(source);
        //            if (fieldValue != null)
        //            {
        //                target.GetType().InvokeMember(filed.Name, BindingFlags.SetField, null, target, new object[] { fieldValue });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
    }
}
