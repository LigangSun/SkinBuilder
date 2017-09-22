using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ZLIS.SkinControl.Office2007Blue
{
    public class MainMenuSeperator : Button
    {
        #region About Constructor



        public MainMenuSeperator()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.Opaque, false);
            BackColor = Color.Transparent;
        }


        #endregion

        #region About Button Settings

        private int _radius = 2;
        private int maxHeight = 6;
        [Description("the max height of Separator")]
        [DefaultValue(6)]
        public int MaxHeight
        {
            get { return maxHeight; }
            set
            {
                maxHeight = value;
            }
        }

        [Description("the height of Separator")]
        [DefaultValue(4)]
        public int SeparatorHeight
        {
            get { return Height; }
            set
            {
                Height = value;
                if (Height > maxHeight)
                    Height = maxHeight;
            }
        }

        private Color startColor = Color.PaleTurquoise;
        private Color endColor = Color.DarkTurquoise;

        [DefaultValue("PaleTurquoise")]
        public Color StartColor
        {
            get { return startColor; }
            set
            {
                startColor = value;
            }
        }

        [DefaultValue("DarkTurquoise")]
        public Color EndColor
        {
            get { return endColor; }
            set
            {
                endColor = value;
            }
        }

        #endregion

        #region Paint Methods

        private int offsetx = 0, offsety = 0;

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Rectangle rect = new Rectangle(new Point(0,0),new Size(Width,Height));
            LinearGradientBrush lgbrush = new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical);
            GraphicsPath gp = new GraphicsPath();
            gp.AddRectangle(rect);
            gp.CloseFigure();
            
                        
            pevent.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            pevent.Graphics.FillPath(lgbrush,gp);
            Region = new Region(gp);

        }

        protected override void OnResize(EventArgs e)
        {
            if (Height > maxHeight)
                Height = maxHeight;

            Rectangle r = new Rectangle(new Point(-1, -1), new Size(Width + _radius, Height + _radius));
            if (Size != null)
            {
                GraphicsPath pathregion = new GraphicsPath();
                DrawArc(r, pathregion);
                Region = new Region(pathregion);
            }
            base.OnResize(e);
        }


        public void DrawArc(Rectangle re, GraphicsPath pa)
        {
            int _radiusX0Y0 = _radius, _radiusXFY0 = _radius, _radiusX0YF = _radius, _radiusXFYF = _radius;

            pa.AddArc(re.X, re.Y, _radiusX0Y0, _radiusX0Y0, 180, 90);
            pa.AddArc(re.Width - _radiusXFY0, re.Y, _radiusXFY0, _radiusXFY0, 270, 90);
            pa.AddArc(re.Width - _radiusXFYF, re.Height - _radiusXFYF, _radiusXFYF, _radiusXFYF, 0, 90);
            pa.AddArc(re.X, re.Height - _radiusX0YF, _radiusX0YF, _radiusX0YF, 90, 90);
            pa.CloseFigure();
        }

        #endregion
    }
}
