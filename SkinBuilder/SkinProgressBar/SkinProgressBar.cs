using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SkinBuilder.SkinProgressBar
{
    public partial class SkinProgressBar : Control
    {
        private Color startColor = Color.Green;
        private Color middleColor = Color.LightGreen;
        private Color endColor = Color.LightGray;

        private Image backgroundImage = null;

        private int value = 0;
        private int maxValue = 100;

        public new Image BackgroundImage
        {
            get { return this.backgroundImage; }
            set { this.backgroundImage = value; }
        }

        public Image ForegroundImage
        {
            get;
            set;
        }

        public Color StartColor
        {
            get { return this.startColor; }
            set { this.startColor = value; }
        }

        public Color MiddleColor
        {
            get { return this.middleColor; }
            set { this.middleColor = value; }
        }

        public Color EndColor
        {
            get { return this.endColor; }
            set { this.endColor = value; }
        }

        public int Value
        {
            get { return this.value; }
            set 
            {
                this.value = value;
                this.Invalidate();
            }
        }

        public int Maximum
        {
            get { return this.maxValue; }
            set
            {
                this.maxValue = value;
                this.Invalidate();
            }
        }

        public int Minimum
        {
            get;
            set;
        }

        public int Step
        {
            get;
            set;
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

        public SkinProgressBar()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void PerformStep()
        {
            this.value += this.Step;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (this.backgroundImage != null)
            {
                g.DrawImage(this.backgroundImage, this.ClientRectangle, 0, 0, this.backgroundImage.Width, this.backgroundImage.Height, GraphicsUnit.Pixel);
            }
            else
            {
                base.InvokePaintBackground(this, e);
            }

            if (this.value == 0)
                return;

            int length = (this.ClientRectangle.Width - 2) * this.Value / this.Maximum;
            if (this.ForegroundImage != null)
            {
                if (length < 1)
                    length = 1;
                Rectangle rect = new Rectangle(1, 1, length, (this.ClientRectangle.Height - 2));
                g.DrawImage(this.ForegroundImage, rect, 0, 0, this.ForegroundImage.Width, this.ForegroundImage.Height, GraphicsUnit.Pixel);
            }
            else
            {
                if (true)
                {
                    if (length < 1)
                        length = 1;
                    Rectangle rect1 = new Rectangle(1, 1, length, (this.ClientRectangle.Height - 2) / 2);
                    Rectangle rect2 = new Rectangle(1, rect1.Bottom - 1, length, (this.ClientRectangle.Height - 2) / 2 + 1);

                    using (LinearGradientBrush brush1 = new LinearGradientBrush(rect1, this.startColor, this.middleColor, 90f))
                    {
                        g.FillRectangle(brush1, rect1);
                    }

                    using (LinearGradientBrush brush2 = new LinearGradientBrush(rect2, this.middleColor, this.endColor, 90f))
                    {
                        g.FillRectangle(brush2, rect2);
                    }
                }
                else
                {
                    if (length < 2)
                        length = 2;
                    Rectangle rect1 = new Rectangle(1, 1, length / 2, this.ClientRectangle.Height - 2);
                    Rectangle rect2 = new Rectangle(rect1.Right - 1, 1, length / 2 - 1, this.ClientRectangle.Height - 2);

                    using (LinearGradientBrush brush1 = new LinearGradientBrush(new Point(rect1.Left, rect1.Top), new Point(rect1.Right, rect1.Top),
                                                                                   this.startColor,
                                                                                   this.middleColor))
                    {
                        g.FillRectangle(brush1, rect1);
                    }

                    using (LinearGradientBrush brush2 = new LinearGradientBrush(new Point(rect2.Left, rect2.Top), new Point(rect2.Right, rect2.Top),
                                                                                   this.middleColor,
                                                                                   this.endColor))
                    {
                        g.FillRectangle(brush2, rect2);
                    }
                }
            }
        }
    }
}
