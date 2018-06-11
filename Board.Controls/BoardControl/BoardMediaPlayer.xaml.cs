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

namespace Board.Controls.BoardControl
{
    /// <summary>
    /// BoardMediaPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class BoardMediaPlayer : UserControl, IDesigner, IWorker
    {
        public DesignerMedia DesignerItem { get; set; }



        public Uri MediaSourceUri
        {
            get { return (Uri)GetValue(MediaSourceUriProperty); }
            set { SetValue(MediaSourceUriProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MediaSourceUri.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaSourceUriProperty =
            DependencyProperty.Register("MediaSourceUri", typeof(Uri), typeof(BoardMediaPlayer), new PropertyMetadata(null));

        private static void MediaSourceUriPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoardMediaPlayer bmp = d as BoardMediaPlayer;
            if (e.NewValue != null)
                bmp.uMedio.Play();
        }



        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Speed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(BoardMediaPlayer), new PropertyMetadata(0.5, SpeedPropertyChanged));


        private static void SpeedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoardMediaPlayer bmp = d as BoardMediaPlayer;
            bmp.uMedio.SpeedRatio = (double)e.NewValue;
        }

        public BoardMediaPlayer()
        {
            InitializeComponent();
            DesignerItem = new DesignerMedia();
            InitBinding();
        }
        public BoardMediaPlayer(DesignerMedia dm)
        {
            InitializeComponent();
            DesignerItem = dm;
            InitBinding();
        }

        public object GetDesignerItem()
        {
            return this.DesignerItem;
        }


        public void InitBinding()
        {
            this.DataContext = DesignerItem;
            this.SetBinding(MediaSourceUriProperty, new Binding("Source") { Source = uMedio, Mode = BindingMode.OneWay });
            this.SetBinding(SpeedProperty, new Binding("SpeedRatio") { Source = DataContext });
            this.uMedio.Source = new Uri(System.IO.Directory.GetCurrentDirectory() + "\\Medias\\" + DesignerItem.MediaSource, UriKind.Absolute);
        }

        private void uMedio_MediaEnded(object sender, RoutedEventArgs e)
        {
            uMedio.Position = new TimeSpan(0, 0, 0);
            uMedio.Play();
        }

        public void StartWork()
        {
            if (MediaSourceUri == null)
                return;
            if (System.IO.File.Exists(MediaSourceUri.AbsolutePath))
                uMedio.Play();
        }
    }
}
