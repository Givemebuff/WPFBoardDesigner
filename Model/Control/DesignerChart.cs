using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get { return this._chartAxesX; }
            set
            {
                this._chartAxesX = value;
                OnPropertyChanged("ChartAxesY");
            }
        }

        #endregion

        #region 数据

        #region 数据源
        #endregion

        #region 数据线



        #endregion

        #endregion

    }
    public class DesignerChartTitle : DesignerVisualElement
    {

        public DesignerChartTitle()
        {
            this.Text = "图表标题";
            this.TitleFont = new DesignerFont();
            this.TitleBorder = new DesignerBorder() { BorderThickness = new System.Windows.Thickness(1) };
        }

        [Browsable(false)]
        public DesignerFont _titleFont
        {
            get; set;
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
            get; set;
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
            get; set;
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
            get; set;
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
            get; set;
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
            get; set;
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
            get; set;
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
            get; set;
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

        #region 基础

        #region 颜色
        public Brush _color { set; get; }

        public Brush Color
        {
            set
            {
                this._color = value;
                OnPropertyChanged("Color");
            }
            get { return this._color; }
        }

        public Brush _highLightColor { get; set; }
        public Brush HighLightColor
        {
            get { return this._highLightColor; }
            set
            {
                this._highLightColor = value;
                OnPropertyChanged("HighLightColor");
            }
        }
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

        public Visifire.Commons.BorderStyles _borderStyle
        {
            get; set;
        }
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

        public DesignerFont _labelFont { get; set; }
        public DesignerFont LabelFont
        {
            get { return this._labelFont; }
            set
            {
                this._labelFont = value;
                OnPropertyChanged("LabelFont");
            }
        }

        public double _labelAngle { get; set; }
        public double LabelAngle
        {
            get { return this._labelAngle; }
            set
            {
                this._labelAngle = value;
                OnPropertyChanged("LabelAngle");
            }
        }


        public Brush _labelBackground { get; set; }
        public Brush LabelBackground
        {
            get { return this._labelBackground; }
            set
            {
                this._labelBackground = value;
                OnPropertyChanged("LabelBackground");
            }
        }

        public string _labelText { get; set; }
        public string LabelText
        {
            get { return this._labelText; }
            set
            {
                this._labelText = value;
                OnPropertyChanged("LabelText");
            }
        }

        public LabelStyles _labelStyle { get; set; }
        public LabelStyles LabelStyle
        {
            get { return this._labelStyle; }
            set
            {
                this._labelStyle = value;
                OnPropertyChanged("LabelStyle");
            }
        }

        public bool _labelEnabled { get; set; }
        public bool LabelEnabled
        {
            get { return this._labelEnabled; }
            set { this._labelEnabled = value; OnPropertyChanged("LabelEnabled"); }
        }



        #region LabelLine
        public Brush _labelLineColor { get; set; }
        public Brush LabelLineColor
        {
            get { return this._labelLineColor; }
            set
            {
                this._labelLineColor = value;
                OnPropertyChanged("LabelLineColor");
            }
        }

        public LineStyles _labelLineStyle { get; set; }
        public LineStyles LabelLineStyle
        {
            get { return this._labelLineStyle; }
            set
            {
                this._labelLineStyle = value;
                OnPropertyChanged("OnPropertyChanged");
            }
        }

        public double _labelLineThickness { get; set; }
        public double LabelLineThickness
        {
            get { return this._labelLineThickness; }
            set
            {
                this._labelLineThickness = value;
                OnPropertyChanged("LabelLineThickness");
            }
        }

        public bool _labelLineEnabled { get; set; }
        public bool LabelLineEnabled
        {
            get { return this._labelLineEnabled; }
            set { this._labelLineEnabled = value; OnPropertyChanged("LabelLineEnabled"); }
        }



        #endregion


        #endregion

        #region Legend 


        public string _legendText { get; set; }
        public string LegendText
        {
            get { return this._legendText; }
            set { this._legendText = value; OnPropertyChanged("LegendText"); }
        }

        public Brush _legendMarkerColor { get; set; }
        public Brush LegendMarkerColor
        {
            get { return this._legendMarkerColor; }
            set { this._legendMarkerColor = value; OnPropertyChanged("LegendMarkerColor"); }
        }

        public MarkerTypes _legendMarkerType { get; set; }
        public MarkerTypes LegendMarkerType
        {
            get { return this._legendMarkerType; }
            set { this._legendMarkerType = value; OnPropertyChanged("LegendMarkerType"); }
        }

        #endregion

        #endregion


        #region 数据
        public DesignerDataSource _dataSource { get; set; }
        public DesignerDataSource DataSource
        {
            get { return this._dataSource; }
            set { this._dataSource = value; OnPropertyChanged("DataSource"); }
        }

        public DataPointCollection _dataPoints { set; get; }
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

        #endregion


        #region 选项

        #region 基础选项

        #region 依赖轴

        [Browsable(false)]
        public AxisTypes _axisXType
        {
            get; set;
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
            get; set;
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

        public bool _autoFitToPlotArea
        {
            set;
            get;
        }

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

        /// <summary>
        /// 包含数据点
        /// </summary>
        public bool _includeDataPointsInLegend
        {
            set;
            get;
        }

        public bool IncludeDataPointsInLegend
        {
            get { return this._includeDataPointsInLegend; }
            set
            {
                this._includeDataPointsInLegend = value;
                OnPropertyChanged("IncludeDataPointsInLegend");
            }
        }
        /// <summary>
        /// 包含百分比
        /// </summary>
        public bool _includePercentageInLegend
        {
            set;
            get;
        }

        public bool IncludePercentageInLegend
        {
            get { return this._includePercentageInLegend; }
            set
            {
                this._includePercentageInLegend = value;
                OnPropertyChanged("IncludePercentageInLegend");
            }
        }

        /// <summary>
        /// 包含值
        /// </summary>
        public bool _includeYValueInLegend
        {
            set;
            get;
        }

        public bool IncludeYValueInLegend
        {
            get { return this._includeYValueInLegend; }
            set
            {
                this._includeYValueInLegend = value;
                OnPropertyChanged("IncludeYValueInLegend ");
            }
        }



        #endregion

        #endregion






        #endregion


    }

}
