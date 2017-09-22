using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public partial class SkinComboBox : ComboBox, ISkinBase
    {
        private ControlState state = ControlState.Normal;

        public event DrawingEventHandler DrawComboBorder;
        public event DrawingEventHandler DrawComboButton;

        private Dictionary<ControlState, Bitmap> buttonBitmapDictionary = new Dictionary<ControlState, Bitmap>();
        private Color borderNormalColor = Color.Black;
        private Color borderHighlightColor = Color.Gray;


#region From ISkinBase

        public ControlState State()
        {
            return this.state;
        }

        public Bitmap GetBitmap(ControlState state)
        {
            if (this.buttonBitmapDictionary.ContainsKey(state))
                return this.buttonBitmapDictionary[state];

            return null;
        }

        public ImageObject GetImageObject()
        {
            return null;
        }

#endregion


#region Properties

        public ControlState ControlState
        {
            get { return this.state; }
        }

#endregion

#region Button Bitmaps

        [DefaultValue(null)]
        public Bitmap NormalBitmap
        {
            set { this.buttonBitmapDictionary[ControlState.Normal] = value; }
            get 
            {
                if (!this.buttonBitmapDictionary.ContainsKey(ControlState.Normal))
                    return null;
                return this.buttonBitmapDictionary[ControlState.Normal]; 
            }
        }

        [DefaultValue(null)]
        public Bitmap HighlightBitmap
        {
            set { this.buttonBitmapDictionary[ControlState.Highlight] = value; }
            get
            {
                if (!this.buttonBitmapDictionary.ContainsKey(ControlState.Highlight))
                    return null;
                return this.buttonBitmapDictionary[ControlState.Highlight];
            }
        }

        [DefaultValue(null)]
        public Bitmap DownBitmap
        {
            set { this.buttonBitmapDictionary[ControlState.Down] = value; }
            get
            {
                if (!this.buttonBitmapDictionary.ContainsKey(ControlState.Down))
                    return null;
                return this.buttonBitmapDictionary[ControlState.Down];
            }
        }

        [DefaultValue(null)]
        public Bitmap DisableBitmap
        {
            set { this.buttonBitmapDictionary[ControlState.Disable] = value; }
            get
            {
                if (!this.buttonBitmapDictionary.ContainsKey(ControlState.Disable))
                    return null;
                return this.buttonBitmapDictionary[ControlState.Disable];
            }
        }

#endregion

#region Border Color

        [DefaultValue(typeof(Color), "Black")]
        public Color BorderNormalColor
        {
            set { this.borderNormalColor = value; }
            get { return this.borderNormalColor; }
        }

        [DefaultValue(typeof(Color), "Gray")]
        public Color BorderHighlightColor
        {
            set { this.borderHighlightColor = value; }
            get { return this.borderHighlightColor; }
        }

#endregion

#region Constructor and Init Functions

        public SkinComboBox()
        {
            InitializeComponent();

            this.InitStyles();
            this.InitEvents();
        }

        private void InitStyles()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.ResizeRedraw, true);
        }

        private void InitEvents()
        {
            this.DrawComboBorder += new DrawingEventHandler(DrawComboBoxBorder);
            this.DrawComboButton += new DrawingEventHandler(DrawComboBoxButton);
        }

#endregion

#region Mouse Events

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (this.Enabled)
                this.state = ControlState.Highlight;
            else
                this.state = ControlState.Disable;

            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (this.Enabled)
                this.state = ControlState.Normal;
            else
                this.state = ControlState.Disable;

            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (this.Enabled)
                this.state = ControlState.Down;
            else
                this.state = ControlState.Disable;

            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (this.Enabled && !this.DroppedDown)
                state = ControlState.Normal;
            else if (this.DroppedDown)
                state = ControlState.Down;
            else
                state = ControlState.Disable;

            this.Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (this.Enabled)
                this.state = ControlState.Down;
            else
                this.state = ControlState.Disable;

            this.Invalidate();
        }

#endregion

#region  Drop Down Events

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);

            if (this.Enabled)
                this.state = ControlState.Down;
            else
                this.state = ControlState.Disable;

            this.Invalidate();
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);

            if (this.Enabled)
                this.state = ControlState.Normal;
            else
                this.state = ControlState.Disable;

            this.Invalidate();
        }

#endregion

#region Windows Proc

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            try
            {
                if (this.DropDownStyle != ComboBoxStyle.Simple)
                {
                    Health121.Utility.Win32.Messages msg = (Health121.Utility.Win32.Messages)Enum.Parse(typeof(Health121.Utility.Win32.Messages), m.Msg.ToString());
                    switch (msg)
                    {
                        case Health121.Utility.Win32.Messages.WM_PAINT:
                            {
                                Graphics g = null;
                                try
                                {
                                    g = Graphics.FromHwnd(this.Handle);
                                }
                                catch (Exception)
                                {

                                }

                                if (g == null)
                                    return;

                                Bitmap bmp = new Bitmap(this.Width, this.Height);
                                Graphics gBmp = Graphics.FromImage(bmp);

                                gBmp.Clear(this.BackColor);

                                if (this.RightToLeft == RightToLeft.Yes)
                                {
                                    Rectangle rect = new Rectangle(1, 1, 17, this.Height - 2);
                                    OnDrawComboBoxButton(this, gBmp, rect);
                                }
                                else
                                {
                                    Rectangle rect = new Rectangle(this.Width - 18, 0, 17, this.Height - 1);
                                    OnDrawComboBoxButton(this, gBmp, rect);
                                }

                                OnDrawComboBoxBorder(this, gBmp, Help.ConvRectangle(g.ClipBounds));

                                StringFormat strFmt = new StringFormat();
                                strFmt.Alignment = StringAlignment.Near;
                                strFmt.LineAlignment = StringAlignment.Center;
                                strFmt.Trimming = StringTrimming.EllipsisCharacter;
                                strFmt.FormatFlags = StringFormatFlags.LineLimit;
                                gBmp.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new Rectangle(0, 2, this.Width - 20, this.Height), strFmt);

                                if (!this.DesignMode)
                                {
                                    try
                                    {
                                        //#if !DEBUG  // There is some problem with this on design mode
                                  //           Health121.Utility.Win32.User.SendMessage(this.Handle, Health121.Utility.Win32.Messages.WM_PRINTCLIENT, 0, 0);
                                        //#endif
                                    }
                                    catch
                                    {

                                    }
                                }

                                g.DrawImage(bmp, new Rectangle(0, 0, this.Width, this.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel);

                                gBmp.Dispose();
                                bmp.Dispose();
                                g.Dispose();
                            }
                            return;
                        case Health121.Utility.Win32.Messages.WM_ERASEBKGND:
                            m.Result = IntPtr.Zero;
                            break;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

#endregion

#region Drawing Functions

        protected virtual void OnDrawComboBoxBorder(Object sender, Graphics g, Rectangle rect)
        {
            if (DrawComboBorder != null)
                DrawComboBorder(sender, g, rect);
        }

        protected virtual void OnDrawComboBoxButton(Object sender, Graphics g, Rectangle rect)
        {
            if (DrawComboButton != null)
                DrawComboButton(sender, g, rect);
        }

        private void DrawComboBoxBorder(Object sender, Graphics g, Rectangle rect)
        {
            SkinManager.Render.DrawComboBoxBorder(sender, g, rect);
        }

        private void DrawComboBoxButton(Object sender, Graphics g, Rectangle rect)
        {
            SkinManager.Render.DrawComboBoxButton(sender, g, rect);
        }
#endregion
    }
}