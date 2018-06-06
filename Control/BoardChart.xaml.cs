using BoardDesigner.Converter;
using BoardDesigner.Interface;
using BoardDesigner.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using Visifire.Charts;

namespace BoardDesigner.UControl
{
    /// <summary>
    /// BoardChart.xaml 的交互逻辑
    /// </summary>
    public partial class BoardChart : UserControl, IDesigner
    {
        public DesignerChart DesignerItem { get; set; }

        #region 标题

        public ObservableCollection<DesignerChartTitle> ChartTitles
        {
            get { return (ObservableCollection<DesignerChartTitle>)GetValue(ChartTitlesProperty); }
            set { SetValue(ChartTitlesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartTitlesProperty =
            DependencyProperty.Register("ChartTitles",
            typeof(ObservableCollection<DesignerChartTitle>),
            typeof(BoardChart),
            new PropertyMetadata(new ObservableCollection<DesignerChartTitle>(), new PropertyChangedCallback(ChartTitlesPropertyChanged)));

        private static void ChartTitlesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoardChart bc = d as BoardChart;
            ObservableCollection<DesignerChartTitle> titles = e.NewValue as ObservableCollection<DesignerChartTitle>;
            bc.InitTitle(titles);
        }

        void ChartTitles_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object obj in e.NewItems)
                    {
                        AddTitle(obj as DesignerChartTitle);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (object obj in e.OldItems)
                    {
                        RemoveTitle(obj as DesignerChartTitle);
                    }
                    break;

                default: break;
            }
        }

        void AddTitle(DesignerChartTitle newTitle)
        {
            Title t = new Title();
            //t.DataContext = newTitle;
            t.Uid = newTitle.ID.ToString();

            t.SetBinding(Title.BackgroundProperty, new Binding("TitleBackground") { Source = newTitle });

            t.SetBinding(Title.BorderColorProperty, new Binding("TitleBorder.BorderBrush") { Source = newTitle });
            t.SetBinding(Title.BorderThicknessProperty, new Binding("TitleBorder.BorderThickness") { Source = newTitle });
            t.SetBinding(Title.CornerRadiusProperty, new Binding("TitleBorder.CornerRadius") { Source = newTitle });

            t.SetBinding(Title.TextProperty, new Binding("Text") { Source = newTitle });

            t.SetBinding(Title.FontColorProperty, new Binding("TitleFont.FontColor") { Source = newTitle });
            t.SetBinding(Title.FontFamilyProperty, new Binding("TitleFont.FontFamily") { Source = newTitle });
            t.SetBinding(Title.FontSizeProperty, new Binding("TitleFont.FontSize") { Source = newTitle });
            t.SetBinding(Title.FontStyleProperty, new Binding("TitleFont.FontStyle") { Source = newTitle });
            t.SetBinding(Title.FontWeightProperty, new Binding("TitleFont.FontWeight") { Source = newTitle });

            Chart uChart = this.Content as Chart;
            if (uChart.Titles == null)
                uChart.Titles = new TitleCollection();
            if (!uChart.Titles.Contains(t))
                uChart.Titles.Add(t);
        }

        void RemoveTitle(DesignerChartTitle title)
        {
            Chart uChart = this.Content as Chart;
            for (int i = 0; i < uChart.Titles.Count; i++)
            {
                if (uChart.Titles[i].Uid == title.ID.ToString())
                    uChart.Titles.Remove(uChart.Titles[i]);
            }
        }

        public void InitTitle(ObservableCollection<DesignerChartTitle> titles)
        {
            Chart uChart = this.Content as Chart;
            if (uChart.Titles == null)
                uChart.Titles = new TitleCollection();
            uChart.Titles.Clear();
            foreach (DesignerChartTitle t in titles)
            {
                AddTitle(t);
            }
        }

        #endregion

        #region X轴

        public ObservableCollection<DesignerChartAxis> ChartAxesX
        {
            get { return (ObservableCollection<DesignerChartAxis>)GetValue(ChartAxesXProperty); }
            set { SetValue(ChartAxesXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartAxesXProperty =
            DependencyProperty.Register("ChartAxesX",
            typeof(ObservableCollection<DesignerChartAxis>),
            typeof(BoardChart),
            new PropertyMetadata(new ObservableCollection<DesignerChartAxis>(), new PropertyChangedCallback(ChartAxesXPropertyChanged)));

        private static void ChartAxesXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoardChart bc = d as BoardChart;
            ObservableCollection<DesignerChartAxis> xs = e.NewValue as ObservableCollection<DesignerChartAxis>;
            bc.InitAxesX(xs);
        }

        void ChartAxesX_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object obj in e.NewItems)
                    {
                        AddAxisX(obj as DesignerChartAxis);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (object obj in e.OldItems)
                    {
                        RemoveAxisX(obj as DesignerChartAxis);
                    }
                    break;

                default: break;
            }
        }

        void AddAxisX(DesignerChartAxis newX)
        {
            Chart uChart = this.Content as Chart;
            Axis nX = new Axis();
            nX.Uid = newX.ID.ToString();
            nX.SetBinding(Axis.AxisMaximumProperty, new Binding("AxisMaximum") { Source = newX });
            nX.SetBinding(Axis.AxisMinimumProperty, new Binding("AxisMinimum") { Source = newX });
            nX.SetBinding(Axis.AxisTypeProperty, new Binding("AxisType") { Source = newX });
            nX.SetBinding(Axis.LogarithmicProperty, new Binding("Logarithmic") { Source = newX });
            nX.SetBinding(Axis.SuffixProperty, new Binding("Suffix") { Source = newX });
            nX.SetBinding(Axis.IncludeZeroProperty, new Binding("IncludeZero") { Source = newX });
            nX.SetBinding(Axis.IntervalTypeProperty, new Binding("IntervalType") { Source = newX });
            nX.SetBinding(Axis.IntervalProperty, new Binding("Interval") { Source = newX });
            nX.SetBinding(Axis.LineThicknessProperty, new Binding("LineThickness") { Source = newX });
            nX.SetBinding(Axis.LineStyleProperty, new Binding("LineStyle") { Source = newX });
            nX.SetBinding(Axis.LineColorProperty, new Binding("LineColor") { Source = newX });
            nX.SetBinding(Axis.AxisTypeProperty, new Binding("AxisType") { Source = newX });
            nX.SetBinding(Axis.BackgroundProperty, new Binding("AxisBackground") { Source = newX });
            if (uChart.AxesX == null)
                uChart.AxesX = new AxisCollection();
            if (!uChart.AxesX.Contains(nX))
                uChart.AxesX.Add(nX);
        }

        void RemoveAxisX(DesignerChartAxis X)
        {
            Chart uChart = this.Content as Chart;
            for (int i = 0; i < uChart.AxesX.Count; i++)
            {
                if (uChart.AxesX[i].Uid == X.ID.ToString())
                    uChart.AxesX.Remove(uChart.AxesX[i]);
            }
        }

        public void InitAxesX(ObservableCollection<DesignerChartAxis> xs)
        {

            Chart uChart = this.Content as Chart;
            if (uChart.AxesX == null)
                uChart.AxesX = new AxisCollection();
            uChart.AxesX.Clear();

            foreach (DesignerChartAxis x in xs)
            {
                AddAxisX(x);
            }
        }

        #endregion

        #region Y轴

        public ObservableCollection<DesignerChartAxis> ChartAxesY
        {
            get { return (ObservableCollection<DesignerChartAxis>)GetValue(ChartAxesYProperty); }
            set { SetValue(ChartAxesYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartAxesYProperty =
            DependencyProperty.Register("ChartAxesY",
            typeof(ObservableCollection<DesignerChartAxis>),
            typeof(BoardChart),
            new PropertyMetadata(new ObservableCollection<DesignerChartAxis>(), new PropertyChangedCallback(ChartAxesYPropertyChanged)));

        private static void ChartAxesYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoardChart bc = d as BoardChart;
            ObservableCollection<DesignerChartAxis> xs = e.NewValue as ObservableCollection<DesignerChartAxis>;
            bc.InitAxesY(xs);
        }

        void ChartAxesY_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object obj in e.NewItems)
                    {
                        AddAxisY(obj as DesignerChartAxis);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (object obj in e.OldItems)
                    {
                        RemoveAxisY(obj as DesignerChartAxis);
                    }
                    break;

                default: break;
            }
        }

        void AddAxisY(DesignerChartAxis newY)
        {
            Chart uChart = this.Content as Chart;
            Axis nY = new Axis();
            nY.Uid = newY.ID.ToString();
            nY.SetBinding(Axis.AxisMaximumProperty, new Binding("AxisMaximum") { Source = newY });
            nY.SetBinding(Axis.AxisMinimumProperty, new Binding("AxisMinimum") { Source = newY });
            nY.SetBinding(Axis.AxisTypeProperty, new Binding("AxisType") { Source = newY });
            nY.SetBinding(Axis.LogarithmicProperty, new Binding("Logarithmic") { Source = newY });
            nY.SetBinding(Axis.SuffixProperty, new Binding("Suffix") { Source = newY });
            nY.SetBinding(Axis.IncludeZeroProperty, new Binding("IncludeZero") { Source = newY });
            nY.SetBinding(Axis.IntervalTypeProperty, new Binding("IntervalType") { Source = newY });
            nY.SetBinding(Axis.IntervalProperty, new Binding("Interval") { Source = newY });
            nY.SetBinding(Axis.LineThicknessProperty, new Binding("LineThickness") { Source = newY });
            nY.SetBinding(Axis.LineStyleProperty, new Binding("LineStyle") { Source = newY });
            nY.SetBinding(Axis.LineColorProperty, new Binding("LineColor") { Source = newY });
            nY.SetBinding(Axis.AxisTypeProperty, new Binding("AxisType") { Source = newY });
            nY.SetBinding(Axis.BackgroundProperty, new Binding("AxisBackground") { Source = newY });
            if (uChart.AxesY == null)
                uChart.AxesY = new AxisCollection();
            if (!uChart.AxesY.Contains(nY))
                uChart.AxesY.Add(nY);
        }

        void RemoveAxisY(DesignerChartAxis Y)
        {
            Chart uChart = this.Content as Chart;

            for (int i = 0; i < uChart.AxesY.Count; i++)
            {
                if (uChart.AxesY[i].Uid == Y.ID.ToString())
                    uChart.AxesY.Remove(uChart.AxesY[i]);
            }
        }

        public void InitAxesY(ObservableCollection<DesignerChartAxis> xs)
        {

            Chart uChart = this.Content as Chart;
            if (uChart.AxesY == null)
                uChart.AxesY = new AxisCollection();
            uChart.AxesY.Clear();

            foreach (DesignerChartAxis x in xs)
            {
                AddAxisY(x);
            }
        }

        #endregion

        #region 数据线

        public ObservableCollection<DesignerChartDataSeries> ChartDataSeries
        {
            get { return (ObservableCollection<DesignerChartDataSeries>)GetValue(ChartDataSeriesProperty); }
            set { SetValue(ChartDataSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartDataSeriesProperty =
            DependencyProperty.Register("ChartDataSeries",
            typeof(ObservableCollection<DesignerChartDataSeries>),
            typeof(BoardChart),
            new PropertyMetadata(new ObservableCollection<DesignerChartDataSeries>(), new PropertyChangedCallback(ChartDataSeriesPropertyChanged)));

        private static void ChartDataSeriesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoardChart bc = d as BoardChart;
            ObservableCollection<DesignerChartDataSeries> ss = e.NewValue as ObservableCollection<DesignerChartDataSeries>;
            bc.InitDataSeries(ss);
        }

        void ChartDataSeries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object obj in e.NewItems)
                    {
                        AddSeries(obj as DesignerChartDataSeries);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (object obj in e.OldItems)
                    {
                        RemoveSeries(obj as DesignerChartDataSeries);
                    }
                    break;

                default: break;
            }
        }

        void AddSeries(DesignerChartDataSeries newSeries)
        {
            newSeries.DataPoints.CollectionChanged += DataPoints_CollectionChanged;
            DataSeries s = new Visifire.Charts.DataSeries();
            //线属性
            s.Uid = newSeries.ID.ToString();
            s.SetBinding(DataSeries.AutoFitToPlotAreaProperty, new Binding("AutoFitToPlotArea") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.AxisXTypeProperty, new Binding("AxisXType") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.AxisYTypeProperty, new Binding("AxisYType") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.BorderColorProperty, new Binding("SeriesBorder.BorderBrush") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.BorderStyleProperty, new Binding("BorderStyle") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.BorderThicknessProperty, new Binding("SeriesBorder.BorderThickness") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.ColorProperty, new Binding("Color") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.DataPointsProperty, new Binding("DataPoints") { Source = newSeries,Converter = (IValueConverter)(new DesignerDataPointCollectionToVisifireDataPointCollectionConverter())});
            s.SetBinding(DataSeries.HighLightColorProperty, new Binding("HighLightColor") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.IncludeDataPointsInLegendProperty, new Binding("IncludeDataPointsInLegend") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.IncludePercentageInLegendProperty, new Binding("IncludePercentageInLegend") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.IncludeYValueInLegendProperty, new Binding("IncludeYValueInLegend") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelBackgroundProperty, new Binding("LabelBackground") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelEnabledProperty, new Binding("LabelEnabled") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelFontColorProperty, new Binding("LabelFont.FontColor") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelFontFamilyProperty, new Binding("LabelFont.FontFamily") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelFontSizeProperty, new Binding("LabelFont.FontSize") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelFontStyleProperty, new Binding("LabelFont.FontStyle") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelFontWeightProperty, new Binding("LabelFont.FontWeight") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelLineColorProperty, new Binding("LabelLineColor") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelLineEnabledProperty, new Binding("LabelLineEnabled") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelLineStyleProperty, new Binding("LabelLineStyle") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelLineThicknessProperty, new Binding("LabelLineThickness") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelStyleProperty, new Binding("LabelStyle") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelTextProperty, new Binding("LabelText") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LegendMarkerColorProperty, new Binding("LegendMarkerColor") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LegendMarkerTypeProperty, new Binding("LegendMarkerType") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LegendTextProperty, new Binding("LegendText") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LightingEnabledProperty, new Binding("LightingEnabled") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LineCapProperty, new Binding("LineCap") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LineFillProperty, new Binding("LineFill") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LineStyleProperty, new Binding("LineStyle") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LineTensionProperty, new Binding("LineTension") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LineThicknessProperty, new Binding("LineThickness") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.MarkerBorderColorProperty, new Binding("MarkerBorderColor") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.MarkerBorderThicknessProperty, new Binding("MarkerBorderThickness") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.MarkerColorProperty, new Binding("MarkerColor") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.MarkerEnabledProperty, new Binding("MarkerEnabled") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.MarkerScaleProperty, new Binding("MarkerScale") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.MarkerSizeProperty, new Binding("MarkerSize") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.MarkerTypeProperty, new Binding("MarkerType") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.RadiusXProperty, new Binding("RadiusX") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.RadiusYProperty, new Binding("RadiusY") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.RenderAsProperty, new Binding("RenderAs") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.ShadowEnabledProperty, new Binding("ShadowEnabled") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.ShowInLegendProperty, new Binding("ShowInLegend") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.LabelAngleProperty, new Binding("LabelAngle") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.XValueTypeProperty, new Binding("XValueType") { Source = newSeries, Mode = BindingMode.OneWay });
            s.SetBinding(DataSeries.ZIndexProperty, new Binding("ZIndex") { Source = newSeries, Mode = BindingMode.OneWay });
                   


            Chart uChart = this.Content as Chart;
            if (uChart.Series == null)
                uChart.Series = new DataSeriesCollection();
            if (!uChart.Series.Contains(s))
                uChart.Series.Add(s);
        }

        void RemoveSeries(DesignerChartDataSeries s)
        {
            Chart uChart = this.Content as Chart;
            for (int i = 0; i < uChart.Series.Count; i++)
            {
                if (uChart.Series[i].Uid == s.ID.ToString())
                    uChart.Series.Remove(uChart.Series[i]);
            }
        }

        public void InitDataSeries(ObservableCollection<DesignerChartDataSeries> ss)
        {
            Chart uChart = this.Content as Chart;
            if (uChart.Series == null)
                uChart.Series = new DataSeriesCollection();
            uChart.Series.Clear();
            foreach (DesignerChartDataSeries s in ss)
            {
                AddSeries(s);
            }
        }

        void DataPoints_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //获取所属数据线，找到对应线，对该线进行数据点增删
            DesignerDataPointCollection ddplist = sender as DesignerDataPointCollection;
            string uid = (ddplist.Parent as DesignerChartDataSeries).ID.ToString();
            DataSeries ds = null;
            Chart uChart = this.Content as Chart;
            foreach (DataSeries s in uChart.Series)
            {
                if (s.Uid == uid)
                {
                    ds = s;
                    break;
                }
            }
            if (ds == null)
                throw new Exception("添加数据点时找不到数据线");
          
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object obj in e.NewItems)
                    {
                        
                        AddDataPoint(ds, obj as DesignerDataPoint);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (object obj in e.OldItems)
                    {
                        RemoveDataPoint(ds, obj as DesignerDataPoint);
                    }
                    break;

                default: break;
            }
            
        }    
       


        void AddDataPoint(DataSeries ds, DesignerDataPoint dp)
        {
            
            DataPoint newDp = new DataPoint();
            newDp.Uid = dp.ID.ToString();
            newDp.Name = dp.Name;
            newDp.SetBinding(DataPoint.AxisXLabelProperty, new Binding("AxisXLabel") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.BorderColorProperty, new Binding("LabelBorder.BorderBrush") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.BorderStyleProperty, new Binding("BorderStyle") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.BorderThicknessProperty, new Binding("LabelBorder.BorderThickness") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.ColorProperty, new Binding("Color") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.EnabledProperty, new Binding("Enabled") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelAngleProperty, new Binding("LabelAngle") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelBackgroundProperty, new Binding("LabelBackground") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelEnabledProperty, new Binding("LabelEnabled") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelFontColorProperty, new Binding("LabelFont.FontColor") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelFontFamilyProperty, new Binding("LabelFont.FontFamily") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelFontSizeProperty, new Binding("LabelFont.FontSize") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelFontStyleProperty, new Binding("LabelFont.FontStyle") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelFontWeightProperty, new Binding("LabelFont.FontWeight") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelLineColorProperty, new Binding("LabelLineColor") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelLineEnabledProperty, new Binding("LabelLineEnabled") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelLineStyleProperty, new Binding("LabelLineStyle") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelLineThicknessProperty, new Binding("LabelLineThickness") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelStyleProperty, new Binding("LabelStyle") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LabelTextProperty, new Binding("LabelText") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LegendMarkerColorProperty, new Binding("LegendMarkerColor") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LegendMarkerTypeProperty, new Binding("LegendMarkerType") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LegendTextProperty, new Binding("LegendText") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.LightingEnabledProperty, new Binding("LightingEnabled") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.MarkerBorderColorProperty, new Binding("MarkerBorderColor") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.MarkerBorderThicknessProperty, new Binding("MarkerBorderThickness") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.MarkerColorProperty, new Binding("MarkerColor") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.MarkerEnabledProperty, new Binding("MarkerEnabled") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.MarkerScaleProperty, new Binding("MarkerScale") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.MarkerSizeProperty, new Binding("MarkerSize") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.MarkerTypeProperty, new Binding("MarkerType") { Source = dp, Mode = BindingMode.OneWay });

            newDp.SetBinding(DataPoint.OpacityProperty, new Binding("DataPointOpacity") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.RadiusXProperty, new Binding("RadiusX") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.RadiusYProperty, new Binding("RadiusY") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.ShadowEnabledProperty, new Binding("ShadowEnabled") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.ShowInLegendProperty, new Binding("ShowInLegend") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.StickColorProperty, new Binding("StickColor") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.XValueProperty, new Binding("XValue") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.YValueProperty, new Binding("YValue") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.YValuesProperty, new Binding("YValues") { Source = dp, Mode = BindingMode.OneWay });
            newDp.SetBinding(DataPoint.ZValueProperty, new Binding("ZValue") { Source = dp, Mode = BindingMode.OneWay });

            if (ds.DataPoints == null)
                ds.DataPoints = new DataPointCollection();
            ds.DataPoints.Add(newDp);
            

        }
        void RemoveDataPoint(DataSeries ds, DesignerDataPoint dp)
        {
            var array = ds.DataPoints.Where((d) => d.Name == dp.Name);
            //TODO 是否内存泄漏
            foreach (IDataPoint idp in array)
            {
                ds.DataPoints.Remove(idp);
            }
        }


        #endregion
        public object GetDesignerItem()
        {
            return this.DesignerItem;
        }

        public BoardChart()
        {
            InitializeComponent();
            DesignerItem = new DesignerChart();
            Init();
        }

        public BoardChart(DesignerChart dc)
        {
            InitializeComponent();
            DesignerItem = dc;
            Init();
        }
        public void Init()
        {
            this.DataContext = DesignerItem;
            Binding canvasLeft = new Binding("Position.Location.X") { Source = DataContext };
            this.SetBinding(Canvas.LeftProperty, canvasLeft);
            Binding canvasTop = new Binding("Position.Location.Y") { Source = DataContext };
            this.SetBinding(Canvas.TopProperty, canvasTop);

            this.SetBinding(ChartTitlesProperty, new Binding("ChartTitles") { Source = DataContext });
            this.SetBinding(ChartAxesXProperty, new Binding("ChartAxesX") { Source = DataContext });
            this.SetBinding(ChartAxesYProperty, new Binding("ChartAxesY") { Source = DataContext });
            this.SetBinding(ChartDataSeriesProperty, new Binding("Series") { Source = DataContext });

            ChartTitles.CollectionChanged += ChartTitles_CollectionChanged;
            ChartAxesX.CollectionChanged += ChartAxesX_CollectionChanged;
            ChartAxesY.CollectionChanged += ChartAxesY_CollectionChanged;
            ChartDataSeries.CollectionChanged += ChartDataSeries_CollectionChanged;
        }

    }
}
