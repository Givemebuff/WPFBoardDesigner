using Board.Converter.Converter.BindConverter;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Board.Controls.BoardControl
{
    /// <summary>
    /// BoardDynamicLabel.xaml 的交互逻辑
    /// </summary>
    public partial class BoardDynamicLabel : UserControl,IDesigner,IWorker
    {
        #region FormatText
        public string FormatText
        {
            get { return (string)GetValue(FormatTextProperty); }
            set { SetValue(FormatTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FormatText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatTextProperty =
            DependencyProperty.Register("FormatText", typeof(string), typeof(BoardDynamicLabel), new PropertyMetadata("动态数据文本"));       

        #endregion

        #region IDesginer
        public DesignerDynamicLabel DesignerModel { get; set; }
        public object GetDesignerModel()
        {
            return this.DesignerModel;          
        }

        #endregion

        public BoardDynamicLabel()
        {
            InitializeComponent();
            DesignerModel = new DesignerDynamicLabel();
            Init();
        }

        public BoardDynamicLabel(DesignerDynamicLabel ddl)
        {
            InitializeComponent();
            DesignerModel = ddl;
            Init();

        }

        void Init() 
        {
            this.DataContext = DesignerModel;
            MultiBinding bind = new MultiBinding();
            Binding b1 = new Binding("Text") { Source = DataContext };
            Binding b2 = new Binding("BindName") { Source = DataContext };
            Binding b3 = new Binding("FormatString") { Source = DataContext };
            bind.Converter = (IMultiValueConverter)(new DynamicLabelFormatTextConverter());
            bind.Bindings.Add(b1);
            bind.Bindings.Add(b2);
            bind.Bindings.Add(b3);
            this.SetBinding(FormatTextProperty, bind);
        }

        public DispatcherTimer Timer { get; set; }
        public void StartWork()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, this.DesignerModel.DataAccessTimeSpan);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        async void Timer_Tick(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(this.DesignerModel.DataSourceKey))
                return;
            DataTable data = (await Task<object>.Run(
                ()=>{
                    return DataSourceManager.GetData(this.DesignerModel.DataSourceKey);
                })) as DataTable;
            if (data == null || data.Rows.Count <= 0)
                return;
            else 
            {
                DoubleAnimation opacityAnimation2 = new DoubleAnimation();
                opacityAnimation2.From = 0;//透明度初始值
                opacityAnimation2.To = 1;//透明度值
                opacityAnimation2.Duration = new Duration(TimeSpan.FromSeconds(2));
                uLabel.BeginAnimation(Label.OpacityProperty, opacityAnimation2);
                this.DesignerModel.Text = data.Rows[0][this.DesignerModel.BindName].ToString();               
            }
        }
    }
}
