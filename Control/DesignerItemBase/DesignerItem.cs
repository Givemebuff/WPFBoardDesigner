using BoardDesigner.BaseThumb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BoardDesigner.Base
{
    public class DesignerItem : ContentControl
    {

        public double Left 
        {
            get 
            {
                return DesignerCanvas.GetLeft(this);
            }
            set
            {
                DesignerCanvas.SetLeft(this,value);                
            }
        }
        public double Top
        {
            get
            {
                return DesignerCanvas.GetTop(this);
            }
            set
            {
                DesignerCanvas.SetTop(this, value);
            }
        }


        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected", typeof(bool),
                                      typeof(DesignerItem),
                                      new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty MoveableThumbTemplateProperty =
            DependencyProperty.RegisterAttached("MoveableThumbTemplate", typeof(ControlTemplate), typeof(DesignerItem));

        public static ControlTemplate GetMoveableThumbTemplate(UIElement element)
        {
            return (ControlTemplate)element.GetValue(MoveableThumbTemplateProperty);
        }

        public static void SetMoveableThumbTemplateProperty(UIElement element, ControlTemplate value)
        {
            element.SetValue(MoveableThumbTemplateProperty, value);
            
        }

        static DesignerItem()
        {            
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignerItem), new FrameworkPropertyMetadata(typeof(DesignerItem)));
            
        }

        public DesignerItem()
        {
            this.Loaded += new RoutedEventHandler(this.DesignerItem_Loaded);
            
        }

        public DesignerItem(object content) 
        {
            this.Content = content;
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;

            if (designer != null)
            {
               
                //选择多个时
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                {
                    this.IsSelected = !this.IsSelected;
                }
                else//单个选择时
                {
                    DesignerCanvas dc = this.Parent as DesignerCanvas;
                    designer.DeselectAll();
                    this.IsSelected = true;
                    dc.SelectItem = this;
                   
                }
            }

            e.Handled = false;
        }

        private void DesignerItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Template != null)
            {
                ContentPresenter contentPresenter =
                    this.Template.FindName("CC", this) as ContentPresenter;
                contentPresenter.IsHitTestVisible = false;
                MoveableThumb thumb =
                    this.Template.FindName("MoveThumb", this) as MoveableThumb;

                if (contentPresenter != null && thumb != null)
                {
                    UIElement contentVisual =
                        VisualTreeHelper.GetChild(contentPresenter, 0) as UIElement;

                    if (contentVisual != null)
                    {
                        ControlTemplate template =
                            DesignerItem.GetMoveableThumbTemplate(contentVisual) as ControlTemplate;

                        if (template != null)
                        {
                            thumb.Template = template;
                        }
                    }
                }
            }
        }
    }
}
