using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Visifire.Charts;
using Visifire.Commons;

namespace BoardDesigner.Model
{
    public class DesignerChart : DesignerControl
    {
        public DesignerChart()
        {
            this.Type = DesignerElementType.Chart;
            this.ChartTitles = new ObservableCollection<DesignerChartTitle>();
            this.ChartTitles.Add(new DesignerChartTitle());
            this.ChartAxesX = new ObservableCollection<DesignerChartAxis>();
            this.ChartAxesX.Add(new DesignerChartAxis());
            this.ChartAxesY = new ObservableCollection<DesignerChartAxis>();
            this.ChartAxesY.Add(new DesignerChartAxis());
            this.Series = new ObservableCollection<DesignerChartDataSeries>();
            this.Series.Add(new DesignerChartDataSeries());
        }
        #region 标题
        [Browsable(false)]
        public ObservableCollection<DesignerChartTitle> _chartTitles { get; set; }

        [Category("图表")]
        [DisplayName("标题集合")]
        [Description("设置标题")]
        public ObservableCollection<DesignerChartTitle> ChartTitles
        {
            get { return this._chartTitles; }
            set
            {
                this._chartTitles = value;
                OnPropertyChanged("ChartTitles");
            }
        }
        #endregion

        #region X轴

        [Browsable(false)]
        public ObservableCollection<DesignerChartAxis> _chartAxesX { get; set; }

        [Category("图表")]
        [DisplayName("X轴集合")]
        [Description("设置X轴")]
        public ObservableCollection<DesignerChartAxis> ChartAxesX
        {
            get { return this._chartAxesX; }
            set
            {
                this._chartAxesX = value;
                OnPropertyChanged("ChartAxesX");
            }
        }

        #endregion

        #region Y轴

        [Browsable(false)]
        public ObservableCollection<DesignerChartAxis> _chartAxesY { get; set; }

        [Category("图表")]
        [DisplayName("Y轴集合")]
        [Description("设置Y轴")]
        public ObservableCollection<DesignerChartAxis> ChartAxesY
        {
            get { return this._chartAxesY; }
            set
            {
                this._chartAxesY = value;
                OnPropertyChanged("ChartAxesY");
            }
        }

        #endregion

        #region 数据

        #region 数据线

        [Browsable(false)]
        public ObservableCollection<DesignerChartDataSeries> _series { get; set; }

        [Category("图表数据")]
        [DisplayName("数据线集合")]
        [Description("数据线集合")]
        public ObservableCollection<DesignerChartDataSeries> Series
        {
            get { return this._series; }
            set
            {
                this._series = value;
                OnPropertyChanged("Series");
            }
        }

        #endregion

        #endregion

    }
    public class DesignerChartTitle : DesignerVisualElement
    {

        public DesignerChartTitle()
        {
            this.Text = "图表标题";
            this.TitleFont = new DesignerFont();
            this.TitleBorder = new DesignerBorder() { BorderThickness = new System.Windows.Thickness(0) };
        }

        [Browsable(false)]
        public DesignerFont _titleFont
        {
            get;
            set;
        }

        [Category("基础")]
        [DisplayName("标题字体")]
        [Description("设置标字体")]
        public DesignerFont TitleFont
        {
            get
            {
                return this._titleFont;
            }
            set
            {
                this._titleFont = value;
                OnPropertyChanged("TitleFont");
            }
        }

        [Browsable(false)]
        public string _text
        {
            get;
            set;
        }
        [Category("基础")]
        [DisplayName("标题文本")]
        [Description("设置标题文本")]
        public string Text
        {
            get { return this._text; }
            set
            {
                this._text = value;
                OnPropertyChanged("Text");
            }
        }

        [Browsable(false)]
        public DesignerBorder _titleBorder
        {
            get;
            set;
        }
        [Category("基础")]
        [DisplayName("标题边框")]
        [Description("设置标题边框")]
        public DesignerBorder TitleBorder
        {
            get { return this._titleBorder; }
            set
            {
                this._titleBorder = value;
                OnPropertyChanged("TitleBorder");
            }
        }
        [Browsable(false)]
        public Brush _titleBackground
        {
            get;
            set;
        }
        [Category("基础")]
        [DisplayName("标题背景")]
        [Description("设置标题背景")]
        public Brush TitleBackground
        {
            get
            {
                return this._titleBackground;
            }
            set
            {
                this._titleBackground = value;
                OnPropertyChanged("TitleBackground");
            }
        }
    }
    public class DesignerChartAxis : DesignerVisualElement
    {

        public DesignerChartAxis()
        {
            this.AxisType = AxisTypes.Primary;
            this.LineColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            this.LineStyle = LineStyles.Solid;
            this.LineThickness = 1;
            this.AxisMaximum = 1;
            this.AxisMinimum = 0;
            this.Interval = 1;
            this.IntervalType = IntervalTypes.Auto;
            this.IncludeZero = true;
        }

        #region 轴类型
        [Browsable(false)]
        public AxisTypes _axisType
        {
            get;
            set;
        }
        [Category("轴")]
        [DisplayName("轴类型")]
        [Description("设置轴类型")]
        public AxisTypes AxisType
        {
            get { return this._axisType; }
            set
            {
                this._axisType = value;
                OnPropertyChanged("AxisType");
            }
        }
        #endregion

        #region 轴线
        [Browsable(false)]
        public Brush _lineColor
        {
            get;
            set;
        }
        [Category("轴")]
        [DisplayName("轴颜色")]
        [Description("设置轴颜色")]
        public Brush LineColor
        {
            get { return this._lineColor; }
            set
            {
                this._lineColor = value;
                OnPropertyChanged("LineColor");
            }
        }

        [Browsable(false)]
        public LineStyles _lineStyle
        {
            get;
            set;
        }
        [Category("轴")]
        [DisplayName("轴线类型")]
        [Description("设置轴线类型")]
        public LineStyles LineStyle
        {
            get { return this._lineStyle; }
            set
            {
                this._lineStyle = value;
                OnPropertyChanged("LineStyle");
            }
        }

        [Browsable(false)]
        public double _lineThickness
        {
            get;
            set;
        }
        [Category("轴")]
        [DisplayName("轴线粗细")]
        [Description("设置轴线粗细")]
        public double LineThickness
        {
            get { return this._lineThickness; }
            set
            {
                this._lineThickness = value;
                OnPropertyChanged("LineThickness");
            }
        }



        #endregion

        #region 轴数据
        [Browsable(false)]
        public object _axisMaximum { get; set; }

        [Category("轴")]
        [DisplayName("轴最大值")]
        [Description("设置轴最大值")]
        public object AxisMaximum
        {
            get
            {
                return this._axisMaximum;
            }
            set
            {
                this._axisMaximum = value;
                OnPropertyChanged("AxisMaximum");
            }
        }
        [Browsable(false)]
        public object _axisMinimum { get; set; }
        [Category("轴")]
        [DisplayName("轴最小值")]
        [Description("设置轴最小值")]
        public object AxisMinimum
        {
            get
            {
                return this._axisMinimum;
            }
            set
            {
                this._axisMinimum = value;
                OnPropertyChanged("AxisMinimum");
            }
        }
        [Browsable(false)]
        public double _interval { set; get; }
        [Category("轴")]
        [DisplayName("轴坐标间隔")]
        [Description("设置轴坐标间隔")]
        public double Interval
        {
            get { return this._interval; }
            set
            {
                this._interval = value;
                OnPropertyChanged("Interval");
            }
        }
        [Browsable(false)]
        public IntervalTypes _intervalType { set; get; }
        [Category("轴")]
        [DisplayName("轴坐标间隔类型")]
        [Description("设置轴坐标间隔类型")]
        public IntervalTypes IntervalType
        {
            get { return this._intervalType; }
            set
            {
                this._intervalType = value;
                OnPropertyChanged("IntervalType");
            }
        }

        #endregion

        #region 其他

        [Browsable(false)]
        public bool _includeZero
        {
            get;
            set;
        }
        [Category("轴")]
        [DisplayName("包括零点")]
        [Description("是否包括零点")]
        public bool IncludeZero
        {
            get { return this._includeZero; }
            set
            {
                this._includeZero = value;
                OnPropertyChanged("IncludeZero");
            }
        }

        public string _suffix { get; set; }
        [Category("轴")]
        [DisplayName("修饰字符")]
        [Description("单位之类的")]
        public string Suffix
        {
            get
            {
                return this._suffix;
            }
            set
            {
                this._suffix = value;
                OnPropertyChanged("Suffix");
            }
        }

        public bool _logarithmic { get; set; }
        [Category("轴")]
        [DisplayName("对数形式")]
        [Description("对数形式")]
        public bool Logarithmic
        {
            get { return this._logarithmic; }
            set
            {
                this._logarithmic = value;
                OnPropertyChanged("Logarithmic");
            }
        }

        #region 背景

        [Browsable(false)]
        public Brush _axisbackground { get; set; }

        [Category("轴")]
        [DisplayName("背景")]
        [Description("背景设置")]
        public Brush AxisBackground
        {
            get { return this._axisbackground; }
            set
            {
                this._axisbackground = value;
                OnPropertyChanged("AxisBackground");
            }
        }

        #endregion


        #endregion
    }
    public class DesignerChartDataSeries : DesignerVisualElement
    {
        public DesignerChartDataSeries()
        {
            this.AutoFitToPlotArea = false;
            this.AxisXType = AxisTypes.Primary;
            this.AxisYType = AxisTypes.Primary;
            this.SeriesBorder = new DesignerBorder() { BorderThickness = new Thickness(0) };
            this.BorderStyle = BorderStyles.Solid;
            this.Color = new SolidColorBrush(System.Windows.Media.Color.FromRgb(74, 134, 232));
            this.DataPoints = new DataPointCollection();
            this.HighLightColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            this.IncludeDataPointsInLegend = false;
            this.IncludePercentageInLegend = false;
            this.IncludeYValueInLegend = false;
            this.LabelAngle = double.NaN;
            this.LabelBackground = null;
            this.LabelEnabled = false;
            this.LabelFont = new DesignerFont() { FontFamily = new FontFamily("Verdana"), FontSize = 10.2 };
            this.LabelLineColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(128, 128, 128));
            this.LabelLineEnabled = false;
            this.LabelLineStyle = LineStyles.Solid;
            this.LabelLineThickness = 0;
            this.LabelStyle = LabelStyles.OutSide;
            this.LabelText = "#YValue";
            this.SeriesName = "Series";
            this.LegendText = "";
            this.LightingEnabled = true;
            this.LightWeight = false;
            this.LineCap = PenLineCap.Round;
            this.LineFill = null;
            this.LineStyle = LineStyles.Dashed;
            this.LineTension = 0.5;
            this.LineThickness = 2;
            this.MarkerBorderColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(23, 145, 50));
            this.MarkerBorderThickness = new Thickness(1);
            this.MarkerColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(245, 226, 164));
            this.MarkerEnabled = false;
            this.MarkerScale = 1;
            this.MarkerSize = 4;
            this.MarkerType = MarkerTypes.Circle;
            this.RadiusX = new CornerRadius(0);
            this.RadiusY = new CornerRadius(0);
            this.RenderAs = Visifire.Charts.RenderAs.Line;
            this.ShadowEnabled = true;
            this.ShowInLegend = true;
            this.ZIndex = 0;
            this.XValueType = ChartValueTypes.Auto;
            this.LegendMarkerColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255,0,0));
        }

        #region 颜色

        [Browsable(false)]
        public Brush _color { set; get; }

        [Category("表现")]
        [DisplayName("呈现颜色")]
        [Description("颜色")]
        public Brush Color
        {
            set
            {
                this._color = value;
                OnPropertyChanged("Color");
            }
            get { return this._color; }
        }

        #region Radius

         [Browsable(false)]
        public System.Windows.CornerRadius _radiusX { set; get; }

        [Category("表现")]
        [DisplayName("RadiusX")]
        [Description("RadiusX")]
        public System.Windows.CornerRadius RadiusX
        {
            set { this._radiusX = value; OnPropertyChanged("RadiusX"); }
            get { return this._radiusX; }
        }

         [Browsable(false)]
        public System.Windows.CornerRadius _radiusY { set; get; }

        [Category("表现")]
        [DisplayName("RadiusY")]
        [Description("RadiusY")]
        public System.Windows.CornerRadius RadiusY
        {
            set { this._radiusY = value; OnPropertyChanged("RadiusY"); }
            get { return this._radiusY; }
        }




        #endregion

        #endregion

        #region 边框

        [Browsable(false)]
        public DesignerBorder _seriesBorder
        {
            get;
            set;
        }
        [Category("线边框")]
        [DisplayName("线边框")]
        [Description("设置线边框")]

        public DesignerBorder SeriesBorder
        {
            get { return this._seriesBorder; }
            set
            {
                this._seriesBorder = value;
                OnPropertyChanged("SeriesBorder");
            }
        }

         [Browsable(false)]
        public Visifire.Commons.BorderStyles _borderStyle
        {
            get;
            set;
        }

        [Category("线边框")]
        [DisplayName("线边框线条类型")]
        [Description("线边框线条类型")]
        public Visifire.Commons.BorderStyles BorderStyle
        {
            get { return this._borderStyle; }
            set
            {
                this._borderStyle = Visifire.Commons.BorderStyles.Solid;
            }
        }



        #endregion

        #region Label
         [Browsable(false)]
        public DesignerFont _labelFont { get; set; }
        [Category("标签")]
        [DisplayName("标签字体")]
        [Description("标签字体")]
        public DesignerFont LabelFont
        {
            get { return this._labelFont; }
            set
            {
                this._labelFont = value;
                OnPropertyChanged("LabelFont");
            }
        }
         [Browsable(false)]
        public double _labelAngle { get; set; }
        [Category("标签")]
        [DisplayName("标签角度")]
        [Description("标签角度")]
        public double LabelAngle
        {
            get { return this._labelAngle; }
            set
            {
                this._labelAngle = value;
                OnPropertyChanged("LabelAngle");
            }
        }

         [Browsable(false)]
        public Brush _labelBackground { get; set; }
        [Category("标签")]
        [DisplayName("标签背景")]
        [Description("标签背景")]
        public Brush LabelBackground
        {
            get { return this._labelBackground; }
            set
            {
                this._labelBackground = value;
                OnPropertyChanged("LabelBackground");
            }
        }
         [Browsable(false)]
        public string _labelText { get; set; }
        [Category("标签")]
        [DisplayName("标签文本")]
        [Description("标签文本")]
        public string LabelText
        {
            get { return this._labelText; }
            set
            {
                this._labelText = value;
                OnPropertyChanged("LabelText");
            }
        }
         [Browsable(false)]
        public LabelStyles _labelStyle { get; set; }
        [Category("标签")]
        [DisplayName("标签类型")]
        [Description("标签类型")]
        public LabelStyles LabelStyle
        {
            get { return this._labelStyle; }
            set
            {
                this._labelStyle = value;
                OnPropertyChanged("LabelStyle");
            }
        }
         [Browsable(false)]
        public bool _labelEnabled { get; set; }
        [Category("标签")]
        [DisplayName("呈现标签")]
        [Description("呈现标签")]
        public bool LabelEnabled
        {
            get { return this._labelEnabled; }
            set { this._labelEnabled = value; OnPropertyChanged("LabelEnabled"); }
        }



        #region LabelLine
         [Browsable(false)]
        public Brush _labelLineColor { get; set; }
        [Category("标签线")]
        [DisplayName("标签线颜色")]
        [Description("标签线颜色")]
        public Brush LabelLineColor
        {
            get { return this._labelLineColor; }
            set
            {
                this._labelLineColor = value;
                OnPropertyChanged("LabelLineColor");
            }
        }
         [Browsable(false)]
        public LineStyles _labelLineStyle { get; set; }
        [Category("标签线")]
        [DisplayName("标签线类型")]
        [Description("标签线类型")]
        public LineStyles LabelLineStyle
        {
            get { return this._labelLineStyle; }
            set
            {
                this._labelLineStyle = value;
                OnPropertyChanged("OnPropertyChanged");
            }
        }
         [Browsable(false)]
        public double _labelLineThickness { get; set; }
        [Category("标签线")]
        [DisplayName("标签线粗细")]
        [Description("标签线粗细")]
        public double LabelLineThickness
        {
            get { return this._labelLineThickness; }
            set
            {
                this._labelLineThickness = value;
                OnPropertyChanged("LabelLineThickness");
            }
        }
         [Browsable(false)]
        public bool _labelLineEnabled { get; set; }
        [Category("标签线")]
        [DisplayName("呈现标签线")]
        [Description("呈现标签线")]
        public bool LabelLineEnabled
        {
            get { return this._labelLineEnabled; }
            set { this._labelLineEnabled = value; OnPropertyChanged("LabelLineEnabled"); }
        }



        #endregion


        #endregion

        #region Legend
         [Browsable(false)]
        public string _seriesName { set; get; }
        [Category("图例")]
        [DisplayName("数据项名称")]
        [Description("数据项名称")]
        public string SeriesName
        {
            set { this._seriesName = value; OnPropertyChanged("SeriesName"); }
            get { return this._seriesName; }
        }
         [Browsable(false)]
        public bool _showInLegend { set; get; }
        [Category("图例")]
        [DisplayName("呈现图例")]
        [Description("在图例中呈现")]
        public bool ShowInLegend
        {
            set { this._showInLegend = value; OnPropertyChanged("ShowInLegend"); }
            get { return this._showInLegend; }
        }


         [Browsable(false)]
        public string _legendText { get; set; }
        [Category("图例")]
        [DisplayName("图例内文本")]
        [Description("图例内文本")]
        public string LegendText
        {
            get { return this._legendText; }
            set { this._legendText = value; OnPropertyChanged("LegendText"); }
        }

         [Browsable(false)]
        public Brush _legendMarkerColor { get; set; }
        [Category("图例")]
        [DisplayName("图例标记颜色")]
        [Description("图例标记颜色")]
        public Brush LegendMarkerColor
        {
            get { return this._legendMarkerColor; }
            set { this._legendMarkerColor = value; OnPropertyChanged("LegendMarkerColor"); }
        }
         [Browsable(false)]
        public MarkerTypes _legendMarkerType { get; set; }
        [Category("图例")]
        [DisplayName("图例标记类型")]
        [Description("图例标记类型")]
        public MarkerTypes LegendMarkerType
        {
            get { return this._legendMarkerType; }
            set { this._legendMarkerType = value; OnPropertyChanged("LegendMarkerType"); }
        }

        #endregion

        #region Light

         [Browsable(false)]
        public Brush _highLightColor { get; set; }
        [Category("高亮")]
        [DisplayName("高亮颜色")]
        [Description("高亮颜色")]
        public Brush HighLightColor
        {
            get { return this._highLightColor; }
            set
            {
                this._highLightColor = value;
                OnPropertyChanged("HighLightColor");
            }
        }

         [Browsable(false)]
        public bool _lightingEnabled { set; get; }
        [Category("高亮")]
        [DisplayName("呈现高亮")]
        [Description("呈现高亮")]
        public bool LightingEnabled
        {
            set { this._lightingEnabled = value; OnPropertyChanged("LightingEnabled"); }
            get { return this._lightingEnabled; }
        }

         [Browsable(false)]
        public bool _lightWeight { get; set; }

        [Category("高亮")]
        [DisplayName("LightWeight")]
        [Description("LightWeight")]
        public bool LightWeight
        {
            get { return this._lightWeight; }
            set { this._lightWeight = value; OnPropertyChanged("LightWeight"); }
        }


        #endregion

        #region Line

         [Browsable(false)]
        public PenLineCap _lineCap { set; get; }
        [Category("线条")]
        [DisplayName("LineCap")]
        [Description("LineCap")]
        public PenLineCap LineCap
        {
            set { this._lineCap = value; OnPropertyChanged("LineCap"); }
            get { return this._lineCap; }
        }

         [Browsable(false)]
        public Brush _lineFill { set; get; }
        [Category("线条")]
        [DisplayName("LineFill")]
        [Description("LineFill")]
        public Brush LineFill
        {
            set { this._lineFill = value; OnPropertyChanged("LineFill"); }
            get { return this._lineFill; }
        }

         [Browsable(false)]
        public LineStyles _lineStyle { set; get; }
        [Category("线条")]
        [DisplayName("LineStyle")]
        [Description("LineStyle")]
        public LineStyles LineStyle
        {
            set { this._lineStyle = value; OnPropertyChanged("LineStyle"); }
            get { return this._lineStyle; }
        }

         [Browsable(false)]
        public double _lineTension { set; get; }
        [Category("线条")]
        [DisplayName("LineTension")]
        [Description("LineTension")]
        public double LineTension
        {
            set { this._lineTension = value; OnPropertyChanged("LineTension"); }
            get { return this._lineTension; }
        }


         [Browsable(false)]
        public double _lineThickness { set; get; }
        [Category("线条")]
        [DisplayName("LineThickness")]
        [Description("LineThickness")]
        public double LineThickness
        {
            set { this._lineThickness = value; OnPropertyChanged("LineThickness"); }
            get { return this._lineThickness; }
        }

        #endregion

        #region Marker

         [Browsable(false)]
        public System.Windows.Media.Brush _markerBorderColor { set; get; }
        [Category("标记")]
        [DisplayName("标记边框颜色")]
        [Description("标记边框颜色")]
        public System.Windows.Media.Brush MarkerBorderColor
        {
            set { this._markerBorderColor = value; OnPropertyChanged("MarkerBorderColor"); }
            get { return this._markerBorderColor; }
        }

         [Browsable(false)]
        public Thickness _markerBorderThickness { set; get; }
        [Category("标记")]
        [DisplayName("标记边框粗细")]
        [Description("标记边框粗细")]
        public Thickness MarkerBorderThickness
        {
            set { this._markerBorderThickness = value; OnPropertyChanged("MarkerBorderThickness"); }
            get { return this._markerBorderThickness; }
        }

         [Browsable(false)]
        public System.Windows.Media.Brush _markerColor { set; get; }
        [Category("标记")]
        [DisplayName("标记颜色")]
        [Description("标记颜色")]
        public System.Windows.Media.Brush MarkerColor
        {
            set { this._markerColor = value; OnPropertyChanged("MarkerColor"); }
            get { return this._markerColor; }
        }

         [Browsable(false)]
        public bool _markerEnabled { set; get; }
        [Category("标记")]
        [DisplayName("呈现标记")]
        [Description("呈现标记")]
        public bool MarkerEnabled
        {
            set { this._markerEnabled = value; OnPropertyChanged("MarkerEnabled"); }
            get { return this._markerEnabled; }
        }

        [Browsable(false)]
        public double _markerScale { set; get; }
        [Category("标记")]
        [DisplayName("MarkerScale")]
        [Description("MarkerScale")]
        public double MarkerScale
        {
            set { this._markerScale = value; OnPropertyChanged("MarkerScale"); }
            get { return this._markerScale; }
        }

         [Browsable(false)]
        public double _markerSize { set; get; }

        [Category("标记")]
        [DisplayName("标记大小")]
        [Description("标记大小")]
        public double MarkerSize
        {
            set { this._markerSize = value; OnPropertyChanged("MarkerSize"); }
            get { return this._markerSize; }
        }

         [Browsable(false)]
        public MarkerTypes _markerType { set; get; }

        [Category("标记")]
        [DisplayName("标记类型")]
        [Description("标记类型")]
        public MarkerTypes MarkerType
        {
            set { this._markerType = value; OnPropertyChanged("MarkerType"); }
            get { return this._markerType; }
        }



        #endregion

    

        #region 表现形式
         [Browsable(false)]
        public RenderAs _renderAs { set; get; }

        [Category("表现")]
        [DisplayName("图表类型")]
        [Description("图表类型")]
        public RenderAs RenderAs
        {
            set { this._renderAs = value; OnPropertyChanged("RenderAs"); }
            get { return this._renderAs; }
        }

        #region 展现层次
         [Browsable(false)]
        public int _zIndex { set; get; }

        [Category("表现")]
        [DisplayName("层")]
        [Description("层")]
        public int ZIndex
        {
            set { this._zIndex = value; OnPropertyChanged("ZIndex"); }
            get { return this._zIndex; }
        }

        #endregion

        #endregion

        #region 阴影
         [Browsable(false)]
        public bool _shadowEnabled { set; get; }

        [Category("表现")]
        [DisplayName("呈现阴影")]
        [Description("呈现阴影")]
        public bool ShadowEnabled
        {
            set { this._shadowEnabled = value; OnPropertyChanged("ShadowEnabled"); }
            get { return this._shadowEnabled; }
        }



        #endregion

        #region 数据

         [Browsable(false)]
        public DesignerDataSource _dataSource { get; set; }


        [Category("数据")]
        [DisplayName("数据源")]
        [Description("数据源")]
        public DesignerDataSource DataSource
        {
            get { return this._dataSource; }
            set { this._dataSource = value; OnPropertyChanged("DataSource"); }
        }

         [Browsable(false)]
        public DataPointCollection _dataPoints { set; get; }
        [Category("数据")]
        [DisplayName("数据点集合")]
        [Description("数据点集合")]
        public DataPointCollection DataPoints
        {
            set
            {
                this._dataPoints = value;
                OnPropertyChanged("DataPoints");
            }
            get
            {
                return this._dataPoints;
            }
        }

        #region 数据种类
         [Browsable(false)]
        public ChartValueTypes _xValueType { set; get; }

        [Category("数据")]
        [DisplayName("XValueType")]
        [Description("XValueType")]
        public ChartValueTypes XValueType
        {
            set
            {
                this._xValueType = value;
                OnPropertyChanged("XValueType");
            }
            get
            {
                return this._xValueType;
            }
        }



        #endregion



        #endregion

        #region 选项

        #region 基础选项

        #region 依赖轴

        [Browsable(false)]
        public AxisTypes _axisXType
        {
            get;
            set;
        }
        [Category("轴")]
        [DisplayName("依赖X轴")]
        [Description("依赖X轴")]
        public AxisTypes AxisXType
        {
            get { return this._axisXType; }
            set
            {
                this._axisXType = value;
                OnPropertyChanged("AxisXType");
            }
        }

        [Browsable(false)]
        public AxisTypes _axisYType
        {
            get;
            set;
        }
        [Category("轴")]
        [DisplayName("依赖Y轴")]
        [Description("依赖Y轴")]
        public AxisTypes AxisYType
        {
            get { return this._axisYType; }
            set
            {
                this._axisYType = value;
                OnPropertyChanged("AxisYType");
            }
        }

        #endregion


        #endregion

        #region 拓展选项

        #region 自动填充区域
         [Browsable(false)]
        public bool _autoFitToPlotArea
        {
            set;
            get;
        }
        [Category("其他")]
        [DisplayName("自动填充到区域")]
        [Description("自动填充到区域")]
        public bool AutoFitToPlotArea
        {
            get { return this._autoFitToPlotArea; }
            set
            {
                this._autoFitToPlotArea = value;
                OnPropertyChanged("AutoFitToPlotArea");
            }
        }

        #endregion

        #region Legend内容
         [Browsable(false)]
        /// <summary>
        /// 包含数据点
        /// </summary>
        public bool _includeDataPointsInLegend
        {
            set;
            get;
        }
        [Category("图例")]
        [DisplayName("图例包含数据点")]
        [Description("图例包含数据点")]

        public bool IncludeDataPointsInLegend
        {
            get { return this._includeDataPointsInLegend; }
            set
            {
                this._includeDataPointsInLegend = value;
                OnPropertyChanged("IncludeDataPointsInLegend");
            }
        }
         [Browsable(false)]
        /// <summary>
        /// 包含百分比
        /// </summary>
        public bool _includePercentageInLegend
        {
            set;
            get;
        }
        [Category("图例")]
        [DisplayName("图例包含百分比")]
        [Description("图例包含百分比")]
        public bool IncludePercentageInLegend
        {
            get { return this._includePercentageInLegend; }
            set
            {
                this._includePercentageInLegend = value;
                OnPropertyChanged("IncludePercentageInLegend");
            }
        }
         [Browsable(false)]
        /// <summary>
        /// 包含值
        /// </summary>
        public bool _includeYValueInLegend
        {
            set;
            get;
        }
        [Category("图例")]
        [DisplayName("图例包含值")]
        [Description("图例包含值")]
        public bool IncludeYValueInLegend
        {
            get { return this._includeYValueInLegend; }
            set
            {
                this._includeYValueInLegend = value;
                OnPropertyChanged("IncludeYValueInLegend");
            }
        }



        #endregion

        #endregion






        #endregion

    }

}
