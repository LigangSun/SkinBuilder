using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public class RibbonPanel : System.Windows.Forms.Panel
    {

        public RibbonPanel()
        {
            this.Padding = new Padding(0, 3, 0, 0);
            timer1.Interval = 1;
            timer1.Tick += new EventHandler(timer1_Tick);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        int X0;
        int XF;
        int Y0;
        int YF;
        int T = 3;
        int i_Zero = 180;
        int i_Sweep = 90;
        int X; int Y;
        GraphicsPath path;
        int D = -1;
        int R0 = 215;
        int G0 = 227;
        int B0 = 242;
        Color _BaseColor = Color.FromArgb(215, 227, 242);
        Color _BaseColorOn = Color.FromArgb(215, 227, 242);
        int i_mode = 0; //0 Entering, 1 Leaving
        int i_factor = 8;
        int i_fR = 1; int i_fG = 1; int i_fB = 1;
        int i_Op = 255;

        string S_TXT = "";

        public Color BaseColor
        {
            get
            {
                return _BaseColor;
            }
            set
            {
                _BaseColor = value;
                R0 = _BaseColor.R;
                B0 = _BaseColor.B;
                G0 = _BaseColor.G;
            }
        }

        public Color BaseColorOn
        {
            get
            {
                return _BaseColorOn;
            }
            set
            {
                _BaseColorOn = value;
                R0 = _BaseColor.R;
                B0 = _BaseColor.B;
                G0 = _BaseColor.G;
            }
        }

        public string Caption
        {
            get
            {
                return S_TXT;
            }
            set
            {
                S_TXT = value;
                this.Refresh();
            }
        }

        public int Speed
        {
            get
            {
                return i_factor;
            }
            set
            {
                i_factor = value;
            }
        }

        public int Opacity
        {
            get
            {
                return i_Op;
            }
            set
            {
                if (value < 256 | value > -1)
                { i_Op = value; }
            }

        }


        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            X0 = 0; XF = this.Width + X0 - 3;
            Y0 = 0; YF = this.Height + Y0 - 3;
            T = 6;
            Point P0 = new Point(X0, Y0 - 1);
            Point PF = new Point(X0, Y0 + YF + 8);
            Pen b1 = new Pen(Color.FromArgb(i_Op, R0 - 18, G0 - 17, B0 - 19));
            Pen b2 = Pens.Black;
            try
            {
                //For Light Colors
                b2 = new Pen(Color.FromArgb(i_Op, R0 - 74, G0 - 49, B0 - 15));
            }
            catch
            {
                //For Dark Colors
                b2 = new Pen(Color.FromArgb(i_Op, R0 - 22, G0 - 11, B0));
            }
            Pen b22 = new Pen(Color.FromArgb(i_Op, R0 + 23, G0 + 21, B0));
            Pen b3 = new Pen(Color.FromArgb(i_Op, R0 + 14, G0 + 9, B0));
            Pen b4 = new Pen(Color.FromArgb(i_Op, R0 - 8, G0 - 4, B0 - 2));
            Pen b5 = new Pen(Color.FromArgb(i_Op, R0+4, G0+3, B0));
            Pen b6 = new Pen(Color.FromArgb(i_Op, R0 - 16, G0 - 11, B0 - 5));
            Pen b8 = new Pen(Color.FromArgb(i_Op, R0 + 12, G0 + 17, B0));
            Pen b7 = new Pen(Color.FromArgb(i_Op, R0 - 22, G0 - 10, B0));

            e.Graphics.PageUnit = GraphicsUnit.Pixel;
            Brush B4 = b4.Brush;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            X = X0; Y = Y0; i_Zero = 180; D = 0;

            DrawArc();
            e.Graphics.FillPath(b5.Brush, path);
            Rectangle rect = e.ClipRectangle;
            //LinearGradientBrush brocha = new LinearGradientBrush(rect, b6.Color, b8.Color,LinearGradientMode.Vertical);
            LinearGradientBrush brocha = new LinearGradientBrush(P0, PF, b6.Color, b8.Color);
            DrawArc2(17, YF + 7);
            e.Graphics.FillPath(brocha, path);
            D--;
            DrawFHalfArc();
            e.Graphics.DrawPath(b2, path);
            DrawSHalfArc();
            e.Graphics.DrawPath(b22, path);

            if (activestate)
            {
                e.Graphics.DrawLine(b5,new Point(activex0+1,0),new Point(activexf-9,0));
            }

            base.OnPaint(e);
        }


        protected override void OnMouseEnter(EventArgs e)
        {
            Point P_EX = Cursor.Position;
            P_EX = this.PointToClient(P_EX);
            if (P_EX.X > 0 | P_EX.X < this.Width | P_EX.Y > 0 | P_EX.Y < this.Height)
            {
                i_mode = 0;
                timer1.Start();
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Point P_EX = Cursor.Position;
            P_EX = this.PointToClient(P_EX);
            if (P_EX.X < 0 | P_EX.X >= this.Width | P_EX.Y < 0 | P_EX.Y >= this.Height)
            {
                i_mode = 1;
                timer1.Start();
            }
            base.OnMouseLeave(e);
        }

        public void DrawArc()
        {
            X = X0 - 2; Y = Y0 - 1; i_Zero = 180; D++;
            path = new GraphicsPath();
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; X += XF;
            path.AddArc(X - D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y += YF;
            path.AddArc(X - D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; X -= XF;
            path.AddArc(X + D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y -= YF;
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep);
        }

        public void DrawFHalfArc()
        {
            X = X0 - 2; Y = Y0 - 1; i_Zero = 180; D++;
            path = new GraphicsPath();
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; X += XF - 1;
            path.AddArc(X - D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y += YF;
            path.AddArc(X - D, Y - D, T, T, i_Zero, i_Sweep);

        }

        public void DrawSHalfArc()
        {
            X = X0 - 3; Y = Y0 - 1; i_Zero = 180; D++;
            path = new GraphicsPath();
            i_Zero += 90; X += XF;
            path.AddArc(X - D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y += YF - 1;
            path.AddArc(X - D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; X -= XF - 1;
            path.AddArc(X + D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y -= YF - 1;
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep);

        }

        protected override void OnResize(EventArgs eventargs)
        {
            this.Refresh();
            base.OnResize(eventargs);
        }

        public void DrawArc2(int OF_Y, int SW_Y)
        {
            X = X0 - 1; Y = Y0 + OF_Y; i_Zero = 180;
            path = new GraphicsPath();
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; X += XF - 1;
            path.AddArc(X - D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y += SW_Y - 20;
            path.AddArc(X - D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; X -= XF - 1;
            path.AddArc(X + D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y -= SW_Y - 20;
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep);
        }

        private Timer timer1 = new Timer();

        void timer1_Tick(object sender, EventArgs e)
        {
            #region Entering
            if (i_mode == 0)
            {
                if (System.Math.Abs(_BaseColorOn.R - R0) > i_factor)
                { i_fR = i_factor; }
                else { i_fR = 1; }
                if (System.Math.Abs(_BaseColorOn.G - G0) > i_factor)
                { i_fG = i_factor; }
                else { i_fG = 1; }
                if (System.Math.Abs(_BaseColorOn.B - B0) > i_factor)
                { i_fB = i_factor; }
                else { i_fB = 1; }

                if (_BaseColorOn.R < R0)
                {
                    R0 -= i_fR;
                }
                else if (_BaseColorOn.R > R0)
                {
                    R0 += i_fR;
                }

                if (_BaseColorOn.G < G0)
                {
                    G0 -= i_fG;
                }
                else if (_BaseColorOn.G > G0)
                {
                    G0 += i_fG;
                }

                if (_BaseColorOn.B < B0)
                {
                    B0 -= i_fB;
                }
                else if (_BaseColorOn.B > B0)
                {
                    B0 += i_fB;
                }

                if (_BaseColorOn == Color.FromArgb(R0, G0, B0))
                {
                    timer1.Stop();
                }
                else
                {
                    this.Refresh();
                }
            }
            #endregion

            #region Leaving
            if (i_mode == 1)
            {
                if (System.Math.Abs(_BaseColor.R - R0) < i_factor)
                { i_fR = 1; }
                else { i_fR = i_factor; }
                if (System.Math.Abs(_BaseColor.G - G0) < i_factor)
                { i_fG = 1; }
                else { i_fG = i_factor; }
                if (System.Math.Abs(_BaseColor.B - B0) < i_factor)
                { i_fB = 1; }
                else { i_fB = i_factor; }

                if (_BaseColor.R < R0)
                {
                    R0 -= i_fR;
                }
                else if (_BaseColor.R > R0)
                {
                    R0 += i_fR;
                }
                if (_BaseColor.G < G0)
                {
                    G0 -= i_fG;
                }
                else if (_BaseColor.G > G0)
                {
                    G0 += i_fG;
                }
                if (_BaseColor.B < B0)
                {
                    B0 -= i_fB;
                }
                else if (_BaseColor.B > B0)
                {
                    B0 += i_fB;
                }

                if (_BaseColor == Color.FromArgb(R0, G0, B0))
                {
                    timer1.Stop();
                }
                else
                {
                    this.Refresh();
                }

            }
            #endregion

        }

        private int activex0 = 0;
        private int activexf = 0;
        private bool activestate = false;
        public void LinePos(int x0,int xf,bool state)
        {
            activex0 = x0; activexf = xf; activestate = state;
            this.Refresh();
        }
    }
}