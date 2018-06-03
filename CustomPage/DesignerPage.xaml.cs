using BoardDesigner.Base;
using BoardDesigner.BoardControl;
using BoardDesigner.Model;
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
        #region 编辑对象

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
              new DesignerBoard() { 
                  Size = new DesignerSize(1366, 758),
                  Background = new DesignerBrush(Color.FromRgb(255, 255, 255))//new DesignerBrush(Color.FromRgb(255, 255, 255))//new SolidColorBrush(Color.FromRgb(255, 255, 255))//
              },
              new PropertyChangedCallback(BoardPropertyChanged)));

        private static void BoardPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {        
            ((DesignerPage)d).MainPanel.SelectItem = e.NewValue;
        }


        #endregion
       
        public DesignerPage()
        {
            InitializeComponent();
            this.MainPanel.DataContext = Board;
        }
    }
}
