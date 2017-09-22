using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace ZLIS.SkinBuilder
{
    public delegate void GroupExpandedDelegate(SkinListViewGroup group);

    public partial class SkinListView : Control
    {
        #region Members

        protected View view = View.Details;
        protected SkinListViewTheme theme = null;

        protected VScrollBar vscrollBar = new VScrollBar();
        protected HScrollBar hscrollBar = null;
        protected bool showGroups = true;

        protected SkinListViewGroupCollection groups = new SkinListViewGroupCollection();
        protected SkinListViewItemCollection items = new SkinListViewItemCollection();

        private List<SkinListViewGroup> groupsCache = new List<SkinListViewGroup>();

        private SkinListViewItemBase selectedItem = null;
        private SkinListViewItemBase highlightItem = null;

        private List<SkinListViewItemBase> selectedItemList = new List<SkinListViewItemBase>();

        public EventHandler<EventArgs> SelectedItemChangedEvent;
        public EventHandler<EventArgs> SelectedItemChangingEvent;

        #endregion

        #region Events

        public event GroupExpandedDelegate GroupExpandedEvent;
        #endregion

        #region Property

        public View ViewStyle
        {
            get { return this.view; }
            set
            {
                this.view = value;
                this.Invalidate();
            }
        }

        public SkinListViewGroupCollection Groups
        {
            get { return this.groups; }
        }

        public SkinListViewItemCollection Items
        {
            get { return this.items; }
        }

        public virtual SkinListViewTheme Theme
        {
            get { return this.theme; }
            set
            {
                this.theme = value;
                this.Invalidate();
            }
        }

        public virtual bool ShowGroups
        {
            get { return this.showGroups; }
            set
            {
                this.showGroups = value;
                this.Invalidate();
            }
        }

        public SkinListViewItemBase SelectedItem
        {
            get 
            {
                if (this.selectedItemList.Count == 0)
                    return null;

                return this.selectedItemList[0];
            }
            set
            {
                //if (this.selectedItem == value)
                //    return;

                //if (this.selectedItem != null)
                //    this.selectedItem.State = ControlState.Normal;

                //this.selectedItem = value;
                //if (value != null)
                //    value.State = ControlState.Checked;

                if (this.SelectedItemChangingEvent != null)
                    this.SelectedItemChangingEvent(this, EventArgs.Empty);

                this.selectedItemList.Clear();
                this.selectedItemList.Add(value);

                this.Invalidate();

                if (this.SelectedItemChangedEvent != null)
                    this.SelectedItemChangedEvent(this, EventArgs.Empty);
            }
        }

        public SkinListViewItemBase[] SelectedItems
        {
            get { return this.selectedItemList.ToArray(); }
        }

        public bool MultiSelect
        {
            get;
            set;
        }

        protected SkinListViewItemBase HighlightItem
        {
            get { return this.highlightItem; }
            set
            {
                if (this.highlightItem == value)
                    return;

                if (this.highlightItem != null)
                    this.highlightItem.State = ControlState.Normal;

                this.highlightItem = value;
                if (value != null)
                    value.State = ControlState.Highlight;

                this.Invalidate();
            }
        }

        public bool DoubleClickExpanded
        {
            get;
            set;
        }

        public bool ShowSelectAlways
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public SkinListView()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserMouse |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable, true);

            this.groups.SkinListView = this;
            this.items.SkinListView = this;
            this.theme = new SkinListViewTheme(this);

            this.DoubleClickExpanded = false;
            this.MultiSelect = false;

            this.InitScrollBar();
        }

        #endregion

        #region Item content changed callback

        internal void ItemExist(SkinListViewItem item)
        {
            foreach (SkinListViewItemBase temp in this.items)
            {
                if (temp == item)
                    throw new Exception("The item already exist!");
            }
        }

        internal void GroupExist(SkinListViewGroup group)
        {
            foreach (SkinListViewGroup temp in this.groupsCache)
            {
                if (temp == group)
                    throw new Exception("The group already exist!");
            }
        }

        internal void OnNewItemAdded(SkinListViewItem item)
        {
            bool exist = false;
            foreach (SkinListViewItemBase temp in this.items)
            {
                if (temp == item)
                {
                    exist = true;
                    break;
                }
            }

            if (!exist)
                this.items.Add(item);

            this.ResizeScrollBar();

            this.Invalidate();
        }

        internal void OnItemDeleted(SkinListViewItem item)
        {
            foreach (SkinListViewItemBase temp in this.items)
            {
                if (temp == item)
                {
                    this.items.Remove(item);
                    break;
                }
            }

            this.ResizeScrollBar();

            this.Invalidate();
        }

        internal void OnItemCollectionClear(SkinListViewItemCollection items)
        {
            foreach (SkinListViewItem item in items)
            {
                this.items.Remove(item);
            }

            this.ResizeScrollBar();

            this.Invalidate();
        }

        internal void OnNewGroupAdded(SkinListViewGroup group)
        {
            bool exist = false;
            foreach (SkinListViewItemBase temp in this.groupsCache)
            {
                if (temp == group)
                {
                    exist = true;
                    break;
                }
            }

            if (!exist)
                this.groupsCache.Add(group);

            this.ResizeScrollBar();

            this.Invalidate();
        }

        internal void OnGroupDeleted(SkinListViewGroup group)
        {
            foreach (SkinListViewItem i in group.Items)
            {
                this.items.Remove(i);
            }

            this.groupsCache.Remove(group);

            this.ResizeScrollBar();

            this.Invalidate();
        }

        internal void OnGroupCollectionClear(SkinListViewGroupCollection groups)
        {
            foreach (SkinListViewGroup group in groups)
            {
                this.OnGroupDeleted(group);
            }

            this.ResizeScrollBar();

            this.Invalidate();
        }

        internal void OnItemTextChanged(SkinListViewItemBase item)
        {
            this.Invalidate();
        }

        internal void OnItemImageChanged(SkinListViewItemBase item)
        {
            this.Invalidate();
        }

        internal void OnItemTextImageRelationChanged(SkinListViewItemBase item)
        {
            this.Invalidate();
        }

        internal void OnItemTextAlignmentChanged(SkinListViewItemBase item)
        {
            this.Invalidate();
        }

        internal void OnItemTextDirectionChanged(SkinListViewItemBase item)
        {
            this.Invalidate();
        }

        internal void OnItemTextFontChanged(SkinListViewItemBase item)
        {
            this.Invalidate();
        }

        internal void OnItemTextColorChanged(SkinListViewItemBase item)
        {
            this.Invalidate();
        }

        internal void OnBackgroundImageChanged()
        {
            this.Invalidate();
        }

        internal void OnBackgroundImageSplitMarginChanged()
        {
            this.Invalidate();
        }

        internal void OnItemBKNormalImageChanged()
        {
            this.Invalidate();
        }

        internal void OnItemBKHighlightImageChanged()
        {
            this.Invalidate();
        }

        internal void OnItemBKSelectedImageChanged()
        {
            this.Invalidate();
        }

        internal void OnItemBKDisableImageChanged()
        {
            this.Invalidate();
        }

        internal void OnItemBKSplitMarginChanged()
        {
            this.Invalidate();
        }

        internal void OnGroupHeaderBKNormalImageChanged()
        {
            this.Invalidate();
        }

        internal void OnGroupHeaderBKHighlightImageChanged()
        {
            this.Invalidate();
        }

        internal void OnGroupHeaderBKSelectedImageChanged()
        {
            this.Invalidate();
        }

        internal void OnGroupHeaderBKDisableImageChanged()
        {
            this.Invalidate();
        }

        internal void OnGroupHeaderBKSplitMarginChanged()
        {
            this.Invalidate();
        }

        internal void OnArrowFoldImageChanged()
        {
            this.Invalidate();
        }

        internal void OnArrowUnFoldImageChanged()
        {
            this.Invalidate();
        }

        internal void OnItemSizeChanged()
        {
            this.ResizeScrollBar();
            this.Invalidate();
        }

        internal void OnGroupHeaderHeightChanged()
        {
            this.Invalidate();
        }

        internal void OnGroupExpandChanged(SkinListViewGroup group)
        {
            this.ResizeScrollBar();
            this.Invalidate();

            if (this.GroupExpandedEvent != null)
                this.GroupExpandedEvent(group);
        }

        internal void OnItemStateChanged(SkinListViewItemBase item)
        {
            this.Invalidate();
        }

        #endregion

        #region Mouse Events

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Point pt = new Point(e.X, e.Y);
            bool clickPMArea = false;
            SkinListViewItemBase itemBase = this.ItemFromPoint(pt, out clickPMArea);
            //   if (itemBase != null)
            {
                this.HighlightItem = itemBase;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this.HighlightItem = null;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Point pt = new Point(e.X, e.Y);
            bool clickPMArea = false;
            SkinListViewItemBase itemBase = this.ItemFromPoint(pt, out clickPMArea);
            if (itemBase == null && this.ShowSelectAlways)
                return;

            if (this.MultiSelect)
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    foreach (SkinListViewItemBase item in this.selectedItemList)
                    {
                        if (item == itemBase)
                        {
                            this.selectedItemList.Remove(item);
                            this.Invalidate();
                            return;
                        }
                    }
                    this.selectedItemList.Add(itemBase);
                }
                else
                {
                    SkinListViewItemBase backupItem = this.SelectedItem;

                    foreach (SkinListViewItemBase item in this.selectedItemList)
                    {
                        if (item == itemBase)
                            return;
                    }

                    if (this.SelectedItemChangingEvent != null)
                        this.SelectedItemChangingEvent(this, EventArgs.Empty);

                    this.selectedItemList.Clear();
                    this.selectedItemList.Add(itemBase);

                    if (this.SelectedItemChangedEvent != null)
                    {
                        this.SelectedItemChangedEvent(this, EventArgs.Empty);
                    }
                }
            }
            else
            {
                SkinListViewItemBase backupItem = this.SelectedItem;

                foreach (SkinListViewItemBase item in this.selectedItemList)
                {
                    if (item == itemBase)
                        return;
                }

                if (this.SelectedItemChangingEvent != null)
                    this.SelectedItemChangingEvent(this, EventArgs.Empty);

                this.selectedItemList.Clear();
                this.selectedItemList.Add(itemBase);

                if (this.SelectedItemChangedEvent != null)
                {
                    this.SelectedItemChangedEvent(this, EventArgs.Empty);
                }
            }

            this.Invalidate();
        //    this.SelectedItem = itemBase;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            Point pt = new Point(e.X, e.Y);
            bool clickPMArea = false;
            SkinListViewItemBase itemBase = this.ItemFromPoint(pt, out clickPMArea);
            if (itemBase != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (itemBase is SkinListViewGroup &&
                        ((Control.ModifierKeys & Keys.Control) != Keys.Control))
                    {
                        if (!this.DoubleClickExpanded ||
                            clickPMArea)
                        {
                            SkinListViewGroup group = itemBase as SkinListViewGroup;
                            group.Expand = !group.Expand;
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (itemBase.ContextMenuStrip != null)
                    {
                        itemBase.ContextMenuStrip.Show(this, pt);
                    }
                }
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            Point pt = new Point(e.X, e.Y);
            bool clickPMArea = false;
            SkinListViewItemBase itemBase = this.ItemFromPoint(pt, out clickPMArea);
            if (itemBase != null)
            {
                if (itemBase is SkinListViewGroup)
                {
                    SkinListViewGroup group = itemBase as SkinListViewGroup;
                    if (this.DoubleClickExpanded)
                    {
                        group.Expand = !group.Expand;
                    }
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (this.vscrollBar.Visible)
            {
                int delta = e.Delta;
                if (Math.Abs(delta) > this.theme.ItemSize.Height)
                {
                    if (delta < 0)
                        delta = -this.theme.ItemSize.Height;
                    else
                        delta = this.theme.ItemSize.Height;
                }

                int newPos = this.vscrollBar.Value;
                newPos -= delta;
                if (newPos < 0)
                    this.vscrollBar.Value = 0;
                else if (newPos > this.vscrollBar.Maximum)
                    this.vscrollBar.Value = this.vscrollBar.Maximum;
                else
                    this.vscrollBar.Value = newPos;
            }
        }

        #endregion

        #region Keyboard Events

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Up:
                    this.MoveUp();
                    break;
                case Keys.Down:
                    this.MoveDown();
                    break;
                case Keys.Right:
                    this.MoveRight();
                    break;
                case Keys.Left:
                    this.MoveLeft();
                    break;
            }
        }

        private void MoveUp()
        {
            if (this.selectedItemList.Count == 0)
                return;

            int index = -1;
            if (this.showGroups)
            {
                foreach (SkinListViewGroup group in this.groups)
                {

                }
            }
            else
            {
                for (int i = 0; i < this.items.Count; i++)
                {
                }
            }

            if (index <= 0)
                return;
        }

        private void MoveDown()
        {

        }

        private void MoveRight()
        {

        }

        private void MoveLeft()
        {
        }

        #endregion

        #region Scroll

        private void InitScrollBar()
        {
            this.vscrollBar.Parent = this;
            this.vscrollBar.Minimum = 0;
            this.vscrollBar.Maximum = 0;
            this.vscrollBar.SmallChange = this.theme.ItemSize.Height / 2;
            this.vscrollBar.Hide();

            this.vscrollBar.ValueChanged += new EventHandler(OnScroll);
        }

        private void ResizeScrollBar()
        {
            this.vscrollBar.Left = this.ClientRectangle.Right - this.vscrollBar.Width;
            this.vscrollBar.Top = this.ClientRectangle.Top + 1;
            this.vscrollBar.Height = this.ClientRectangle.Height - 2;
            //   this.vscrollBar.Dock = DockStyle.Right;
            int max = this.GetTotalHeight();
            if (max > this.vscrollBar.Height)
                this.vscrollBar.Maximum = max - this.ClientRectangle.Height + this.theme.ItemSize.Height;
            else
                this.vscrollBar.Maximum = max;
            if (this.vscrollBar.Height > 0)
                this.vscrollBar.LargeChange = this.theme.ItemSize.Height;
            if (max > this.ClientRectangle.Height - 2)
            {
                vscrollBar.Show();
                vscrollBar.BringToFront();
            }
            else
            {
                vscrollBar.Hide();
                vscrollBar.Value = 0;
            }
        }

        private void OnScroll(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        #endregion

        #region Size changed

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.ResizeScrollBar();
        }

        #endregion

        #region Drawing

        #region Drawing Event
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(this.BackColor);

            Rectangle rect = this.ClientRectangle;

            Rectangle clientRect = this.GetClientRectangle();

            this.DrawBackground(g, rect);
            switch (this.view)
            {
                case View.Details:
                    this.DrawDetail(g, clientRect);
                    break;
                case View.LargeIcon:
                    break;
                case View.List:
                    break;
                case View.SmallIcon:
                    break;
                case View.Tile:
                    break;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //    base.OnPaintBackground(e);
        }

        #endregion

        #region Virtual Drawing Methods

        protected virtual void DrawBackground(Graphics g, Rectangle rect)
        {
            ControlState state = ControlState.Normal;

            if (!this.Enabled)
            {
                state = ControlState.Disable;
                if (this.theme.BackgroundImageObject.DisableBitmap == null && this.theme.BackgroundImage != null)
                    this.theme.BackgroundImageObject.DisableBitmap = new Bitmap(ToolStripRenderer.CreateDisabledImage(this.theme.BackgroundImage));
            }

            DrawEngine.DrawRect2(g, this.theme.BackgroundImageObject, rect, state);
        }

        private void DrawDetail(Graphics g, Rectangle rect)
        {
            g.SetClip(rect);

            Point offset = new Point(rect.X, rect.Y - this.GetVScrollBarPosition());
            if (this.showGroups)
            {
                foreach (SkinListViewGroup group in this.groups)
                {
                    Point nextOffset = this.DrawGroup(g, offset, group);
                    offset.Y = nextOffset.Y;
                }
            }
            else
            {
                foreach (SkinListViewItem item in this.items)
                {
                    offset = this.DrawDetailItem(g, offset, item);
                }
            }

            g.ResetClip();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        protected Point DrawGroup(Graphics g, Point offset, SkinListViewGroup group)
        {
            Point groupOffset = offset;

            Rectangle groupRect = this.GetGroupRectangle(offset);
            this.DrawGroupBackground(g, groupRect, group);
            this.DrawArrowImage(g, groupRect, group);
            this.DrawGroupContent(g, groupRect, group);

            groupOffset.Y += groupRect.Height;

            if (!group.Expand)
                return groupOffset;

            foreach (SkinListViewGroup subGroup in group.Groups)
            {
                if (this.theme.ArrowFoldImage != null)
                    groupOffset.X += this.GetArrowWidth();
                groupOffset = this.DrawGroup(g, groupOffset, subGroup);
                groupOffset.X = offset.X;
            }

            foreach (SkinListViewItem item in group.Items)
            {
                Point itemOffset = groupOffset;
                Rectangle itemRect = this.GetItemRectangel(itemOffset);
                this.DrawItemBackground(g, itemRect, item);
                Point newOffset = this.DrawDetailItem(g, itemOffset, item);
                itemOffset.Y = newOffset.Y;
                groupOffset.Y = itemOffset.Y;
            }

            return groupOffset;
        }

        protected Point DrawDetailItem(Graphics g, Point offset, SkinListViewItem item)
        {
            Point itemOffset = offset;

            // Draw item content
            Rectangle itemRect = this.GetItemRectangel(itemOffset);
            this.DrawItemBackground(g, itemRect, item);
            this.DrawItemContent(g, itemRect, item);

            itemOffset.Y += this.theme.ItemSize.Height;

            return itemOffset;
        }

        protected virtual void DrawGroupContent(Graphics g, Rectangle rect, SkinListViewGroup group)
        {
            // Draw Image
            // Draw Item Image
            Rectangle imgRect = Rectangle.Empty;
            if (group.Image != null)
            {
                Image itemImage = group.Image[group.State];
                if (itemImage == null)
                {
                    itemImage = group.Image[ControlState.Normal];
                }

                if (itemImage != null)
                {
                    imgRect = rect;
                    imgRect.X += this.GetArrowWidth();
                    if (itemImage.Height < imgRect.Height)
                    {
                        imgRect.Y += (imgRect.Height - itemImage.Height) / 2;
                        imgRect.Height = itemImage.Height;
                    }
                    else 
                    {
                        imgRect.Y += 1;
                        imgRect.Height -= 2;
                    }

                    imgRect.Width = imgRect.Height;

                    g.DrawImage(itemImage, imgRect, 0, 0, itemImage.Width, itemImage.Height, GraphicsUnit.Pixel);
                }
            }

            // Draw Text
            RectangleF textRect = new RectangleF((float)rect.Left + this.GetArrowWidth() + 5.0f + imgRect.Width, (float)rect.Top,
                (float)(rect.Width - this.GetArrowWidth() - 5 - imgRect.Width), (float)rect.Height);
            StringFormat strFmt = new StringFormat();
            strFmt.Trimming = StringTrimming.EllipsisWord;
            strFmt.FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap;

            this.SetStringAlignment(strFmt, group.TextAlignment);

            Font textFont = group.TextFont;
            if (textFont == null)
                textFont = this.Font;
            Color textColor = group.TextColor;
            foreach (SkinListViewItemBase item in this.selectedItemList)
            {
                if (group == item && group != this.highlightItem)
                {
                    textColor = group.SelectedColor;
                    break;
                }
            }

            g.DrawString(group.Text, textFont, new SolidBrush(textColor), textRect, strFmt);
        }

        protected virtual void DrawArrowImage(Graphics g, Rectangle rect, SkinListViewGroup group)
        {
            Rectangle arrowRect = this.GetArrowRectangle(rect, group);
            if (arrowRect == Rectangle.Empty || arrowRect.Width == 0 || arrowRect.Height == 0)
                return;

            // Draw arrow
            Image arrowImage = this.theme.ArrowFoldImage;
            if (group.Expand)
                arrowImage = this.theme.ArrowUnFoldImage;

            g.DrawImage(arrowImage, arrowRect, 0, 0, arrowImage.Width, arrowImage.Height, GraphicsUnit.Pixel);
        }

        protected Rectangle GetArrowRectangle(Rectangle rect, SkinListViewGroup group)
        {
            if (group.Items.Count == 0 && group.Groups.Count == 0)
                return Rectangle.Empty;

            // Draw arrow
            Image arrowImage = this.theme.ArrowFoldImage;
            if (group.Expand)
                arrowImage = this.theme.ArrowUnFoldImage;

            Rectangle arrowRect = Rectangle.Empty;
            if (arrowImage != null)
            {
                Font textFont = group.TextFont;
                if (textFont == null)
                    textFont = this.Font;

                Graphics g = this.CreateGraphics();
                SizeF textSizeF = g.MeasureString("ABCDEFG", textFont);
                g.Dispose();

                int textHeight = (int)Math.Round(textSizeF.Height + 1.0f);

                arrowRect = rect;
                arrowRect.X += 2;
                if (arrowImage.Height < textHeight)
                {
                    switch (group.TextAlignment)
                    {
                        case ContentAlignment.TopLeft:
                        case ContentAlignment.TopCenter:
                        case ContentAlignment.TopRight:
                            {
                                arrowRect.Y += (textHeight - arrowImage.Height) / 2;
                                arrowRect.Height = arrowImage.Height;
                            }
                            break;
                        case ContentAlignment.MiddleLeft:
                        case ContentAlignment.MiddleCenter:
                        case ContentAlignment.MiddleRight:
                            {
                                arrowRect.Y += (arrowRect.Height - arrowImage.Height) / 2;
                                arrowRect.Height = arrowImage.Height;
                            }
                            break;
                        case ContentAlignment.BottomLeft:
                        case ContentAlignment.BottomCenter:
                        case ContentAlignment.BottomRight:
                            {
                                arrowRect.Y += (arrowRect.Height - textHeight + (textHeight - arrowImage.Height) / 2);
                                arrowRect.Height = arrowImage.Height;
                            }
                            break;
                    }
                }
                arrowRect.Width = arrowImage.Width;
            }

            return arrowRect;
        }

        protected virtual void DrawGroupBackground(Graphics g, Rectangle rect, SkinListViewGroup group)
        {
            ControlState state = group.State;
            foreach (SkinListViewItemBase item in this.selectedItemList)
            {
                if (item == group)
                {
                    state = ControlState.Checked;
                    break;
                }
            }
            if (group == this.highlightItem)
                state = ControlState.Highlight;

            Rectangle bkRect = rect;
            bkRect.X = 0;
            bkRect.Width = this.ClientRectangle.Width - this.GetVScrollBarWidth();
            DrawEngine.DrawRect2(g, this.theme.GroupHeaderBKImageObject, bkRect, state);
        }

        protected virtual void DrawItemContent(Graphics g, Rectangle rect, SkinListViewItem item)
        {
            // Draw Item Image
            Rectangle imgRect = Rectangle.Empty;
            if (item.Image != null)
            {
                Image itemImage = item.Image[item.State];
                if (itemImage == null)
                {
                    itemImage = item.Image[ControlState.Normal];
                }

                if (itemImage != null)
                {
                    imgRect = rect;
                    if (itemImage.Height < imgRect.Height)
                    {
                        imgRect.Y += (imgRect.Height - itemImage.Height) / 2;
                        imgRect.Height = imgRect.Height;
                    }

                    imgRect.Width = itemImage.Width;

                    g.DrawImage(itemImage, imgRect, 0, 0, itemImage.Width, itemImage.Height, GraphicsUnit.Pixel);
                }
            }

            foreach (SkinListViewItem subItem in item.Items)
            {
            }
        }

        protected virtual void DrawItemBackground(Graphics g, Rectangle rect, SkinListViewItem item)
        {
            ControlState state = item.State;
            foreach (SkinListViewItemBase itemBase in this.selectedItemList)
            {
                if (itemBase == item)
                {
                    state = ControlState.Checked;
                    break;
                }
            }
            if (item == this.highlightItem)
                state = ControlState.Highlight;

            Rectangle bkRect = rect;
            bkRect.X = 0;
            bkRect.Width = this.ClientRectangle.Width - this.GetVScrollBarWidth();
            DrawEngine.DrawRect2(g, this.theme.ItemBKImageObject, bkRect, state);
        }

        #endregion

        #endregion Drawing

        #region Help Method

        private Rectangle GetClientRectangle()
        {
            Rectangle clientRect = Rectangle.Inflate(this.ClientRectangle, 0, 0);
            clientRect.Offset(0, 0);

            clientRect.Width -= this.GetVScrollBarWidth();
            clientRect.Height -= this.GetHScrollBarHeight();

            return clientRect;
        }

        protected int GetVScrollBarWidth()
        {
            if (this.vscrollBar != null && this.vscrollBar.Visible)
                return this.vscrollBar.Width;

            return 0;
        }

        private int GetHScrollBarHeight()
        {
            if (this.hscrollBar != null && this.hscrollBar.Visible)
                return this.hscrollBar.Height;

            return 0;
        }

        private int GetVScrollBarPosition()
        {
            if (this.vscrollBar != null && this.vscrollBar.Visible)
                return this.vscrollBar.Value;

            return 0;
        }

        private int GetHScrollBarPosition()
        {
            if (this.hscrollBar != null && this.hscrollBar.Visible)
                return this.hscrollBar.Value;

            return 0;
        }

        private Rectangle GetGroupRectangle(Point offset)
        {
            return new Rectangle(offset.X, offset.Y, this.ClientRectangle.Width - offset.X - this.GetVScrollBarWidth(), this.theme.GroupHeaderHeight);
        }

        private Rectangle GetItemRectangel(Point offset)
        {
            return new Rectangle(this.GetArrowWidth() + offset.X, offset.Y,
                this.ClientRectangle.Width - offset.X - this.GetVScrollBarWidth() - this.GetArrowWidth(), this.theme.ItemSize.Height);
        }

        protected int GetArrowWidth()
        {
            if (!showGroups)
                return 4;

            if (this.theme.ArrowFoldImage != null)
                return this.theme.ArrowFoldImage.Width + 4 + 2;

            if (this.theme.ArrowUnFoldImage != null)
                return this.theme.ArrowUnFoldImage.Width + 4 + 2;

            return 4 + 2;
        }

        private int GetArrowHeight()
        {
            if (this.theme.ArrowFoldImage != null)
                return this.theme.ArrowFoldImage.Height;

            if (this.theme.ArrowUnFoldImage != null)
                return this.theme.ArrowUnFoldImage.Height;

            return 0;
        }

        private SkinListViewItemBase ItemFromPoint(Point pt, out bool clickPMArea)
        {
            Point offset = new Point(1, 1);
            offset.X -= this.GetHScrollBarPosition();
            offset.Y -= this.GetVScrollBarPosition();

            clickPMArea = false;
            if (this.showGroups)
            {
                foreach (SkinListViewGroup group in this.groups)
                {
                    Rectangle groupRect = this.GetGroupRectangle(offset);
                    if (groupRect.Contains(pt))
                    {
                        Rectangle arrowRect = this.GetArrowRectangle(groupRect, group);
                        if (arrowRect.Contains(pt))
                            clickPMArea = true;
                        
                        return group;
                    }

                    offset.Y += this.theme.GroupHeaderHeight;

                    if (!group.Expand)
                        continue;

                    Point nextOffset = offset;
                    if (this.showGroups)
                        nextOffset.X += this.GetArrowWidth();

                    clickPMArea = false;
                    SkinListViewItemBase itemBase = this.ItemFromPoint(group, ref nextOffset, pt, out clickPMArea);
                    if (itemBase != null)
                        return itemBase;

                    offset.Y = nextOffset.Y;
                }
            }
            else
            {
                foreach (SkinListViewItemBase item in this.items)
                {
                    Rectangle itemRect = new Rectangle(offset, new Size(this.ClientRectangle.Width, this.theme.ItemSize.Height));
                    if (itemRect.Contains(pt))
                        return item;

                    offset.Y += theme.ItemSize.Height;
                }
            }

            return null;
        }

        private SkinListViewItemBase ItemFromPoint(SkinListViewGroup group, ref Point offset, Point pt, out bool clickPMArea)
        {
            Point nextGroupOffset = new Point(offset.X, offset.Y);

            clickPMArea = false;
            foreach (SkinListViewGroup g in group.Groups)
            {
                if (this.showGroups)
                {
                    Rectangle groupRect = this.GetGroupRectangle(nextGroupOffset);
                    if (groupRect.Contains(pt))
                    {
                        Rectangle arrowRect = this.GetArrowRectangle(groupRect, group);
                        if (arrowRect.Contains(pt))
                            clickPMArea = true;

                        offset = nextGroupOffset;
                        return g;
                    }

                    nextGroupOffset.Y += this.theme.GroupHeaderHeight;

                    if (!g.Expand)
                        continue;
                }

                if (this.showGroups)
                    nextGroupOffset.X += this.GetArrowWidth();

                clickPMArea = false;
                SkinListViewItemBase itemBase = this.ItemFromPoint(g, ref nextGroupOffset, pt, out clickPMArea);
                if (itemBase != null)
                {
                    offset = nextGroupOffset;
                    return itemBase;
                }

                nextGroupOffset.X = offset.X;
            }

            Point itemOffset = nextGroupOffset;
            foreach (SkinListViewItem i in group.Items)
            {
                Rectangle itemRect = this.GetItemRectangel(itemOffset);
                if (itemRect.Contains(pt))
                {
                    offset = nextGroupOffset;
                    return i;
                }

                itemOffset.Y += this.theme.ItemSize.Height;
            }

            offset = itemOffset;

            return null;
        }

        private int GetTotalHeight()
        {
            int totalHeight = 0;

            if (this.showGroups)
            {
                foreach (SkinListViewGroup group in this.groups)
                {
                    totalHeight += this.theme.GroupHeaderHeight;

                    if (!group.Expand)
                        continue;

                    totalHeight += this.GetTotalHeight(group);
                }
            }
            else
            {
                foreach (SkinListViewItem i in this.items)
                {
                    totalHeight += this.theme.ItemSize.Height;
                }
            }

            return totalHeight;
        }

        private int GetTotalHeight(SkinListViewGroup group)
        {
            int totalHeight = 0;
            foreach (SkinListViewGroup subGroup in group.Groups)
            {
                if (this.showGroups)
                {
                    totalHeight += this.theme.GroupHeaderHeight;

                    if (!subGroup.Expand)
                        continue;
                }

                totalHeight += this.GetTotalHeight(subGroup);
            }

            foreach (SkinListViewItem i in group.Items)
            {
                totalHeight += this.theme.ItemSize.Height;
            }

            return totalHeight;
        }

        private void SetStringAlignment(StringFormat strFmt, ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    {
                        strFmt.Alignment = StringAlignment.Near;
                        strFmt.LineAlignment = StringAlignment.Near;
                    }
                    break;
                case ContentAlignment.TopCenter:
                    {
                        strFmt.Alignment = StringAlignment.Center;
                        strFmt.LineAlignment = StringAlignment.Near;
                    }
                    break;
                case ContentAlignment.TopRight:
                    {
                        strFmt.Alignment = StringAlignment.Far;
                        strFmt.LineAlignment = StringAlignment.Near;
                    }
                    break;
                case ContentAlignment.MiddleLeft:
                    {
                        strFmt.Alignment = StringAlignment.Near;
                        strFmt.LineAlignment = StringAlignment.Center;
                    }
                    break;
                case ContentAlignment.MiddleCenter:
                    {
                        strFmt.Alignment = StringAlignment.Center;
                        strFmt.LineAlignment = StringAlignment.Center;
                    }
                    break;
                case ContentAlignment.MiddleRight:
                    {
                        strFmt.Alignment = StringAlignment.Far;
                        strFmt.LineAlignment = StringAlignment.Center;
                    }
                    break;
                case ContentAlignment.BottomRight:
                    {
                        strFmt.Alignment = StringAlignment.Far;
                        strFmt.LineAlignment = StringAlignment.Far;
                    }
                    break;
                case ContentAlignment.BottomCenter:
                    {
                        strFmt.Alignment = StringAlignment.Far;
                        strFmt.LineAlignment = StringAlignment.Far;
                    }
                    break;
                case ContentAlignment.BottomLeft:
                    {
                        strFmt.Alignment = StringAlignment.Near;
                        strFmt.LineAlignment = StringAlignment.Far;
                    }
                    break;
            }
        }

        #endregion
    }

    public class SkinListViewItemBase
    {
        #region Members

        protected SkinListView owner = null;

        protected ImageObject image = new ImageObject();
        protected string text = string.Empty;

        protected TextImageRelation textImageRelation = TextImageRelation.ImageBeforeText;
        protected ContentAlignment textAlignment = ContentAlignment.MiddleLeft;
        protected ToolStripTextDirection textDirection = ToolStripTextDirection.Horizontal;

        protected Font textFont = null;
        protected Color textColor = Color.Black;
        protected Color selectedColor = Color.Black;

        protected ControlState state = ControlState.Normal;

        protected object tag = null;

        protected SkinListViewItemBase parent = null;

        #endregion

        #region Property

        internal virtual SkinListView SkinListView
        {
            set { this.owner = value; }
        }

        public virtual ImageObject Image
        {
            get { return this.image; }
            set
            {
                this.image = value;
                if (this.owner != null)
                    this.owner.OnItemImageChanged(this);
            }
        }

        public virtual string Text
        {
            get { return this.text; }
            set
            {
                this.text = value;
                if (this.owner != null)
                    this.owner.OnItemTextChanged(this);
            }
        }

        public virtual TextImageRelation TextImageRelation
        {
            get { return this.textImageRelation; }
            set
            {
                this.textImageRelation = value;
                if (this.owner != null)
                    this.owner.OnItemTextImageRelationChanged(this);
            }
        }

        public virtual ContentAlignment TextAlignment
        {
            get { return this.textAlignment; }
            set
            {
                this.textAlignment = value;
                if (this.owner != null)
                    this.owner.OnItemTextAlignmentChanged(this);
            }
        }

        public virtual ToolStripTextDirection TextDirection
        {
            get { return this.textDirection; }
            set
            {
                this.textDirection = value;
                if (this.owner != null)
                    this.owner.OnItemTextDirectionChanged(this);
            }
        }

        public virtual Font TextFont
        {
            get { return this.textFont; }
            set
            {
                this.textFont = value;
                if (this.owner != null)
                    this.owner.OnItemTextFontChanged(this);
            }
        }

        public virtual Color TextColor
        {
            get { return this.textColor; }
            set
            {
                this.textColor = value;
                if (this.owner != null)
                    this.owner.OnItemTextColorChanged(this);
            }
        }

        public virtual Color SelectedColor
        {
            get { return this.selectedColor; }
            set
            {
                this.selectedColor = value;
                if (this.owner != null)
                    this.owner.OnItemTextColorChanged(this);
            }
        }

        public virtual ControlState State
        {
            get { return this.state; }
            set
            {
                this.state = value;
                if (this.owner != null)
                    this.owner.OnItemStateChanged(this);
            }
        }

        public virtual object Tag
        {
            get { return this.tag; }
            set { this.tag = value; }
        }

        public virtual SkinListViewItemBase Parent
        {
            set { this.parent = value; }
            get { return this.parent; }
        }

        public virtual ContextMenuStrip ContextMenuStrip
        {
            get;
            set;
        }

        #endregion
    }

    public sealed class SkinListViewItem : SkinListViewItemBase
    {
        #region Members

        private SkinListViewItemCollection itemCollection = null;

        #endregion

        #region Constructor

        public SkinListViewItem()
        {
            this.itemCollection = new SkinListViewItemCollection(this.owner);
            this.itemCollection.Parent = this;
        }

        #endregion

        #region Property

        public SkinListViewItemCollection Items
        {
            get { return this.itemCollection; }
        }

        #endregion
    }

    public class SkinListViewItemCollection : CollectionBase
    {
        #region Members

        private SkinListView owner = null;
        private SkinListViewItemBase parent = null;

        #endregion

        #region Set View

        public SkinListView SkinListView
        {
            get { return this.owner; }
            set
            {
                this.owner = value;
                foreach (SkinListViewItem item in base.InnerList)
                {
                    item.SkinListView = value;
                }
            }
        }

        public SkinListViewItemBase Parent
        {
            get { return this.parent; }
            set
            {
                this.parent = value;
                foreach (SkinListViewItem item in this.InnerList)
                {
                    item.Parent = value;
                }
            }
        }

        #endregion

        #region Constructor

        public SkinListViewItemCollection()
        {
        }

        public SkinListViewItemCollection(SkinListView owner)
        {
            this.owner = owner;
        }

        #endregion

        #region IList Members

        public int Add(SkinListViewItem item)
        {
            if (this.owner != null)
                this.owner.ItemExist(item);

            int ret = base.InnerList.Add(item);
            item.SkinListView = this.owner;
            if (item.Parent == null)
                item.Parent = this.parent;

            if (this.owner != null)
                this.owner.OnNewItemAdded(item);

            return ret;
        }

        public void Clear()
        {
            base.InnerList.Clear();
            this.owner.OnItemCollectionClear(this);
        }

        public bool Contains(SkinListViewItem item)
        {
            foreach (SkinListViewItem temp in base.InnerList)
            {
                if (item == temp)
                    return true;
            }

            return false;
        }

        public int IndexOf(SkinListViewItem item)
        {
            for (int i = 0; i < base.InnerList.Count; i++)
            {
                if (base.InnerList[i] == item)
                    return i;
            }

            return -1;
        }

        public void Insert(int index, SkinListViewItem item)
        {
            if (this.owner != null)
                this.owner.ItemExist(item);

            base.InnerList.Insert(index, item);
            item.SkinListView = this.owner;
            item.Parent = this.parent;
            if (this.owner != null)
                this.owner.OnNewItemAdded(item);
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Remove(SkinListViewItem item)
        {
            base.InnerList.Remove(item);
            if (this.owner != null)
                this.owner.OnItemDeleted(item);
        }

        public void RemoveAt(int index)
        {
            base.InnerList.RemoveAt(index);
            if (this.owner != null)
                this.owner.OnItemDeleted(this[index]);
        }

        public SkinListViewItem this[int index]
        {
            get
            {
                return (SkinListViewItem)base.InnerList[index];
            }
            set
            {
                if (this.owner != null)
                    this.owner.ItemExist(value);

                base.InnerList[index] = value;
                value.SkinListView = this.owner;
                value.Parent = this.parent;
                if (this.owner != null)
                    this.owner.OnNewItemAdded(value);
            }
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            base.InnerList.CopyTo(array, index);
        }

        public int Count
        {
            get { return base.InnerList.Count; }
        }

        public bool IsSynchronized
        {
            get { return base.InnerList.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return base.InnerList.SyncRoot; }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return base.InnerList.GetEnumerator();
        }

        #endregion
    }

    public sealed class SkinListViewGroup : SkinListViewItemBase
    {
        #region Members

        protected SkinListViewItemCollection items = new SkinListViewItemCollection();
        protected SkinListViewGroupCollection groups = new SkinListViewGroupCollection();

        protected bool expand = true;

        #endregion

        #region Constructor

        public SkinListViewGroup(SkinListViewGroup parent, SkinListView owner)
        {
            this.parent = parent;
            this.SkinListView = owner;
            this.groups.SkinListView = this.owner;
            this.groups.Parent = this;
            this.items.SkinListView = this.owner;
            this.items.Parent = this;
        }

        #endregion

        #region Property

        internal override SkinListView SkinListView
        {
            set
            {
                base.SkinListView = value;

                this.owner = value;
                foreach (SkinListViewItem item in this.items)
                {
                    item.SkinListView = value;
                }

                foreach (SkinListViewGroup group in this.groups)
                {
                    groups.SkinListView = value;
                }
            }
        }

        public SkinListViewItemCollection Items
        {
            get { return this.items; }
        }

        public SkinListViewGroupCollection Groups
        {
            get { return this.groups; }
        }

        public bool Expand
        {
            get { return this.expand; }
            set
            {
                this.expand = value;
                if (this.owner != null)
                    this.owner.OnGroupExpandChanged(this);
            }
        }

        #endregion
    }

    public class SkinListViewGroupCollection : CollectionBase
    {
        #region Members

        private SkinListView owner = null;
        private SkinListViewItemBase parent = null;

        #endregion

        #region Set View

        public SkinListView SkinListView
        {
            get { return this.owner; }
            set
            {
                this.owner = value;
                foreach (SkinListViewGroup item in base.InnerList)
                {
                    item.SkinListView = value;
                }
            }
        }

        public SkinListViewItemBase Parent
        {
            get { return this.parent; }
            set
            {
                this.parent = value;
                foreach (SkinListViewItem item in base.InnerList)
                {
                    item.Parent = value;
                }
            }
        }

        #endregion

        #region Constructor

        public SkinListViewGroupCollection()
        {
        }

        public SkinListViewGroupCollection(SkinListView owner)
        {
            this.owner = owner;
        }

        #endregion

        #region IList Members

        public int Add(SkinListViewGroup item)
        {
            if (this.owner != null)
                this.owner.GroupExist(item);

            int ret = base.InnerList.Add(item);
            item.SkinListView = this.owner;
            item.Groups.SkinListView = this.owner;
            item.Items.SkinListView = this.owner;

            if (this.owner != null)
                this.owner.OnNewGroupAdded(item);

            return ret;
        }

        public void Clear()
        {
            base.InnerList.Clear();
            this.owner.OnGroupCollectionClear(this);
        }

        public bool Contains(SkinListViewGroup item)
        {
            foreach (SkinListViewGroup temp in base.InnerList)
            {
                if (item == temp)
                    return true;
            }

            return false;
        }

        public int IndexOf(SkinListViewGroup item)
        {
            for (int i = 0; i < base.InnerList.Count; i++)
            {
                if (base.InnerList[i] == item)
                    return i;
            }

            return -1;
        }

        public void Insert(int index, SkinListViewGroup item)
        {
            if (this.owner != null)
                this.owner.GroupExist(item);

            base.InnerList.Insert(index, item);
            item.SkinListView = this.owner;
            item.Groups.SkinListView = this.owner;
            item.Items.SkinListView = this.owner;

            if (this.owner != null)
                this.owner.OnNewGroupAdded(item);
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Remove(SkinListViewGroup item)
        {
            base.InnerList.Remove(item);
            if (this.owner != null)
                this.owner.OnGroupDeleted(item);
        }

        public void RemoveAt(int index)
        {
            base.InnerList.RemoveAt(index);
            if (this.owner != null)
                this.owner.OnGroupDeleted(this[index]);
        }

        public SkinListViewGroup this[int index]
        {
            get
            {
                return (SkinListViewGroup)base.InnerList[index];
            }
            set
            {
                if (this.owner != null)
                    this.owner.GroupExist(value);

                base.InnerList[index] = value;
                value.SkinListView = this.owner;
                value.Groups.SkinListView = this.owner;
                value.Items.SkinListView = this.owner;
                if (this.owner != null)
                    this.owner.OnNewGroupAdded(value);
            }
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            base.InnerList.CopyTo(array, index);
        }

        public int Count
        {
            get { return base.InnerList.Count; }
        }

        public bool IsSynchronized
        {
            get { return base.InnerList.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return base.InnerList.SyncRoot; }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return base.InnerList.GetEnumerator();
        }

        #endregion
    }

    public class SkinListViewTheme
    {
        #region Members

        protected SkinListView owner = null;

        protected ImageObject backgroundImageObject = new ImageObject();
        protected ImageObject itemBKImageObject = new ImageObject();
        protected ImageObject groupHeaderBKImageObject = new ImageObject();

        protected Image arrowUnFold = null;
        protected Image arrowFold = null;

        protected Size itemSize = new Size(64, 80);
        protected int groupHeaderHeight = 20;

        #endregion

        #region Constructor

        public SkinListViewTheme(SkinListView owner)
        {
            this.owner = owner;
        }

        #endregion

        #region Property

        #region internal property

        internal ImageObject BackgroundImageObject
        {
            get { return this.backgroundImageObject; }
        }

        internal ImageObject ItemBKImageObject
        {
            get { return this.itemBKImageObject; }
        }

        internal ImageObject GroupHeaderBKImageObject
        {
            get { return this.groupHeaderBKImageObject; }
        }

        #endregion

        #region Background
        public virtual Bitmap BackgroundImage
        {
            get { return this.backgroundImageObject.NormalBitmap; }
            set
            {
                this.backgroundImageObject.NormalBitmap = value;
                this.owner.OnBackgroundImageChanged();
            }
        }

        public virtual Padding BackgroundSplitMargin
        {
            get { return this.backgroundImageObject.SplitMargin; }
            set
            {
                this.backgroundImageObject.SplitMargin = value;
                this.owner.OnBackgroundImageSplitMarginChanged();
            }
        }
        #endregion

        #region Item Background
        public virtual Bitmap ItemBKNormalImage
        {
            get { return this.itemBKImageObject.NormalBitmap; }
            set
            {
                this.itemBKImageObject.NormalBitmap = value;
                if (this.owner != null)
                    this.owner.OnItemBKNormalImageChanged();
            }
        }

        public virtual Bitmap ItemBKHighlightImage
        {
            get { return this.itemBKImageObject.HighlightBitmap; }
            set
            {
                this.itemBKImageObject.HighlightBitmap = value;
                if (this.owner != null)
                    this.owner.OnItemBKHighlightImageChanged();
            }
        }

        public virtual Bitmap ItemBKSelectedImage
        {
            get { return this.itemBKImageObject.CheckedBitmap; }
            set
            {
                this.itemBKImageObject.CheckedBitmap = value;
                if (this.owner != null)
                    this.owner.OnItemBKSelectedImageChanged();
            }
        }

        public virtual Bitmap ItemBKDisableImage
        {
            get { return this.itemBKImageObject.DisableBitmap; }
            set
            {
                this.itemBKImageObject.DisableBitmap = value;
                if (this.owner != null)
                    this.owner.OnItemBKDisableImageChanged();
            }
        }

        public virtual Padding ItemBKSplitMargin
        {
            get { return this.itemBKImageObject.SplitMargin; }
            set
            {
                this.itemBKImageObject.SplitMargin = value;
                if (this.owner != null)
                    this.owner.OnItemBKSplitMarginChanged();
            }
        }
        #endregion

        #region Group Header Background

        public virtual Bitmap GroupHeaderNormalImage
        {
            get { return this.groupHeaderBKImageObject.NormalBitmap; }
            set
            {
                this.groupHeaderBKImageObject.NormalBitmap = value;
                this.owner.OnGroupHeaderBKNormalImageChanged();
            }
        }

        public virtual Bitmap GroupHeaderHighlightImage
        {
            get { return this.groupHeaderBKImageObject.HighlightBitmap; }
            set
            {
                this.groupHeaderBKImageObject.HighlightBitmap = value;
                this.owner.OnGroupHeaderBKHighlightImageChanged();
            }
        }

        public virtual Bitmap GroupHeaderSelectedImage
        {
            get { return this.groupHeaderBKImageObject.CheckedBitmap; }
            set
            {
                this.groupHeaderBKImageObject.CheckedBitmap = value;
                this.owner.OnGroupHeaderBKSelectedImageChanged();
            }
        }

        public virtual Bitmap GroupHeaderDisableImage
        {
            get { return this.groupHeaderBKImageObject.DisableBitmap; }
            set
            {
                this.groupHeaderBKImageObject.DisableBitmap = value;
                this.owner.OnGroupHeaderBKDisableImageChanged();
            }
        }

        public virtual Padding GroupHeaderSplitMargin
        {
            get { return this.groupHeaderBKImageObject.SplitMargin; }
            set
            {
                this.groupHeaderBKImageObject.SplitMargin = value;
                this.owner.OnGroupHeaderBKSplitMarginChanged();
            }
        }
        #endregion

        #region Arrow Image
        public virtual Image ArrowFoldImage
        {
            get { return this.arrowFold; }
            set
            {
                this.arrowFold = value;
                if (this.owner != null)
                    this.owner.OnArrowFoldImageChanged();
            }
        }

        public virtual Image ArrowUnFoldImage
        {
            get { return this.arrowUnFold; }
            set
            {
                this.arrowUnFold = value;
                if (this.owner != null)
                    this.owner.OnArrowUnFoldImageChanged();
            }
        }
        #endregion

        #region Item size and Group header height

        public virtual Size ItemSize
        {
            get { return this.itemSize; }
            set
            {
                this.itemSize = value;
                if (this.owner != null)
                    this.owner.OnItemSizeChanged();
            }
        }

        public virtual int GroupHeaderHeight
        {
            get { return this.groupHeaderHeight; }
            set
            {
                this.groupHeaderHeight = value;
                if (this.owner != null)
                    this.owner.OnGroupHeaderHeightChanged();
            }
        }

        #endregion

        #endregion
    }
}
