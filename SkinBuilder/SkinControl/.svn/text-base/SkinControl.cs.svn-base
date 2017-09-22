using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public partial class SkinControl : UserControl
    {
        protected ImageObject imgObj = new ImageObject();

        public new Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set
            {
                base.BackgroundImage = value;
                imgObj.NormalBitmap = (Bitmap)value;
                this.Invalidate();
            }
        }

        public Padding SplitMargin
        {
            get { return this.imgObj.SplitMargin; }
            set { this.imgObj.SplitMargin = value; this.Invalidate(); }
        }

        public SkinControl()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw | 
                ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.BackgroundImage == null)
            {
                base.OnPaint(e);
                return;
            }

            DrawEngine.DrawRect2(e.Graphics, imgObj, this.ClientRectangle, ControlState.Normal);
        }
    }
}
