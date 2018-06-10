using Board.DesignerModel;
using BoardDesigner.Base;
using BoardDesigner.BoardControl;



using Infragistics.Controls.Editors;
using Infragistics.Controls.Maps;
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

namespace BoardDesigner.CustomPage
{
    /// <summary>
    /// DesignerPage.xaml 的交互逻辑
    /// </summary>
    public partial class DesignerPage : Page
    {
        public string FilePath { get; set; }


        #region 

        public object SelectItem
        {
            get { return (object)GetValue(SelectItemProperty); }
            set
            {              
                SetValue(SelectItemProperty, value);
            }
        }
        public static readonly DependencyProperty SelectItemProperty =
            DependencyProperty.Register("SelectItem", typeof(object), typeof(DesignerPage), new PropertyMetadata(null, new PropertyChangedCallback(SelectItemPropertyChanged)));

        private static void SelectItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }


        #endregion
        public DesignerBoard Board
        {
            get { return (DesignerBoard)GetValue(BoardProperty); }
            set
            {
                SetValue(BoardProperty, value);
            }
        }
        public static readonly DependencyProperty BoardProperty =
          DependencyProperty.Register("Board", typeof(DesignerBoard), typeof(DesignerPage), new PropertyMetadata(
              new DesignerBoard()
              {
                  Size = new DesignerSize(1366, 768),
                  Background = new DesignerBrush(Color.FromRgb(255, 255, 255))
              }));

        #region 

        #endregion
       
        public DesignerPage()
        {
            InitializeComponent();           
            CreateBoardDesignerPanel(Board);
        }
        public DesignerPage(DesignerBoard db)
        {
            InitializeComponent();           
            CreateBoardDesignerPanel(db);           
        }

        void CreateBoardDesignerPanel(DesignerBoard db)
        {
            DesignerCanvas dc = new DesignerCanvas(db);
            this.SetBinding(DesignerPage.BoardProperty, new Binding("Board") { Source = dc });
            this.SetBinding(DesignerPage.SelectItemProperty, new Binding("SelectItem") { Source = dc });
            DesignerGrid.Children.Add(dc);
            this.DataContext = Board;
           
        }
    }
}
