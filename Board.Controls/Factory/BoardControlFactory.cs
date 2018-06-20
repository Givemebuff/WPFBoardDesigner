using Board.DesignerModel;

using Board.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Board.BoardControl
{
    public class BoardControlFactory
    {
        public static Dictionary<string, Type> StringToClassDictionary;
        public static Dictionary<Type, Type> ClassToClassDictionary;

        public static Board.Factory.ControlWrapper CreateCandyByName(string name)
        {
            if (StringToClassDictionary.Keys.Contains(name))
            {
                Type type = StringToClassDictionary[name];
                ICandy candy = (ICandy)type.Assembly.CreateInstance(type.FullName);
                return candy.GetCandy();
            }
            else
                throw new Exception("未注册的类名");        

        }

        public static UserControl CreateBoardControlByDesignerType(Type type,DesignerControl dc)
        {
            if (ClassToClassDictionary.Keys.Contains(type))
            {
                Type controlType = ClassToClassDictionary[type];
                ICandy candy = (ICandy)controlType.Assembly.CreateInstance(controlType.FullName);
                return candy.GetCandy(dc);
            }
            else
                throw new Exception("未注册的类名");

        }

        static BoardControlFactory()
        {
            StringToClassDictionary = new Dictionary<string, Type>();
            ClassToClassDictionary = new Dictionary<Type, Type>();
            //基本控件
            StringToClassDictionary.Add("Label", typeof(LabelCandy));
            StringToClassDictionary.Add("DynamicLabel", typeof(DynamicLabelCandy));
            StringToClassDictionary.Add("Image", typeof(ImageCandy));
            StringToClassDictionary.Add("GIF", typeof(GIFCandy));
            StringToClassDictionary.Add("Video", typeof(VideoCandy));
            StringToClassDictionary.Add("Clock", typeof(ClockCandy));   
            StringToClassDictionary.Add("Table", typeof(TableCandy));
            StringToClassDictionary.Add("Chart", typeof(LineChartCandy));
            StringToClassDictionary.Add("ProcessBar", typeof(ProcessBar));         
  
            ///
            ClassToClassDictionary.Add(typeof(DesignerLabel), typeof(LabelCandy));
            ClassToClassDictionary.Add(typeof(DesignerDynamicLabel), typeof(DynamicLabelCandy));
            ClassToClassDictionary.Add(typeof(DesignerImage), typeof(ImageCandy));
            ClassToClassDictionary.Add(typeof(DesignerGif), typeof(GIFCandy));
            ClassToClassDictionary.Add(typeof(DesignerMedia), typeof(VideoCandy));
            ClassToClassDictionary.Add(typeof(DesignerClock), typeof(ClockCandy));
            ClassToClassDictionary.Add(typeof(DesignerTable), typeof(TableCandy));
            ClassToClassDictionary.Add(typeof(DesignerChart), typeof(LineChartCandy));
            ClassToClassDictionary.Add(typeof(DesignerProcessbar), typeof(ProcessBar));
        }
    }
}
