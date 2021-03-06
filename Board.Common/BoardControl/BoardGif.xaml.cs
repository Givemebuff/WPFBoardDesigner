﻿using Board.Converter;
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
using XamlAnimatedGif;

namespace Board.Controls.BoardControl
{
    /// <summary>
    /// BoardGif.xaml 的交互逻辑
    /// </summary>
    public partial class BoardGif : UserControl, IDesigner
    {
        public DesignerGif DesignerModel { get; set; }



        public BoardGif()
        {
            InitializeComponent();
            DesignerModel = new DesignerGif();
            DesignerModel.Size.Width = 200;
            DesignerModel.Size.Height = 200;

            InitBindind();
        }

        public BoardGif(DesignerGif dg)
        {
            InitializeComponent();
            DesignerModel = dg;
            InitBindind();
        }


        private void InitBindind()
        {
            this.DataContext = DesignerModel;
            uImage.SetBinding(AnimationBehavior.AutoStartProperty, new Binding("AutoPlay") { Source = DataContext });
            uImage.SetBinding(AnimationBehavior.SourceUriProperty, new Binding("GIFImageSource") { Source = DataContext, Converter = (IValueConverter)(new StringToUriConverterForImage()) });
        }

        public object GetDesignerModel()
        {
            return this.DesignerModel;
        }

    }
}
