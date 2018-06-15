using Board.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Board.BaseControl
{
    public class DesignerItem : ContentControl,IDesigner
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
        
        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;
            //当左键被按下
            //若连同Control按键一起按下并且没有按其他按键，则处于多选状态
            if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
            {
                //点击一次，修改一次选中状态
                this.IsSelected = !this.IsSelected;
            }
            else //仅仅点击了鼠标左键，则可能处于单选状态或者移动选中项状态 
            {
                //若当前项未被选中，则取消其他所有项选中状态，只选中当前项
                if (this.IsSelected == false) 
                {
                    designer.DeselectAll();
                    this.IsSelected = true;
                }
                else //若当前项已经被选中，则进行其他动作，不进行选项动作
                {
                   
                }
            }

            if(this.IsSelected == true)
                designer.SelectItem = this.Content;

            e.Handled = false;
        }

        /// <summary>
        /// 鼠标左键抬起
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonUp(e);
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

        object IDesigner.GetDesignerModel()
        {
            return (this.Content as IDesigner).GetDesignerModel();
        }
    }
}
