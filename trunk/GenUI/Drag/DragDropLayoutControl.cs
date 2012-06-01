using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraLayout.Dragging;
using DevExpress.XtraLayout.Customization;
using DevExpress.XtraLayout;
using Business;

namespace GenUI
{
    public interface IDragManager
    {
        LayoutControlItem DragItem { get; set; }
        void SetDragCursor(DragDropEffects effect);
    }
    public partial class DragDropLayoutControl : UserControl
    {
        LayoutControlItem newDragItem = null;
        //dragHelper
        DragFrameWindow window;
        public LayoutItemDragController dragController = null;
        public DragDropLayoutControl()
        {
            InitializeComponent();
        }
        public IDragManager DragManager
        {
            get
            {
                if (Parent != null & Parent.Parent != null)
                    return Parent.Parent as IDragManager;
                else
                    return null;

            }
        }
        private void layoutControl2_MouseDown(object sender, MouseEventArgs e)
        {
            newDragItem = layoutControl2.CalcHitInfo(new Point(e.X, e.Y)).Item as LayoutControlItem;
        }
        private void layoutControl2_MouseMove(object sender, MouseEventArgs e)
        {
            if (newDragItem == null || e.Button != MouseButtons.Left) return;
            DragManager.DragItem = newDragItem;
            layoutControl2.DoDragDrop(DragManager.DragItem, DragDropEffects.Copy);
            newDragItem = null;
        }
        private void layoutControl2_DragDrop(object sender, DragEventArgs e)
        {
            if (dragController != null && DragManager.DragItem != null)
            {
                dragController = new LayoutItemDragController(DragManager.DragItem, dragController);
                if (DragManager.DragItem.Owner == null || DragManager.DragItem.Parent == null)
                    dragController.DragWildItem();
                else
                    dragController.Drag();
            }
            HideDragHelper();
            Parent.Cursor = Cursors.Default;
            Prop curprop = (DragManager.DragItem.Tag as Prop);
            string id = DragManager.DragItem.Control.Name;
            PropListCache.Proplist.Remove(PropListCache.Proplist.Find(p => p.ID.ToString() == id));


            DragManager.DragItem = null;

        }
        private void layoutControl2_DragEnter(object sender, DragEventArgs e)
        {
            dragController = null;
            ShowDragHelper();
        }
        private void layoutControl2_DragLeave(object sender, EventArgs e)
        {
            HideDragHelper();
        }
        private void layoutControl2_DragOver(object sender, DragEventArgs e)
        {
            UpdateDragHelper(new Point(e.X, e.Y));
            e.Effect = DragDropEffects.Copy;
            DragManager.SetDragCursor(e.Effect);
        }
        private void layoutControl2_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
        }
        protected DragFrameWindow DragFrameWindow
        {
            get
            {
                if (window == null) window = new DragFrameWindow(layoutControl2);
                return window;
            }
        }
        protected void ShowDragHelper()
        {
            if (DragManager.DragItem == null) return;
            DragFrameWindow.Visible = true;
        }
        protected void HideDragHelper()
        {
            DragFrameWindow.Reset();
            DragFrameWindow.Visible = false;
        }
        protected void UpdateDragHelper(Point p)
        {
            if (DragManager.DragItem == null) return;
            p = layoutControl2.PointToClient(p);
            dragController = new LayoutItemDragController(null, layoutControl2.Root, new Point(p.X, p.Y));
            DragFrameWindow.DragController = dragController;
        }
    }
}
