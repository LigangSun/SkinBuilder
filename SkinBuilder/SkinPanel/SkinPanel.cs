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
    public partial class SkinPanel : Panel
    {
#region Members
        private Color startColor = Color.White;
        private Color endColor = Color.White;
        private int angle = 90;

        private ImageObject imageObject = new ImageObject();

        private Color borderStartColor = Color.Transparent;
        private Color borderEndColor = Color.Transparent;
        private int borderColorAngle = 90;
        private int borderWidth = 1;

        private bool border;
#endregion

#region Properties
        public ImageObject ImageObject
        {
            get { return this.imageObject; }
            set { this.imageObject = value; }
        }

        public override Image BackgroundImage
        {
            set { this.imageObject[ControlState.Normal] = (Bitmap)value; }
            get { return this.imageObject[ControlState.Normal]; }
        }

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

        public Color BorderStartColor
        {
            get { return this.borderStartColor; }
            set
            {
                this.borderStartColor = value;
                this.Invalidate();
            }
        }

        public Color BorderEndColor
        {
            get { return this.borderEndColor; }
            set
            {
                this.borderEndColor = value;
                this.Invalidate();
            }
        }

        public int BorderColorAngle
        {
            get { return this.borderColorAngle; }
            set
            {
                this.borderColorAngle = value;
                this.Invalidate();
            }
        }

        public int BorderWidth
        {
            get { return this.borderWidth; }
            set
            {
                this.borderWidth = value;
                this.Invalidate();
            }
        }

        public bool Border 
        {
            get { return this.border; }
            set 
            {
                this.border = value;
                this.Invalidate();
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

#region Constructor
        public SkinPanel()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.ResizeRedraw, true);
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
            if (this.BackgroundImage != null || this.BackColor == Color.Transparent)
            {
                DrawEngine.DrawRect2(e.Graphics, this.imageObject, this.ClientRectangle, ControlState.Normal);
            }   
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

            // Draw Border
            if (this.Border)
            {
                using (LinearGradientBrush b = new LinearGradientBrush(
                        new PointF(0, 0), new PointF(0, this.ClientRectangle.Height),
                        this.BorderStartColor,
                        this.BorderEndColor))
                {
                    using (Pen p = new Pen(b, this.borderWidth))
                    {
                        e.Graphics.DrawRectangle(p, new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1));
                    }
                }
            }
        }
#endregion
    }
}
