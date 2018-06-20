using Board.DesignerModel;
using Board.Interface;
using Board.Resource;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Processbar.xaml 的交互逻辑
    /// </summary>
    public partial class BoardProcessbar : UserControl, IDesigner, IWorker
    {
        public DesignerProcessbar DesignerModel { get; set; }
        public BoardProcessbar()
        {
            InitializeComponent();
            DesignerModel = new DesignerProcessbar() { Value = 50, Color = new DesignerBrush() { ColorBrush = new SolidColorBrush(Color.FromRgb(0, 255, 0)) } };
            Init();
        }
        public BoardProcessbar(DesignerProcessbar dpb)
        {
            InitializeComponent();
            DesignerModel = dpb;
            Init();
        }
        private void Init()
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
            Timer.Interval = new TimeSpan(0, 0,0,0, DesignerModel.DataAccessTimeSpan);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke
                (() =>
                {
                    DataTable data = DataSourceManager.GetData(this.DesignerModel.DataSourceKey) as DataTable;
                    if (data == null || data.Rows.Count <= 0)
                        return;
                    else
                    {
                        this.DesignerModel.Value = Convert.ToDouble(data.Rows[0][this.DesignerModel.BindName]);
                    }
                });
        }
        private DispatcherTimer Timer { get; set; }
    }
}
