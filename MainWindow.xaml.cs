using BoardDesigner.Base;
using BoardDesigner.CustomPage;
using BoardDesigner.Model;
using Infragistics.Controls.Editors;
using Infragistics.Windows.Ribbon;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoardDesigner
{
    

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {            


        public DesignerCanvas CurrentDesignerCanvas
        {
            get { return (DesignerCanvas)GetValue(CurrentDesignerCanvasProperty); }
            set { SetValue(CurrentDesignerCanvasProperty, value); }
        }

        public static readonly DependencyProperty CurrentDesignerCanvasProperty =
          DependencyProperty.Register("CurrentDesignerCanvas", typeof(DesignerCanvas), 
          typeof(MainWindow),
          new PropertyMetadata(null, new PropertyChangedCallback(CurrentDesignerCanvasPropertyChanged)));

        private static void CurrentDesignerCanvasPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {        
            MainWindow mw =d as MainWindow;
            if (mw.CurrentDesignerCanvas != null) 
            {               
                Binding bd = new Binding("SelectItem"){Source = mw.CurrentDesignerCanvas};
                mw.xamPropertyGrid.SetBinding(XamPropertyGrid.SelectedObjectProperty, bd);               
            }
        }  

       

        public MainWindow()
        {
            InitializeComponent();                       
            contentPage.Content = new Frame() { Content = new DesignerPage() };
            helpPage.Content = new Frame() { Content = new HelpPage() };      
        }



        private void DocumentContentHost_ActiveDocumentChanged(object sender, RoutedPropertyChangedEventArgs<Infragistics.Windows.DockManager.ContentPane> e)
        {
            if (e.NewValue == null)
                return;
            //NewValue时ig的ContentPane
            //下一层ContentControl
            if (e.NewValue.Content == null)
                return;
            //下一层Frame
            Frame frame = (e.NewValue.Content as ContentControl).Content as Frame;
            if (frame == null)
                return;
            //下一层 DesignerPage HelperPage等
            if (frame.Content is DesignerPage)
            {
                DesignerPage page = frame.Content as DesignerPage;
                if (page == null)
                    return;
                //下一层
                CurrentDesignerCanvas = page.MainPanel;
            }
            else
            {
                CurrentDesignerCanvas = null;                
            }
        }     


        
    }
}
