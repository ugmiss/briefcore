using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Telerik.WinControls.UI;
using System.Drawing.Drawing2D;
using System.Threading;
using System.IO;
using Utility;
using System;
using System.Diagnostics;

namespace MyCutter
{
    public partial class CutterForm : RadForm
    {
        /// <summary>
        /// 当前操作类型。
        /// </summary>
        public EnumArrowType arow = EnumArrowType.None;
        /// <summary>
        /// 8个手柄。
        /// </summary>
        public RectangleF[] recArrow = new RectangleF[8];
        /// <summary>
        /// 选择区域。
        /// </summary>
        public RectangleF area = RectangleF.Empty;//
        /// <summary>
        /// 手柄矩形的边长。
        /// </summary>
        public readonly int side = 4;
        /// <summary>
        /// 是否调整大小。
        /// </summary>
        public bool IsSizing = false;
        /// <summary>
        /// 原始区域。
        /// </summary>
        public RectangleF LastArea;
        /// <summary>
        /// 操作原点。
        /// </summary>
        private PointF LastPoint;
        /// <summary>
        /// 当前图书。
        /// </summary>
        Book CurrentBook;
        /// <summary>
        /// 当前块。
        /// </summary>
        Block CurrentBlock;
        /// <summary>
        /// 当前页。
        /// </summary>
        Page CurrentPage;
        /// <summary>
        /// 页列表。
        /// </summary>
        List<Page> PageList = new List<Page>();
        /// <summary>
        /// 当前根节点。
        /// </summary>
        RadTreeNode BookNode;
        /// <summary>
        /// 构造方法。
        /// </summary>
        public CutterForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutterForm_Load(object sender, EventArgs e)
        {
            PicBoxMenu.Items[0].Click += new EventHandler(AddArea_Click);
            PicBoxMenu.Items[1].Click += new EventHandler(EditArea_Click);

            treeMenu.Items[0].Click += new EventHandler(AddPageClick);
        }
        /// <summary>
        /// 图区单击。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                PicBoxMenu.Show(this.pictureBox1.PointToScreen(e.Location));
                LastPoint = e.Location;
            }
        }
        /// <summary>
        /// 添加块。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddArea_Click(object sender, EventArgs e)
        {
            if (area.Height * area.Width == 0)
            {
                area = new Rectangle();
                area.Size = new Size(50, 50); ;
                area.Location = LastPoint;
            }
            pictureBox1.Refresh();
        }
        /// <summary>
        /// 编辑块。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EditArea_Click(object sender, EventArgs e)
        {
            PicBoxMenu.DropDown.ClosePopup(RadPopupCloseReason.Mouse);
            BlockForm bf = new BlockForm(CurrentBook.BookName);
            if (CurrentBlock == null)
                bf.Block = new Block();
            else
                bf.Block = CurrentBlock;
            bf.Block.X = (int)area.Location.X;
            bf.Block.Y = (int)area.Location.Y;
            bf.Block.Width = (int)area.Width;
            bf.Block.Height = (int)area.Height;
            bf.ShowDialog();
            if (bf.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                CurrentBlock = bf.Block;
                if (CurrentPage.Blocks == null)
                    CurrentPage.Blocks = new List<Block>();
                CurrentPage.Blocks.Add(CurrentBlock);
            }
            SaveBlock();
        }
        /// <summary>
        /// 鼠标按下。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Cursor != Cursors.Arrow)
                    IsSizing = true;
                this.LastPoint = new PointF(e.X, e.Y);
                LastArea = new RectangleF(area.Location, area.Size);
            }
        }
        /// <summary>
        /// 鼠标抬起。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            IsSizing = false;
        }
        /// <summary>
        /// 鼠标移动。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            radLabelElement1.Text = e.Location.ToString();
            // 更新光标。
            /*
             0 1 2
             3   4 
             5 6 7 
             */
            if (!IsSizing)
            {
                if (e.Y <= recArrow[1].Bottom && e.Y >= recArrow[1].Top && e.X >= recArrow[1].Left && e.X <= recArrow[1].Right)//上
                { Cursor = Cursors.SizeNS; arow = EnumArrowType.Up; }
                else if (e.Y <= recArrow[6].Bottom && e.Y >= recArrow[6].Top && e.X >= recArrow[6].Left && e.X <= recArrow[6].Right)//下
                { Cursor = Cursors.SizeNS; arow = EnumArrowType.Down; }
                else if (e.X >= recArrow[3].Left && e.X <= recArrow[3].Right && e.Y >= recArrow[3].Top && e.Y <= recArrow[3].Bottom)//左
                { Cursor = Cursors.SizeWE; arow = EnumArrowType.Left; }
                else if (e.X >= recArrow[4].Left && e.X <= recArrow[4].Right && e.Y >= recArrow[4].Top && e.Y <= recArrow[4].Bottom)//右
                { Cursor = Cursors.SizeWE; arow = EnumArrowType.Right; }
                else if (e.Y <= recArrow[0].Bottom && e.Y >= recArrow[0].Top && e.X >= recArrow[0].Left && e.X <= recArrow[0].Right)//左上
                { Cursor = Cursors.SizeNWSE; arow = EnumArrowType.LeftUp; }
                else if (e.Y <= recArrow[5].Bottom && e.Y >= recArrow[5].Top && e.X >= recArrow[5].Left && e.X <= recArrow[5].Right)//左下
                { Cursor = Cursors.SizeNESW; arow = EnumArrowType.LeftDown; }
                else if (e.Y <= recArrow[2].Bottom && e.Y >= recArrow[2].Top && e.X >= recArrow[2].Left && e.X <= recArrow[2].Right)//右上
                { Cursor = Cursors.SizeNESW; arow = EnumArrowType.RightUp; }
                else if (e.Y <= recArrow[7].Bottom && e.Y >= recArrow[7].Top && e.X >= recArrow[7].Left && e.X <= recArrow[7].Right)//右下
                { Cursor = Cursors.SizeNWSE; arow = EnumArrowType.RightDown; }
                else if (e.Y <= area.Bottom && e.Y >= area.Top && e.X >= area.Left && e.X <= area.Right)
                { Cursor = Cursors.SizeAll; arow = EnumArrowType.SizeAll; }
                else
                { Cursor = Cursors.Arrow; arow = EnumArrowType.None; }
            }
            //左键拖动。
            if (e.Button == MouseButtons.Left)
            {
                PointF t = new PointF(e.X, e.Y);
                PointF l = this.LastPoint;
                ReSize(e);
            }
        }
        /// <summary>
        /// 调整大小。
        /// </summary>
        /// <param name="e"></param>
        public void ReSize(MouseEventArgs e)
        {
            PointF t = new PointF(e.X, e.Y);// e.Location;
            PointF l = this.LastPoint;
            switch (arow)
            {
                case EnumArrowType.Up:
                    {
                        area.Height = LastArea.Height - (t.Y - l.Y);
                        area.Y = t.Y;
                        break;
                    }
                case EnumArrowType.Down:
                    {
                        area.Height = LastArea.Height + (t.Y - l.Y);
                        break;
                    }
                case EnumArrowType.Left:
                    {
                        area.Width = LastArea.Width - (t.X - l.X);
                        area.X = t.X;
                        break;
                    }
                case EnumArrowType.Right:
                    {
                        area.Width = LastArea.Width + (t.X - l.X);
                        break;
                    }
                case EnumArrowType.LeftUp:
                    {
                        area.Width = LastArea.Width - (t.X - l.X);
                        area.Height = LastArea.Height - (t.Y - l.Y);
                        area.Location = new PointF(t.X, t.Y);
                        break;
                    }
                case EnumArrowType.LeftDown:
                    {
                        area.Width = LastArea.Width - (t.X - this.LastPoint.X);
                        area.Height = LastArea.Height + (t.Y - this.LastPoint.Y);
                        break;
                    }
                case EnumArrowType.RightUp:
                    {
                        area.Width = LastArea.Width + (t.X - this.LastPoint.X);
                        area.Height = LastArea.Height - (t.Y - this.LastPoint.Y);
                        area.Y = t.Y;
                        break;
                    }
                case EnumArrowType.RightDown:
                    {
                        area.Width = LastArea.Width + (t.X - this.LastPoint.X);
                        area.Height = LastArea.Height + (t.Y - this.LastPoint.Y);
                        break;
                    }
                case EnumArrowType.SizeAll:
                    {
                        area.X = LastArea.X + (t.X - this.LastPoint.X);
                        area.Y = LastArea.Y + (t.Y - this.LastPoint.Y);
                        break;
                    }
            }
            if (area.Width < 0) area.Width = -area.Width;
            if (area.Height < 0) area.Height = -area.Height;
            pictureBox1.Refresh();
        }
        /// <summary>
        /// 绘制。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Thread.Sleep(6);
            GC.Collect();
            try
            {
                if (CurrentPage == null) return;
                Graphics g = e.Graphics;
                if (!CurrentPage.BackgroundImage.IsNullOrEmpty())
                    e.Graphics.DrawImage(Image.FromFile(Common.GetResouce(CurrentBook.BookName + "\\resources\\", CurrentPage.BackgroundImage)), 0, 0, CurrentBook.Width, CurrentBook.Height);
                else if (!CurrentBook.Background.IsNullOrEmpty())
                    e.Graphics.DrawImage(Image.FromFile(Common.GetResouce(CurrentBook.BookName + "\\resources\\", CurrentBook.Background)), 0, 0, CurrentBook.Width, CurrentBook.Height);


                if (CurrentPage.Blocks != null)
                    foreach (Block block in CurrentPage.Blocks)
                    {
                        if (block.BlockType == "Pic" && block.BlockResource.IsNullOrEmpty())
                        {
                            e.Graphics.DrawImage(Image.FromFile(Common.GetResouce(CurrentBook.BookName + "\\resources\\", block.BlockResource)), block.X, block.Y, block.Width, block.Height);
                        }
                    }


                if (area != RectangleF.Empty)
                {
                    recArrow[0] = new RectangleF(new PointF(area.X, area.Y), new Size(side, side));
                    recArrow[1] = new RectangleF(new PointF(area.X + area.Width / 2 - side / 2, area.Y), new Size(side, side));
                    recArrow[2] = new RectangleF(new PointF(area.X + area.Width - side, area.Y), new Size(side, side));
                    recArrow[3] = new RectangleF(new PointF(area.X, area.Y + area.Height / 2 - side / 2), new Size(side, side));
                    recArrow[4] = new RectangleF(new PointF(area.X + area.Width - side, area.Y + area.Height / 2 - side / 2), new Size(side, side));
                    recArrow[5] = new RectangleF(new PointF(area.X, area.Y + area.Height - side), new Size(side, side));
                    recArrow[6] = new RectangleF(new PointF(area.X + area.Width / 2 - side / 2, area.Y + area.Height - side), new Size(side, side));
                    recArrow[7] = new RectangleF(new PointF(area.X + area.Width - side, area.Y + area.Height - side), new Size(side, side));


                    foreach (RectangleF item in recArrow)
                        e.Graphics.DrawRectangle(Pens.Red, item.X, item.Y, item.Size.Width, item.Size.Height);
                    e.Graphics.SmoothingMode = SmoothingMode.HighQuality; //高质量
                    e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量 
                    e.Graphics.DrawRectangle(Pens.Red, area.X, area.Y, area.Width, area.Height);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex);
            }
        }
        /// <summary>
        /// 新建图书。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElement1_Click(object sender, EventArgs e)
        {
            if (CurrentBook != null) return;
            BookForm bf = new BookForm();
            bf.ShowDialog();
            if (bf.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                CurrentBook = bf.Book;
                BookNode = this.radTreeView1.Nodes.Add(bf.Book.BookName);
                BookNode.Image = CommonResource.配置;
                PageList.Clear();
                string path = AppDomain.CurrentDomain.BaseDirectory + CurrentBook.BookName;
                string path2 = AppDomain.CurrentDomain.BaseDirectory + bf.Book.BookName + "\\resources\\";
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
                //Directory.CreateDirectory(path);
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(path2);
                if (!bf.Book.Background.IsNullOrEmpty())
                {
                    bf.Book.Background = Common.AddResouce(bf.Book.BookName, bf.Book.Background, path2 + Common.GetShortFileName(bf.Book.Background));
                }
            }
        }
        /// <summary>
        /// 树菜单。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radTreeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeMenu.Show(this.radTreeView1.PointToScreen(e.Location));
            }
        }
        /// <summary>
        /// 添加页。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddPageClick(object sender, EventArgs e)
        {
            if (CurrentBook == null)
                return;
            treeMenu.DropDown.ClosePopup(RadPopupCloseReason.Mouse);
            PageForm pf = new PageForm(CurrentBook, PageList.Count);
            pf.ShowDialog();
            if (pf.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                CurrentPage = pf.Page;
                CurrentPage.Blocks = new List<Block>();
                PageList.Add(pf.Page);
                RadTreeNode node1 = BookNode.Nodes.Add(pf.Page.Name);
                node1.Image = CommonResource.复制;
                node1.Tag = pf.Page;
                radTreeView1.ExpandAll();
                radTreeView1.SelectedNode = node1;
                AddPage();
            }
        }
        /// <summary>
        /// 添加页。
        /// </summary>
        void AddPage()
        {
            this.pictureBox1.Width = CurrentBook.Width;
            this.pictureBox1.Height = CurrentBook.Height;
            this.pictureBox1.Visible = true;
        }
        /// <summary>
        /// 保存电子书。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radButtonElement7_Click(object sender, EventArgs e)
        {
            if (CurrentBook == null) return;
            string path = AppDomain.CurrentDomain.BaseDirectory + CurrentBook.BookName;
            List<string> li = new List<string>();
            foreach (Page page in PageList)
            {
                PageInfo info = new PageInfo();
                info.Index = page.Index;
                info.PageJsonFileName = page.Name + "." + Comb.NewGuid() + ".json";
                Newtonsoft.Json.JsonConvert.SerializeObject(page).WriteToFile(path + "\\" + info.PageJsonFileName);
                li.Add(info.PageJsonFileName);
            }
            CurrentBook.PageInfoList = li.ToArray();

            Newtonsoft.Json.JsonConvert.SerializeObject(CurrentBook).WriteToFile(path + "\\" + CurrentBook.BookName + ".book.json");
            //Zip.ZipFolder(path, AppDomain.CurrentDomain.BaseDirectory + CurrentBook.BookName + ".zip");
            MsgBox.Show("保存成功。");
            this.CurrentBook = null;
            this.CurrentPage = null;
            this.CurrentBlock = null;
            this.radTreeView1.Nodes.Clear();
            Process.Start(AppDomain.CurrentDomain.BaseDirectory);
        }
        void SaveBlock()
        {
            this.area = RectangleF.Empty;
            CurrentBlock = null;
            pictureBox1.Refresh();
        }
        private void radButtonElement5_Click(object sender, EventArgs e)
        {

        }

        private void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            CurrentPage = e.Node.Tag as Page;
            if (CurrentPage != null)
                pictureBox1.Refresh();
        }
    }
    public enum EnumArrowType
    {
        Up, Down, Left, Right, LeftUp, LeftDown, RightUp, RightDown, SizeAll, None
    }
}