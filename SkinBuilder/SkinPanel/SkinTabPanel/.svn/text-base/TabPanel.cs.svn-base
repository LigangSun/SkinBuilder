using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public class TabPanel : System.Windows.Forms.Panel
    {
        public TabPanel()
        {
            timer1.Interval = 1;
            timer1.Tick += new EventHandler(timer1_Tick);

            this.mouseCheckerTimer.Interval = 1;
            mouseCheckerTimer.Tick += new EventHandler(mouseCheckerTimer_Tick);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        int X0;
        int XF;
        int Y0;
        int YF;
        int T = 8;
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
        int i_factor = 1;
        int i_fR = 1; int i_fG = 1; int i_fB = 1;
        int i_Op = 255;
        private Timer mouseCheckerTimer = new Timer();
        private System.ComponentModel.IContainer components;

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

            Point P0 = new Point(X0, Y0);
            Point PF = new Point(X0, Y0 + YF);

            Pen b1 = new Pen(Color.FromArgb(i_Op, R0 - 18, G0 - 17, B0 - 19));
            Pen b2 = new Pen(Color.FromArgb(i_Op, R0 - 39, G0 - 24, B0 - 3));
            Pen b3 = new Pen(Color.FromArgb(i_Op, R0 + 11, G0 + 9, B0));
            Pen b4 = new Pen(Color.FromArgb(i_Op, R0 - 8, G0 - 4, B0 - 2));
            Pen b5 = new Pen(Color.FromArgb(i_Op, R0, G0, B0));
            Pen b6 = new Pen(Color.FromArgb(i_Op, R0 - 16, G0 - 11, B0 - 5));
            Pen b8 = new Pen(Color.FromArgb(i_Op, R0 + 1, G0 + +5, B0));
            Pen b7 = new Pen(Color.FromArgb(i_Op, R0 - 22, G0 - 10, B0));

            T = 1;
            DrawArc3(0, 18);
            e.Graphics.PageUnit = GraphicsUnit.Pixel;
            Brush B4 = b4.Brush;
            e.Graphics.SmoothingMode = SmoothingMode.None;
            X = X0; Y = Y0; i_Zero = 180; D = 0;

            e.Graphics.FillPath(b5.Brush, path);
            LinearGradientBrush brocha = new LinearGradientBrush(P0, PF, b6.Color, b8.Color);
            DrawArc2(15, YF - 18);
            e.Graphics.FillPath(brocha, path);
            DrawArc2(YF - 18, 16);
            Pen bdown = new Pen(Color.FromArgb(i_Op, R0 - 22, G0 - 11, B0));
            e.Graphics.FillPath(bdown.Brush, path);

            T = 6;
            DrawArc();
            DrawArc();
            e.Graphics.DrawPath(b2, path);
            DrawArc();
            e.Graphics.DrawPath(b3, path);


            Point P_EX = Cursor.Position;
            P_EX = this.PointToClient(P_EX);

            SizeF txtSize = e.Graphics.MeasureString(S_TXT, this.Font);

            int ix = this.Width / 2 - (int)txtSize.Width / 2;
            int iy = this.Height - (int)Math.Round(txtSize.Height + 4f);
            PointF P_TXT = new PointF(ix, iy);
            Pen pen = new Pen(this.ForeColor);
            e.Graphics.DrawString(S_TXT, this.Font, pen.Brush, P_TXT);

            base.OnPaint(e);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            e.Control.MouseEnter += OnControlMouseEnter;
            e.Control.MouseLeave += OnControlMouseLeave;
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            e.Control.MouseEnter -= OnControlMouseEnter;
            e.Control.MouseLeave -= OnControlMouseLeave;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            OnControlMouseEnter(this, e);

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            OnControlMouseLeave(this, e);

            base.OnMouseLeave(e);
        }

        private void OnControlMouseEnter(object sender, EventArgs e)
        {
            Point P_EX = Cursor.Position;
            P_EX = this.PointToClient(P_EX);
            if (P_EX.X > 0 | P_EX.X < this.Width | P_EX.Y > 0 | P_EX.Y < this.Height)
            {
                this.mouseCheckerTimer.Start();
                timer1.Stop();
                i_mode = 0;
                timer1.Start();
            }
        }

        private void OnControlMouseLeave(object sender, EventArgs e)
        {
            Point P_EX = Cursor.Position;
            P_EX = this.PointToClient(P_EX);
            if (P_EX.X < 0 | P_EX.X >= this.Width | P_EX.Y < 0 | P_EX.Y >= this.Height)
            {
                timer1.Stop();
                i_mode = 1;
                timer1.Start();
            }
        }

        public void DrawArc()
        {
            X = X0; Y = Y0; i_Zero = 180; D++;
            path = new GraphicsPath();
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; X += XF - 6;
            path.AddArc(X - D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y += YF - 6;
            path.AddArc(X - D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; X -= XF - 6;
            path.AddArc(X + D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y -= YF - 6;
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep);
        }

        public void DrawArc2(int OF_Y, int SW_Y)
        {
            X = X0 + 4; Y = Y0 + OF_Y; i_Zero = 180;
            path = new GraphicsPath();
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; X += XF - 8;
            path.AddArc(X - D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y += SW_Y;
            path.AddArc(X - D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; X -= XF - 8;
            path.AddArc(X + D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y -= SW_Y;
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep);
        }

        public void DrawArc3(int OF_Y, int SW_Y)
        {
            X = X0; Y = Y0 + OF_Y; i_Zero = 180;
            path = new GraphicsPath();
            path.AddArc(X + D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; X += XF;
            path.AddArc(X - D, Y + D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y += SW_Y;
            path.AddArc(X - D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; X -= XF;
            path.AddArc(X + D, Y - D, T, T, i_Zero, i_Sweep); i_Zero += 90; Y -= SW_Y;
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
                    this.Refresh();
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
                    this.Refresh();
                }
                else
                {
                    this.Refresh();
                }

            }
            #endregion
        }

        protected override void OnResize(EventArgs eventargs)
        {
            this.Refresh();
            base.OnResize(eventargs);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mouseCheckerTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // mouseCheckerTimer
            // 
            this.mouseCheckerTimer.Enabled = true;
            this.mouseCheckerTimer.Interval = 1;
            this.mouseCheckerTimer.Tick += new System.EventHandler(this.mouseCheckerTimer_Tick);
            this.ResumeLayout(false);

        }

        private void mouseCheckerTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Point P_EX = Cursor.Position;
                P_EX = this.PointToClient(P_EX);
                if (P_EX.X < 0 | P_EX.X >= this.Width | P_EX.Y < 0 | P_EX.Y >= this.Height)
                {
                    this.mouseCheckerTimer.Stop();
                    i_mode = 1;
                    timer1.Start();
                }
            }
            catch (System.Exception ec)
            {
            	
            }
        }
    }
}
