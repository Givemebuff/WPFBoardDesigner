using Board.Controls.BoardControl;
using Board.DesignerModel;
using Board.Interface;
using BoardDesigner.UControl;
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
using System.Windows.Shapes;

namespace BoardDesigner
{
    /// <summary>
    /// BoardViewer.xaml 的交互逻辑
    /// </summary>
    public partial class BoardViewer : Window
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
            Init();
            Work();
        }

        void Init() 
        {
            if (Board == null)
                return;
            this.Height = Board.Size.Height;
            this.Width = Board.Size.Width;

            foreach (DesignerControl cd in Board.Children) 
            {                
                
                switch (cd.Type)
                {
                    case DesignerElementType.Image:
                        BoardImage bi = new BoardImage(cd as DesignerImage);
                        Add(bi, cd);
                        break;
                    case DesignerElementType.Label:
                        BoardLabel bl = new BoardLabel(cd as DesignerLabel);
                        Add(bl, cd);
                        break;
                    case DesignerElementType.Table:
                        BoardDataGrid bdg = new BoardDataGrid(cd as DesignerTable);
                        Add(bdg, cd);
                        break;
                    case DesignerElementType.Chart:
                        BoardChart bc = new BoardChart(cd as DesignerChart);
                        Add(bc, cd);
                        break;
                    default: break;
                }
            }
        }


        void Add(UserControl uc,DesignerControl dc) 
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
