using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ZLIS.SkinBuilder
{
    public partial class SkinRadioButton : RadioButton, ISkinBase
    {
        private ControlState state = ControlState.Normal;

        public event DrawingEventHandler DrawButtonEvent;

        private Dictionary<ControlState, Bitmap> buttonBitmapDictionary = new Dictionary<ControlState, Bitmap>();


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


#region Button Bitmaps

        [DefaultValue(null)]
        public Bitmap CheckedBitmap
        {
            set { this.buttonBitmapDictionary[ControlState.Checked] = value; }
            get
            {
                if (!this.buttonBitmapDictionary.ContainsKey(ControlState.Checked))
                    return null;
                return this.buttonBitmapDictionary[ControlState.Checked];
            }
        }

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

        public SkinRadioButton()
        {
            InitializeComponent();
            this.InitStyles();
            this.InitEvents();

            this.ForeColor = SkinManager.ColorTable.Text;
            this.BackColor = Color.Transparent;
        }

        private void InitStyles()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.SetStyle(ControlStyles.ResizeRedraw |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint, true);
        }

        protected virtual void InitEvents()
        {
            this.DrawButtonEvent = new DrawingEventHandler(DrawButton);
        }

#region Mouse Events

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (this.Enabled)
                this.state = ControlState.Highlight;
            else
                this.state = ControlState.Disable;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (this.Enabled)
                this.state = ControlState.Normal;
            else
                this.state = ControlState.Disable;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (this.Enabled)
                this.state = ControlState.Down;
            else
                this.state = ControlState.Disable;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (this.Enabled)
                this.state = ControlState.Down;
            else
                this.state = ControlState.Disable;
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            if (this.Enabled)
                this.state = ControlState.Normal;
            else
                this.state = ControlState.Disable;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            if (!this.Enabled)
                this.state = ControlState.Disable;
            else
                this.state = ControlState.Normal;

            this.Invalidate();
        }

#endregion

#region Drawing Function

        protected override void OnPaint(PaintEventArgs pevent)
        {
         /*   if (this.NormalBitmap == null)
            {
                base.OnPaint(pevent);
                return;
            }
          */ 

            this.InvokePaintBackground(this, pevent);
            this.OnDrawButton(this, pevent.Graphics, this.ClientRectangle);
        }

        private void OnDrawButton(Object sender, Graphics g, Rectangle rect)
        {
            if (this.DrawButtonEvent != null)
                this.DrawButtonEvent(sender, g, rect);
        }

        private void DrawButton(Object sender, Graphics g, Rectangle rect)
        {
            SkinManager.Render.DrawRadioButton(sender, g, rect, this.Checked);
        }

#endregion
    }
}
