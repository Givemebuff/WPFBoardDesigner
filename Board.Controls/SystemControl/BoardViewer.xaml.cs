using Board.Controls.BoardControl;
using Board.DesignerModel;
using Board.Interface;
using Board.Resource;
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

namespace Board.Controls.SystemControl
{
    /// <summary>
    /// BoardViewer.xaml 的交互逻辑
    /// </summary>
    public partial class BoardViewer : UserControl
    {
        DesignerBoard Board { get; set; }
        public BoardViewer()
        {
            InitializeComponent();
        }

        public BoardViewer(DesignerBoard board)
        {
            InitializeComponent();
            Board = board;
            this.DataContext = Board;
            InitBack();
            InitVisual();           
            Work();
        }

        void InitVisual()
        {
            if (Board == null)
                return;
            this.Height = Board.Size.Height;
            this.Width = Board.Size.Width;

            foreach (DesignerControl cd in Board.VisualChildren)
            {               
                switch (cd.ControlType)
                {
                    case DesignerControlType.Image:
                        BoardImage bi = new BoardImage(cd as DesignerImage);
                        Add(bi, cd);
                        break;
                    case DesignerControlType.GIF:
                        BoardGif bg = new BoardGif(cd as DesignerGif);
                        Add(bg, cd);
                        break;
                    case DesignerControlType.Label:
                        BoardLabel bl = new BoardLabel(cd as DesignerLabel);
                        Add(bl, cd);
                        break;
                    case DesignerControlType.Table:
                        BoardDataGrid bdg = new BoardDataGrid(cd as DesignerTable);
                        Add(bdg, cd);
                        break;
                    case DesignerControlType.Chart:
                        BoardChart bc = new BoardChart(cd as DesignerChart);
                        Add(bc, cd);
                        break;
                    case DesignerControlType.Media:
                        BoardMediaPlayer bmp = new BoardMediaPlayer(cd as DesignerMedia);
                        Add(bmp, cd);
                        break;
                    case DesignerControlType.Clock:
                        BoardClock clock = new BoardClock(cd as DesignerClock);
                        Add(clock, cd);
                        break;
                    case DesignerControlType.DynamicLabel:
                        BoardDynamicLabel dlb = new BoardDynamicLabel(cd as DesignerDynamicLabel);
                        Add(dlb, cd);
                        break;
                    case DesignerControlType.Processbar:
                        BoardProcessbar bpd = new BoardProcessbar(cd as DesignerProcessbar);
                        Add(bpd, cd);
                        break;                 
                    default: break;
                }
                
            }
        }

        void InitBack() 
        {
            foreach (DesignerDataSource ds in Board.BackChildren) 
            {
                DataSourceManager.Register(ds.Name, ds.DataSourceType, ds);
                DataSourceManager.InitTimer(ds.Name);
                DataSourceManager.BeginTimer(ds.Name);
            }
        }


        void Add(UserControl uc, DesignerControl dc)
        {
            uc.SetBinding(Canvas.LeftProperty, new Binding("Position.Location.X") { Source = dc });
            uc.SetBinding(Canvas.TopProperty, new Binding("Position.Location.Y") { Source = dc });
            uc.SetBinding(Canvas.ZIndexProperty, new Binding("Position.ZIndex") { Source = dc });
            MainPanel.Children.Add(uc);
        }

        void Work()
        {
            foreach (object obj in MainPanel.Children)
            {
                if (obj is IWorker)
                {
                    (obj as IWorker).StartWork();
                }
            }
        }
    }
}
