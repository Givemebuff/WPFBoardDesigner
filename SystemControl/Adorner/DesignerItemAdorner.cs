using BoardDesigner.BaseChrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;




namespace BoardDesigner.BaseAdorner
{
    public class DesignerItemAdorner : Adorner
    {
        private VisualCollection visuals;
        private ResizeableChrome _sizeChrome;
        private RotateableChrome _rotateChrome;
        private TransformOriginPointChrome _originChrome;
        private List<Control> Chromes = new List<Control>();
        private LineChrome _lineChrome;

        protected override int VisualChildrenCount
        {
            get
            {
                return this.visuals.Count;
            }
        }

        public DesignerItemAdorner(ContentControl designerItem)
            : base(designerItem)
        {            
            SnapsToDevicePixels = true;
            this.visuals = new VisualCollection(this);
            if (designerItem.Content is Line) 
            {
                this._lineChrome = new LineChrome();
                _lineChrome.DataContext = designerItem;
            }
            else 
            {
                designerItem.RenderTransform = new RotateTransform(0);
                this._sizeChrome = new ResizeableChrome();
                this._sizeChrome.DataContext = designerItem;
                this._rotateChrome = new RotateableChrome();
                this._rotateChrome.DataContext = designerItem;
               // this._originChrome = new TransformOriginPointChrome();
                //this._originChrome.DataContext = designerItem;
                //this.visuals.Add(this._sizeChrome);
                //this.visuals.Add(this._rotateChrome);
            }
            InitChromeList(); 
            AddToChromeList();
        }

        /// <summary>
        /// 排列每个元素
        /// </summary>
        /// <param name="arrangeBounds"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            foreach (Control c in Chromes) 
            {
                if (c == null)
                    continue;
                else 
                {
                    c.Arrange(new Rect(arrangeBounds));
                }
            }
          
            return arrangeBounds;
        }

        private void InitChromeList() 
        {
            Chromes.Add(this._sizeChrome);
            Chromes.Add(this._rotateChrome);
            Chromes.Add(this._lineChrome);
          //  Chromes.Add(this._originChrome);
        }

        private void AddToChromeList() 
        {
            foreach (Control c in Chromes)
            {
                if (c == null)
                    continue;
                else
                {
                    this.visuals.Add(c);
                }
            }
        }

        /// <summary>
        /// 根据索引获取可视化元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            return this.visuals[index];

            
        }
    }
}
