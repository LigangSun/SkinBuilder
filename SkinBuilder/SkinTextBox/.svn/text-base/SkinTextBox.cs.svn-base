using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public partial class SkinTextBox : UserControl
    {
        private Color borderColor = Color.Black;
        private Color activeColor = Color.White;
        private Color backColorBackup = Color.White;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler TextChanged;

        public Color  BorderColor 
        {
            get { return this.borderColor; }
            set
            {
                this.borderColor = value;
                this.ResizeControl();
                this.Invalidate();
            }
        }

        public Color ActiveColor
        {
            get { return this.activeColor; }
            set 
            {
                this.activeColor = value;
                this.Invalidate();
            }
        }

        public new Color BackColor
        {
            set
            {
                base.BackColor = value;
                this.backColorBackup = value;
                this.textBox.BackColor = value;
            }
            get
            {
                return base.BackColor;
            }
        }

        public bool Multiline 
        {
            get { return this.textBox.Multiline; }
            set { this.textBox.Multiline = value; }
        }

        public TextBox TextBox
        {
            get { return this.textBox; }
        }

        public bool UseSystemPasswordChar
        {
            get { return this.textBox.UseSystemPasswordChar; }
            set { this.textBox.UseSystemPasswordChar = value; }
        }

        public new string Text
        {
            get { return this.textBox.Text; }
            set { this.textBox.Text = value; }
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

        public SkinTextBox()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            this.BorderStyle = BorderStyle.None;
            this.ResizeControl();

            this.textBox.LostFocus += textBox_LostFocus;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            this.ResizeControl();
        }

        public void ResizeControl()
        {
            if (!this.textBox.Multiline)
            {
                Point position = new Point(1, 1);
                if (!this.AutoSize)
                {
                    position.Y = (this.Height - this.textBox.Height) / 2;
                }
                else
                {
                    if (this.AutoSize)
                        this.Height = this.textBox.Height - 2;
                }

                this.textBox.Location = position;
                this.textBox.Width = this.Width - 3;
            }
            else
            {
                this.textBox.Location = new Point(1, 1);
                this.textBox.Height = this.Height - 2;
                this.textBox.Width = this.Width - 3;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, this.borderColor, ButtonBorderStyle.Solid);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.ResizeControl();
        }

        private void textBox_MouseLeave(object sender, EventArgs e)
        {
            if (!this.textBox.Focused)
            {
                this.textBox.BackColor = this.backColorBackup;
                base.BackColor = this.backColorBackup;
            }
        }

        private void textBox_MouseMove(object sender, MouseEventArgs e)
        {
            this.textBox.BackColor = this.activeColor;
            base.BackColor = this.activeColor;
        }

        private void textBox_LostFocus(object sender, EventArgs e)
        {
            this.textBox.BackColor = this.backColorBackup;
            base.BackColor = this.backColorBackup;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (this.TextChanged != null)
                this.TextChanged(sender, e);
        }
    }
}
