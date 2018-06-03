using BoardDesigner.BoardControl;
using BoardDesigner.BoardControl.BaseControl;
using BoardDesigner.BoardControl.BaseShape;
using BoardDesigner.BoardControl.ChartControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoardDesigner.Factory
{
    public class CandyFactory
    {
        public static Dictionary<string, Type> ClassDictionary;

        public static ControlWrapper CreateCandyByName(string name)
        {
            if (ClassDictionary.Keys.Contains(name))
            {
                Type type = ClassDictionary[name];
                ICandy candy = (ICandy)type.Assembly.CreateInstance(type.FullName);
                return candy.GetCandy();
            }
            else
                throw new Exception("未注册的类名");        

        }
        static CandyFactory()
        {
            ClassDictionary = new Dictionary<string, Type>();
            //基本图形
            ClassDictionary.Add("Line", typeof(LineCandy));
            ClassDictionary.Add("SpLine", typeof(SpLineCandy));
            ClassDictionary.Add("Rect", typeof(RectCandy));
            ClassDictionary.Add("Circle", typeof(CircleCandy));
            //ClassDictionary.Add("Triangle", typeof(LineCandy));
            //ClassDictionary.Add("Star", typeof(LineCandy));
            //ClassDictionary.Add("Heart", typeof(LineCandy));
            //基本控件
            ClassDictionary.Add("Label", typeof(LabelCandy));
            ClassDictionary.Add("Image", typeof(ImageCandy));
            ClassDictionary.Add("GIF", typeof(GIFCandy));
            ClassDictionary.Add("Video", typeof(VideoCandy));
           // ClassDictionary.Add("Video", typeof(VideoCandy));//浏览器
            //图表控件
            ClassDictionary.Add("Table", typeof(TableCandy));
            ClassDictionary.Add("LineChart", typeof(LineChartCandy));
            ClassDictionary.Add("SpLineChart", typeof(SpLineChartCandy));
            ClassDictionary.Add("ColumnChart", typeof(ColumnChartCandy));
            ClassDictionary.Add("PieChart", typeof(PieChartCandy));
            ClassDictionary.Add("AreaChart", typeof(AreaChartCandy));
        }
    }
}
