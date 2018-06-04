using BoardDesigner.Interface;
using BoardDesigner.Model;
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
                        MainPanel.Children.Add(bi);
                        break;
                    case DesignerElementType.Label:
                        BoardLabel bl = new BoardLabel(cd as DesignerLabel);
                        MainPanel.Children.Add(bl);
                        break;
                    case DesignerElementType.Table:
                        BoardDataGrid bdg = new BoardDataGrid(cd as DesignerTable);
                        MainPanel.Children.Add(bdg);
                        break;
                    default: break;
                }
            }
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
