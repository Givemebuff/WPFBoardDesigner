﻿using Board.DesignerModel;
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
    /// BoardDataGridContentTexkBlock.xaml 的交互逻辑
    /// </summary>
    public partial class BoardDataGridContentTexkBlock : UserControl, IDesigner
    {
        public DesignerTable DesignerModel { get; set; }
        public BoardDataGridContentTexkBlock()
        {
            InitializeComponent();
            DesignerModel = new DesignerTable()
            {
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            InitBinding();
        }

        public BoardDataGridContentTexkBlock(DesignerTable dt, string text)
        {
            InitializeComponent();
            DesignerModel = dt;
            uTextBlock.Text = text;
            InitBinding();
        }

        void InitBinding()
        {
            this.DataContext = DesignerModel;
        }
        public object GetDesignerModel()
        {
            return this.DesignerModel;
        }


    }
}
