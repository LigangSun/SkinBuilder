using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ZLIS.SkinBuilder
{
    public partial class SkinSpliterContainer : SplitContainer
    {
        private ImageObject imageObject = new ImageObject();

        public Bitmap SplitterImage
        {
            set
            {
                this.imageObject.NormalBitmap = value;
                this.Invalidate();
            }
            get { return this.imageObject.NormalBitmap; }
        }

        public Padding SplitterSplitMargin
        {
            get { return this.imageObject.SplitMargin; }
            set
            {
                this.imageObject.SplitMargin = value;
                this.Invalidate();
            }
        }

        public SkinSpliterContainer()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.ResizeRedraw, true);
        }

        #region Members
        private Color startColor = Color.White;
        private Color endColor = Color.White;
        private int angle = 90;
#endregion

#region Properties
      
        public Color StartColor 
        {
            get { return this.startColor; }
            set 
            {
                this.startColor = value;
                this.Invalidate();
            }
        }

        public Color EndColor
        {
            get { return this.endColor; }
            set 
            { 
                this.endColor = value;
                this.Invalidate();
            }
        }

        public int Angle
        {
            get { return this.angle; }
            set 
            {
                this.angle = value;
                this.Invalidate();
            }
        }

#endregion

#region Paint

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.BackColor == Color.Transparent)
               base.OnPaintBackground(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.ClientRectangle.Width == 0 || this.ClientRectangle.Height == 0)
            {
                base.OnPaint(e);
                return;
            }

            if (this.BackColor == Color.Transparent)
                base.OnPaint(e);
            else
            {
                using (LinearGradientBrush b = new LinearGradientBrush(
                        new PointF(0, 0), new PointF(0, this.ClientRectangle.Height),
                        this.StartColor,
                        this.EndColor))
                {
                    e.Graphics.FillRectangle(b, this.ClientRectangle);
                }
            }

            if (this.SplitterImage != null)
            {
                DrawEngine.DrawRect2(e.Graphics, this.imageObject, this.SplitterRectangle, ControlState.Normal);
            }
        }
#endregion
    }
}
