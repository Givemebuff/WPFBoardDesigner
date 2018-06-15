using Board.DesignerModel;
using Board.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Board.Controls.BoardControl
{
    /// <summary>
    /// BoardClock.xaml 的交互逻辑
    /// </summary>
    public partial class BoardClock : UserControl, IDesigner, IWorker
    {
        public DesignerClock DesignerModel { get; set; }

        private DispatcherTimer Timer { get; set; }

        public BoardClock()
        {
            InitializeComponent();
            DesignerModel = new DesignerClock();
            InitBinding();
        }

        public BoardClock(DesignerClock tb)
        {
            InitializeComponent();
            DesignerModel = tb;
            InitBinding();
        }
        public void InitBinding()
        {
            this.DataContext = DesignerModel;
        }

        public object GetDesignerModel()
        {
            return this.DesignerModel;
        }

        public void StartWork()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(() => { this.DesignerModel.Text = DateTime.Now.ToString(); });
        }

       
    }
}
