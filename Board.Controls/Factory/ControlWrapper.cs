using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Board.Factory
{
    public class ControlWrapper : Control
    {
        private object _content { get; set; }

        public void Protect(object control) 
        {
            if (_content == null)
                _content = control;
        }

        public object Tear() 
        {
            return _content;
        }
    }
}
