// ***************************************************************
//  SkinForm   version:  1.0   ? date: 02/14/2008
//  -------------------------------------------------------------
//  
//  -------------------------------------------------------------
//  Health121 Copyright (C) 2008 - All Rights Reserved
// ***************************************************************
//  
//  Author: Jeffery
//  
//  Purpurs:
// ***************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Health121.Utility.Win32;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using Health121.Utility.Win32.Common;

namespace ZLIS.SkinBuilder
{
    public partial class SkinForm : Form
    {
        #region Public Enum

        public enum MaskType
        {
            TopTowRoundCorner,
            TopFourRoundCorner,

            FullMask
        }

        public enum MoveType
        {
            Title,
            Whole,

            None
        }
        #endregion

        #region Members

        private MaskType maskType = MaskType.TopTowRoundCorner;
        private Bitmap maskBitmap = null;
        private Color maskColor = Color.FromArgb(0, 255, 255, 255);
        private Int32 cornerRadius = 6;
        private bool canResize = false;

        private ImageObject imageObject = new ImageObject();

        private int captionHeight = SystemInformation.CaptionHeight;
        private MoveType moveType = MoveType.Title;

        private ContentAlignment titleAlignment = ContentAlignment.MiddleLeft;
        private string titleText;
        private Bitmap titleBitmap;

        #endregion

        #region Properties

        public override Image BackgroundImage
        {
            set { this.imageObject[ControlState.Normal] = (Bitmap)value; }
            get { return this.imageObject[ControlState.Normal]; }
        }

        public Padding SplitMargin
        {
            set { this.imageObject.SplitMargin = value; }
            get { return this.imageObject.SplitMargin; }
        }

        [DefaultValue(typeof(ZLIS.SkinBuilder.SkinForm.MaskType), "TopFourRoundCorner")]
        public MaskType Mask
        {
            get { return this.maskType; }
            set { this.maskType = value; }
        }

        [DefaultValue(null)]
        public Bitmap MaskBitmap
        {
            get { return this.maskBitmap; }
            set
            {
                this.maskBitmap = value;
                if (this.maskBitmap != null)
                {
                    this.maskColor = this.maskBitmap.GetPixel(0, 0);
                    this.MaskForm();
                }
            }
        }

        [DefaultValue(typeof(Color), "Transparent")]
        private Color MaskColor
        {
            get { return this.maskColor; }
            set
            {
                this.maskColor = value;
            }
        }

        [DefaultValue(10)]
        public Int32 CornerRadius
        {
            get { return this.cornerRadius; }
            set { this.cornerRadius = value; }
        }

        public bool CanResize
        {
            get { return this.canResize; }
            set { this.canResize = value; }
        }

        public int CaptionHeight
        {
            get { return this.captionHeight; }
            set { this.captionHeight = value; }
        }

        public MoveType MoveMode
        {
            get { return this.moveType; }
            set { this.moveType = value; }
        }

        public ContentAlignment TitleAlignment
        {
            get { return this.titleAlignment; }
            set { this.titleAlignment = value; }
        }

        public string TitleText
        {
            get { return this.titleText; }
            set
            {
                this.titleText = value;
                this.Invalidate();
            }
        }

        public Bitmap TitleBitmap
        {
            get { return this.titleBitmap; }
            set { this.titleBitmap = value; }
        }

        public new bool MaximizeBox
        {
            get { return base.MaximizeBox; }
            set
            {
                base.MaximizeBox = value;
                if (value)
                {
                    this.ModifyStyle(WindowStyles.WS_OVERLAPPED, WindowStyles.WS_MAXIMIZEBOX);
                }
                else
                {
                    this.ModifyStyle(WindowStyles.WS_MAXIMIZEBOX, WindowStyles.WS_SYSMENU);
                }

                this.ModifyStyle(WindowStyles.WS_OVERLAPPED, WindowStyles.WS_SYSMENU);
            }
        }

        public new bool MinimizeBox
        {
            get { return base.MinimizeBox; }
            set
            {
                base.MinimizeBox = value;
                if (value)
                {
                    this.ModifyStyle(WindowStyles.WS_OVERLAPPED, WindowStyles.WS_MINIMIZEBOX);
                }
                else
                {
                    this.ModifyStyle(WindowStyles.WS_MINIMIZEBOX, WindowStyles.WS_SYSMENU);
                }

                this.ModifyStyle(WindowStyles.WS_OVERLAPPED, WindowStyles.WS_SYSMENU);
            }
        }

        protected new bool DesignMode
        {
            get
            {
                try
                {
                    if (base.DesignMode)
                        return true;

                    Control parent = this.Parent;
                    while (parent != null)
                    {
                        if (parent.Site != null)
                            return parent.Site.DesignMode;

                        parent = parent.Parent;
                    }

                    return false;
                }
                catch (System.Exception ex)
                {

                }

                return false;
            }
        }

        #endregion

        #region Constructor and Init functions

        public SkinForm()
        {
            InitializeComponent();

            this.InitTitleRectangle();

            this.AutoScaleMode = AutoScaleMode.None;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            this.InitStyles();
        }

        protected virtual void InitTitleRectangle()
        {
            if (this.canResize)
            {
                if (this.moveType == MoveType.Title)
                    this.captionHeight = SystemInformation.CaptionHeight;
                else if (this.moveType == MoveType.Whole)
                    this.captionHeight = this.ClientRectangle.Height - SystemInformation.FrameBorderSize.Height * 2;
                else
                    this.captionHeight = 0;
            }
            else
            {
                if (this.moveType == MoveType.Title)
                    this.captionHeight = SystemInformation.CaptionHeight;
                else if (this.moveType == MoveType.Whole)
                    this.captionHeight = this.ClientRectangle.Height;
                else
                    this.captionHeight = 0;
            }
        }

        protected virtual void InitStyles()
        {
            this.ResizeRedraw = true;
            this.DoubleBuffered = true;

            this.ModifyStyle(WindowStyles.WS_OVERLAPPED, WindowStyles.WS_SYSMENU);
            if (this.MinimizeBox)
                this.ModifyStyle(WindowStyles.WS_OVERLAPPED, WindowStyles.WS_MINIMIZEBOX);
            else
                this.ModifyStyle(WindowStyles.WS_MINIMIZEBOX, 0);
            if (this.MaximizeBox)
                this.ModifyStyle(WindowStyles.WS_OVERLAPPED, WindowStyles.WS_MAXIMIZEBOX);
            else
                this.ModifyStyle(WindowStyles.WS_MAXIMIZEBOX, 0);
            this.ModifyStyle(WindowStyles.WS_OVERLAPPED, WindowStyles.WS_CLIPCHILDREN);

            //    this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }
        #endregion

        #region Mask

        protected virtual void MaskForm()
        {
            DateTime start = DateTime.Now;
            Region region = new Region(new Rectangle(0, 0, this.Width, this.Height));
            if (this.WindowState == FormWindowState.Normal)
            {
                switch (this.maskType)
                {
                    case MaskType.TopTowRoundCorner:
                        {
                            if (this.maskBitmap != null)
                            {
                                for (int i = 0; i < this.cornerRadius; i++)
                                {
                                    for (int j = 0; j < cornerRadius; j++)
                                    {
                                        Color clr = this.maskBitmap.GetPixel(i, j);
                                        if (clr == this.MaskColor)
                                        {
                                            region.Xor(new Rectangle(i, j, 1, 1));
                                        }

                                        clr = this.maskBitmap.GetPixel(this.maskBitmap.Width - i - 1, j);
                                        if (clr == this.MaskColor)
                                        {
                                            region.Xor(new Rectangle(this.Width - i - 1, j, 1, 1));
                                        }
                                    }
                                }
                            }
                            else
                                region = new Region(PathCreator.CreateTopRoundRectangle(new Rectangle(0, 0, this.Width, this.Height), this.cornerRadius));
                        }
                        break;
                    case MaskType.TopFourRoundCorner:
                        {
                            if (this.maskBitmap != null)
                            {
                                Color maskColor = this.maskBitmap.GetPixel(0, 0);
                                for (int i = 0; i < this.cornerRadius; i++)
                                {
                                    for (int j = 0; j < cornerRadius; j++)
                                    {
                                        Color clr = this.maskBitmap.GetPixel(i, j);
                                        if (clr == maskColor)
                                        {
                                            region.Xor(new Rectangle(i, j, 1, 1));
                                        }

                                        clr = this.maskBitmap.GetPixel(this.maskBitmap.Width - i - 1, j);
                                        if (clr == maskColor)
                                        {
                                            region.Xor(new Rectangle(this.Width - i - 1, j, 1, 1));
                                        }

                                        clr = this.maskBitmap.GetPixel(i, this.maskBitmap.Height - j - 1);
                                        if (clr == maskColor)
                                        {
                                            region.Xor(new Rectangle(i, this.Height - j - 1, 1, 1));
                                        }

                                        clr = this.maskBitmap.GetPixel(this.maskBitmap.Width - i - 1, this.maskBitmap.Height - j - 1);
                                        if (clr == maskColor)
                                        {
                                            region.Xor(new Rectangle(this.Width - i - 1, this.Height - j - 1, 1, 1));
                                        }
                                    }
                                }
                            }
                            else
                                region = new Region(PathCreator.CreateRoundRectangle(new Rectangle(0, 0, this.Width, this.Height), this.cornerRadius));
                        }
                        break;
                    case MaskType.FullMask:
                        {

                        }
                        break;
                }
            }
            this.Region = region;

            Trace.Write("User time(0):");
            Trace.WriteLine(DateTime.Now - start);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            this.MaskForm();
        }

        #endregion

        #region Public functions
        public bool ModifyStyle(WindowStyles remove, WindowStyles add)
        {
            int style = (int)User.GetWindowLong(this.Handle, User.WindowsLongType.GWL_STYLE);
            int newStyle = ((style & ~(int)remove) | (int)add);
            if (style == newStyle)
                return false;

            IntPtr ret = User.SetWindowLong(this.Handle, User.WindowsLongType.GWL_STYLE, (IntPtr)newStyle);

            return true;
        }
        #endregion

        #region For windows resize

        private Rectangle CaptionRectangle
        {
            get
            {
                int border = 0;
                if (this.canResize)
                    border = SystemInformation.FrameBorderSize.Width;

                Rectangle rect = Rectangle.Empty;
                switch (this.moveType)
                {
                    case MoveType.Title:
                    case MoveType.Whole:
                        rect = new Rectangle(border, border, this.ClientRectangle.Width - 2 * border, this.captionHeight);
                        break;
                    case MoveType.None:
                        break;
                }

                return rect;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            Health121.Utility.Win32.Messages msg = (Health121.Utility.Win32.Messages)Enum.Parse(typeof(Health121.Utility.Win32.Messages), m.Msg.ToString());
            switch (msg)
            {
                case Messages.WM_ERASEBKGND:
                    m.Result = (IntPtr)1;
                    break;
                case Messages.WM_POPUPSYSMENU:
                    if (this.ModifyStyle(WindowStyles.WS_OVERLAPPED, WindowStyles.WS_SYSMENU))
                    {
                        User.PostMessage(this.Handle, Messages.WM_POPUPSYSMENU, m.WParam, m.LParam);
                    }
                    break;
                case Messages.WM_LBUTTONDBLCLK:
                    {
                        Point p = (new Point(Helper.LoWrd((uint)m.LParam), Helper.HiWrd((uint)m.LParam)));
                        if (this.CaptionRectangle.Contains(p) && this.canResize)
                        {
                            if (this.WindowState != FormWindowState.Maximized)
                                this.WindowState = FormWindowState.Maximized;
                            else
                                this.WindowState = FormWindowState.Normal;
                        }
                    }
                    break;
                case Messages.WM_LBUTTONDOWN:
                    {

                    }
                    break;
                case Messages.WM_MOUSEMOVE:
                    {

                    }
                    break;
                case Messages.WM_RBUTTONDOWN:
                    {
                        /*   if (this.Icon != null && this.ShowIcon)
                           {
                               Rectangle iconRect = GenerateIconRect();
                               Point p = this.PointToClient(new Point(Helper.LoWrd((uint)m.LParam), Helper.HiWrd((uint)m.LParam)));
                               if (iconRect.Contains(p))
                               {
                                   int value = (SystemInformation.FrameBorderSize.Width & 0xffff) | (this.captionHeight << 16);
                                   User.PostMessage(this.Handle, Messages.WM_POPUPSYSMENU, m.WParam, new IntPtr(value));
                               }
                           }*/
                    }
                    break;
                case Messages.WM_LBUTTONUP:
                    {

                    }
                    break;
                case Messages.WM_NCHITTEST:
                    {
                        // This is the place where the skin NC area is handled.
                        if (m.Result == (IntPtr)NCHITTEST.HTCLIENT)
                        {
                            this.ModifyStyle(WindowStyles.WS_SYSMENU, WindowStyles.WS_OVERLAPPED);

                            int HITTEST = NCHITTEST.HTCLIENT;

                            Point p = this.PointToClient(new Point(Helper.LoWrd((uint)m.LParam), Helper.HiWrd((uint)m.LParam)));
                            if (this.CaptionRectangle.Contains(p))
                            {
                                if (this.WindowState == FormWindowState.Maximized)
                                    return;

                                //    HITTEST = NCHITTEST.HTCAPTION;
                            }
                            else
                            {
                                if (this.WindowState == FormWindowState.Maximized)
                                    return;

                                if (this.canResize) // Check if the form is resizable
                                {
                                    int xF = this.Width, yF = this.Height;
                                    int xSide = 0;
                                    int border = SystemInformation.FrameBorderSize.Width;
                                    if ((p.X >= xF - this.cornerRadius) && ((p.Y >= yF - this.cornerRadius)))
                                    {
                                        HITTEST = NCHITTEST.HTBOTTOMRIGHT;
                                    }
                                    else
                                    {
                                        // Left side
                                        if (p.X <= 8)
                                        {
                                            if (p.X <= border) HITTEST = NCHITTEST.HTLEFT;
                                            xSide = 1;
                                        }
                                        // Right side
                                        if (p.X >= xF - 8)
                                        {
                                            if (p.X >= xF - border) HITTEST = NCHITTEST.HTRIGHT;
                                            xSide = 2;
                                        }
                                        // Top side
                                        if (p.Y <= border)
                                        {
                                            HITTEST = NCHITTEST.HTTOP;
                                            if (xSide == 1)
                                            {
                                                HITTEST = NCHITTEST.HTTOPLEFT;
                                            }
                                            else if (xSide == 2)
                                            {
                                                HITTEST = NCHITTEST.HTTOPRIGHT;
                                            }
                                        }
                                        // Bottom side
                                        if (p.Y >= yF - border)
                                        {
                                            if (xSide == 1)
                                            {
                                                HITTEST = NCHITTEST.HTBOTTOMLEFT;
                                            }
                                            else
                                            {
                                                HITTEST = NCHITTEST.HTBOTTOM;
                                            }
                                        }
                                    }
                                }
                            }

                            m.Result = (IntPtr)HITTEST;
                        }
                    }
                    break;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        private bool mouseOnBorder = false;
        private bool leftButtonDown = false;
        private Point startPoint = Point.Empty;
        private Point prePoint = Point.Empty;
        private MoveDirection moveDirection = MoveDirection.None;
        private Point startLocation = Point.Empty;

        private enum MoveDirection
        {
            Left,
            Right,
            Top,
            Bottom,

            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,

            None
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this.InitStyles();

            return;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.WindowState != FormWindowState.Normal)
                return;

            Point currentPt = this.PointToClient(Control.MousePosition);

            if (this.leftButtonDown)
            {
                Rectangle workingArea = SystemInformation.WorkingArea;
                workingArea.Height -= SystemInformation.FrameBorderSize.Height;
                if (!workingArea.Contains(this.PointToScreen(currentPt)))
                    return;

                Point deltaPt = new Point(Control.MousePosition.X - this.startPoint.X,
                    Control.MousePosition.Y - this.startPoint.Y);
                this.Location = new Point(this.startLocation.X + deltaPt.X,
                                          this.startLocation.Y + deltaPt.Y);

                Trace.WriteLine(string.Format("Delta {0}", deltaPt));
                Trace.WriteLine(string.Format("Location {0}", Location));
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (this.WindowState == FormWindowState.Maximized)
                return;

            if (!this.CaptionRectangle.Contains(new Point(e.X, e.Y)))
                return;

            this.leftButtonDown = true;
            this.Capture = true;

            this.startPoint = Control.MousePosition;
            this.startLocation = this.Location;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.Capture = false;
            this.leftButtonDown = false;
            this.startPoint = Point.Empty;
            this.prePoint = Point.Empty;
            this.startLocation = Point.Empty;
        }

        #endregion

        #region Draw background
        protected override void OnPaint(PaintEventArgs e)
        {
            //    base.InvokePaintBackground(this, e);

            DrawEngine.DrawRect2(e.Graphics, this.imageObject, this.ClientRectangle, ControlState.Normal);

            if (this.Icon != null &&
                this.ShowIcon &&
                (this.titleAlignment == ContentAlignment.TopLeft ||
                this.titleAlignment == ContentAlignment.MiddleLeft ||
                this.titleAlignment == ContentAlignment.BottomLeft))
            {
                Rectangle iconRect = GenerateIconRect();
                Bitmap iconBmp = this.Icon.ToBitmap();
                e.Graphics.DrawImage(iconBmp, iconRect, 0, 0, this.Icon.Width, this.Icon.Height, GraphicsUnit.Pixel);
                iconBmp.Dispose();
            }

            Rectangle titleRect = new Rectangle(SystemInformation.FixedFrameBorderSize.Width, 0, this.ClientRectangle.Width, this.captionHeight);
            if (this.TitleBitmap != null)
            {
                Rectangle bitmapRect = titleRect;
                switch (this.titleAlignment)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.BottomLeft:
                        {
                            int left = titleRect.Left;
                            if (this.Icon != null)
                                left += SystemInformation.SmallIconSize.Width;
                            bitmapRect = new Rectangle(left,
                                SystemInformation.SizingBorderWidth + (this.captionHeight - this.TitleBitmap.Height) / 2,
                                this.TitleBitmap.Width,
                                this.TitleBitmap.Height);
                        }
                        break;
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.BottomCenter:
                        {
                            bitmapRect = new Rectangle((titleRect.Width - this.TitleBitmap.Width) / 2,
                                SystemInformation.SizingBorderWidth + (this.captionHeight - this.TitleBitmap.Height) / 2,
                                this.TitleBitmap.Width,
                                this.TitleBitmap.Height);
                        }
                        break;
                    case ContentAlignment.TopRight:
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.BottomRight:
                        {
                            bitmapRect = new Rectangle(titleRect.Width - this.TitleBitmap.Width,
                                SystemInformation.SizingBorderWidth + (this.captionHeight - this.TitleBitmap.Height) / 2,
                                this.TitleBitmap.Width,
                                this.TitleBitmap.Height);
                        }
                        break;
                }

                e.Graphics.DrawImage(this.TitleBitmap, bitmapRect, 0, 0, this.TitleBitmap.Width, this.TitleBitmap.Height, GraphicsUnit.Pixel);
            }
            else
            {
                string title = this.TitleText;
                if (title == null || title.Length == 0)
                    title = this.Text;

                SizeF size = e.Graphics.MeasureString(title, this.Font);
                size.Width += 1.0f;
                size.Height += 1.0f;

                Rectangle textRect = titleRect;
                StringFormat strFmt = new StringFormat();
                strFmt.LineAlignment = StringAlignment.Center;
                switch (this.titleAlignment)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.BottomLeft:
                        {
                            int left = SystemInformation.SizingBorderWidth;
                            if (this.Icon != null && this.ShowIcon)
                                left += SystemInformation.SmallIconSize.Width;
                            textRect = new Rectangle(left,
                                SystemInformation.FixedFrameBorderSize.Height + (this.captionHeight - (int)size.Height) / 2,
                                (int)Math.Round(size.Width),
                                (int)Math.Round(size.Height));

                            strFmt.Alignment = StringAlignment.Near;
                        }
                        break;
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.BottomCenter:
                        {
                            textRect = new Rectangle((titleRect.Width - (int)size.Width) / 2,
                                SystemInformation.FixedFrameBorderSize.Height + (this.captionHeight - (int)size.Height) / 2,
                                (int)Math.Round(size.Width),
                                (int)Math.Round(size.Height));

                            strFmt.Alignment = StringAlignment.Center;
                        }
                        break;
                    case ContentAlignment.TopRight:
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.BottomRight:
                        {
                            textRect = new Rectangle(titleRect.Width - (int)size.Width,
                                SystemInformation.SizingBorderWidth + (this.captionHeight - (int)size.Height) / 2,
                                (int)Math.Round(size.Width),
                                (int)Math.Round(size.Height));

                            strFmt.Alignment = StringAlignment.Far;
                        }
                        break;
                }

                e.Graphics.DrawString(title, this.Font, new SolidBrush(this.ForeColor), textRect, strFmt);
            }
        }
        #endregion

        public Rectangle GenerateIconRect()
        {
            return new Rectangle(SystemInformation.SizingBorderWidth, (this.captionHeight - SystemInformation.SmallIconSize.Height) / 2,
                    SystemInformation.SmallIconSize.Width, SystemInformation.SmallIconSize.Height);
        }
    }
}