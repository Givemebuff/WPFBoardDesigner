using Board.DesignerModel;
using System;
using System.Globalization;
using System.Windows.Data;
using Visifire.Charts;


namespace Board.Converter
{
    public class DesignerDataPointCollectionToVisifireDataPointCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DesignerDataPointCollection ddpc = value as DesignerDataPointCollection;
            DataPointCollection dc = new DataPointCollection();
            foreach (DesignerDataPoint dp in ddpc) 
            {
                DataPoint newDp = new DataPoint();
                newDp.Uid = dp.ID.ToString();
                newDp.Name = dp.Name;
                newDp.SetBinding(DataPoint.AxisXLabelProperty, new Binding("AxisXLabel") { Source = dp });
                newDp.SetBinding(DataPoint.BorderColorProperty, new Binding("LabelBorder.BorderBrush") { Source = dp });
                newDp.SetBinding(DataPoint.BorderStyleProperty, new Binding("BorderStyle") { Source = dp });
                newDp.SetBinding(DataPoint.BorderThicknessProperty, new Binding("LabelBorder.BorderThickness") { Source = dp });
                newDp.SetBinding(DataPoint.ColorProperty, new Binding("Color") { Source = dp });
                newDp.SetBinding(DataPoint.EnabledProperty, new Binding("Enabled") { Source = dp });
                newDp.SetBinding(DataPoint.LabelAngleProperty, new Binding("LabelAngle") { Source = dp });
                newDp.SetBinding(DataPoint.LabelBackgroundProperty, new Binding("LabelBackground") { Source = dp });
                newDp.SetBinding(DataPoint.LabelEnabledProperty, new Binding("LabelEnabled") { Source = dp });
                newDp.SetBinding(DataPoint.LabelFontColorProperty, new Binding("LabelFont.FontColor") { Source = dp });
                newDp.SetBinding(DataPoint.LabelFontFamilyProperty, new Binding("LabelFont.FontFamily") { Source = dp });
                newDp.SetBinding(DataPoint.LabelFontSizeProperty, new Binding("LabelFont.FontSize") { Source = dp });
                newDp.SetBinding(DataPoint.LabelFontStyleProperty, new Binding("LabelFont.FontStyle") { Source = dp });
                newDp.SetBinding(DataPoint.LabelFontWeightProperty, new Binding("LabelFont.FontWeight") { Source = dp });
                newDp.SetBinding(DataPoint.LabelLineColorProperty, new Binding("LabelLineColor") { Source = dp });
                newDp.SetBinding(DataPoint.LabelLineEnabledProperty, new Binding("LabelLineEnabled") { Source = dp });
                newDp.SetBinding(DataPoint.LabelLineStyleProperty, new Binding("LabelLineStyle") { Source = dp });
                newDp.SetBinding(DataPoint.LabelLineThicknessProperty, new Binding("LabelLineThickness") { Source = dp });
                newDp.SetBinding(DataPoint.LabelStyleProperty, new Binding("LabelStyle") { Source = dp });
                newDp.SetBinding(DataPoint.LabelTextProperty, new Binding("LabelText") { Source = dp });
                newDp.SetBinding(DataPoint.LegendMarkerColorProperty, new Binding("LegendMarkerColor") { Source = dp });
                newDp.SetBinding(DataPoint.LegendMarkerTypeProperty, new Binding("LegendMarkerType") { Source = dp });
                newDp.SetBinding(DataPoint.LegendTextProperty, new Binding("LegendText") { Source = dp });
                newDp.SetBinding(DataPoint.LightingEnabledProperty, new Binding("LightingEnabled") { Source = dp });
                newDp.SetBinding(DataPoint.MarkerBorderColorProperty, new Binding("MarkerBorderColor") { Source = dp });
                newDp.SetBinding(DataPoint.MarkerBorderThicknessProperty, new Binding("MarkerBorderThickness") { Source = dp });
                newDp.SetBinding(DataPoint.MarkerColorProperty, new Binding("MarkerColor") { Source = dp });
                newDp.SetBinding(DataPoint.MarkerEnabledProperty, new Binding("MarkerEnabled") { Source = dp });
                newDp.SetBinding(DataPoint.MarkerScaleProperty, new Binding("MarkerScale") { Source = dp });
                newDp.SetBinding(DataPoint.MarkerSizeProperty, new Binding("MarkerSize") { Source = dp });
                newDp.SetBinding(DataPoint.MarkerTypeProperty, new Binding("MarkerType") { Source = dp });
                newDp.SetBinding(DataPoint.OpacityProperty, new Binding("DataPointOpacity") { Source = dp });
                newDp.SetBinding(DataPoint.RadiusXProperty, new Binding("RadiusX") { Source = dp });
                newDp.SetBinding(DataPoint.RadiusYProperty, new Binding("RadiusY") { Source = dp });
                newDp.SetBinding(DataPoint.ShadowEnabledProperty, new Binding("ShadowEnabled") { Source = dp });
                newDp.SetBinding(DataPoint.ShowInLegendProperty, new Binding("ShowInLegend") { Source = dp });
                newDp.SetBinding(DataPoint.StickColorProperty, new Binding("StickColor") { Source = dp });
                newDp.SetBinding(DataPoint.XValueProperty, new Binding("XValue") { Source = dp });
                newDp.SetBinding(DataPoint.YValueProperty, new Binding("YValue") { Source = dp });
                newDp.SetBinding(DataPoint.YValuesProperty, new Binding("YValues") { Source = dp });
                newDp.SetBinding(DataPoint.ZValueProperty, new Binding("ZValue") { Source = dp });

                dc.Add((IDataPoint)newDp);
            }
            return dc;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
 