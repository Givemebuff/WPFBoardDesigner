using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Board.XmlConverter
{
    public  class GridLengthConverter
    {
        public static GridLength XmlToGridLength(string xml)
        {
            GridLength length = new GridLength();
            if (string.IsNullOrEmpty(xml)) 
            {
                length = new GridLength(1, GridUnitType.Star);
            }
            else if(xml.IndexOf("*")!=-1)
            {
                string ws = xml.Replace("*", "");
                if (string.IsNullOrEmpty(ws)) 
                {
                    length = new GridLength(1, GridUnitType.Star);
                }
                else 
                {
                    length = new GridLength(Convert.ToDouble(ws), GridUnitType.Star);
                }
            }
            else if (xml.IndexOf("*") == -1)
            {
                string ws = xml.Replace("*", "");
                length = new GridLength(Convert.ToDouble(ws));
            }
            return length; 
           
        }
    }
}
