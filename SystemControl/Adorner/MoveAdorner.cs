using BoardDesigner.BaseChrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace BoardDesigner.BaseAdorner
{
    public class MoveAdorner: Adorner
    {
        private VisualCollection _visuals;

        private ResizeableChrome _chrome;
        protected override int VisualChildrenCount
        {
            get
            {
                return this._visuals.Count;
            }
        }

        public MoveAdorner(ContentControl designerItem)
            : base(designerItem)
        {
            SnapsToDevicePixels = true;

            this._chrome = new ResizeableChrome();
            this._chrome.DataContext = designerItem;
            this._visuals = new VisualCollection(this);
            this._visuals.Add(this._chrome);
        }

        /// <summary>
        /// 排列每个元素
        /// </summary>
        /// <param name="arrangeBounds"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this._chrome.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        /// <summary>
        /// 根据索引获取可视化元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            return this._visuals[index];
        }
    }
}
