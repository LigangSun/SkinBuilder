using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public partial class SkinComboBox2 : UserControl
    {
        #region Members

        private bool fixedHeight = false;
        private ImageObject imgObj = new ImageObject();
        private SkinComboBox2DropDown skinDropDown = new SkinComboBox2DropDown();
        private ComboBoxStyle comboBoxStyle = ComboBoxStyle.DropDownList;
        //    private TextBox textBox = null;

        private bool dropDown = false;

        private ControlState state = ControlState.Normal;

        private int buttonWidth = 20;

        private ToolTip toolTip = null;

        #endregion Members

        #region Events
        public event EventHandler<EventArgs> SelecctedChangedEvent;
        public event EventHandler SelectedIndexChanged;
        #endregion Events

        #region Properties

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

        public new string Text
        {
            set
            {
                if (this.textBox != null)
                {
                    this.textBox.Text = value;
                    this.Invalidate();
                }
                base.Text = value;

                if (!string.IsNullOrEmpty(value))
                {
                    Graphics g = this.CreateGraphics();
                    SizeF textSize = g.MeasureString(value, this.Font);

                    if (this.toolTip == null)
                    {
                        this.toolTip = new ToolTip();
                    }

                    this.toolTip.SetToolTip(this, value);

                    if (Math.Round(textSize.Width + 1) > this.Width - this.buttonWidth - 4)
                    {
                        this.toolTip.IsBalloon = false;
                        this.toolTip.InitialDelay = 300;
                        this.toolTip.AutoPopDelay = 5000;
                        this.toolTip.ShowAlways = true;
                        this.toolTip.Active = true;
                    }
                    else
                    {
                        this.toolTip.Active = false;
                    }
                }
            }
            get
            {
                if (this.textBox != null)
                {
                    return this.textBox.Text;
                }

                return base.Text;
            }
        }

        public bool FixedHeight
        {
            get { return this.fixedHeight; }
            set
            {
                this.fixedHeight = value;
                if (value)
                    this.Height = 20;
            }
        }

        public Bitmap NormalBitmap
        {
            get
            {
                return this.imgObj.NormalBitmap;
            }
            set
            {
                this.imgObj.NormalBitmap = value;
            }
        }

        public Bitmap HighlightBitmap
        {
            get { return this.imgObj.HighlightBitmap; }
            set { this.imgObj.HighlightBitmap = value; }
        }

        public Bitmap DownBitmap
        {
            get { return this.imgObj.DownBitmap; }
            set { this.imgObj.DownBitmap = value; }
        }

        public Bitmap DisableBitmap
        {
            get { return this.imgObj.DisableBitmap; }
            set { this.imgObj.DisableBitmap = value; }
        }

        public ImageObject ImageObject
        {
            get { return this.imgObj; }
            set { this.imgObj = value; }
        }

        public List<object> Items
        {
            get { return this.skinDropDown.Items; }
        }

        public string DisplayMember
        {
            get { return this.skinDropDown.ListBox.DisplayMember; }
            set { this.skinDropDown.ListBox.DisplayMember = value; }
        }

        public object SelectedItem
        {
            get { return this.skinDropDown.ListBox.SelectedItem; }
            set
            {
                this.skinDropDown.ListBox.SelectedItem = value;
                this.Text = this.skinDropDown.ListBox.SelectedText;
                if (this.textBox != null)
                    this.textBox.Text = this.Text;
                this.Invalidate();
            }
        }

        public int SelectedIndex
        {
            get { return this.skinDropDown.ListBox.SelectedIndex; }
            set { this.skinDropDown.ListBox.SelectedIndex = value; }
        }

        public ComboBoxStyle ComboBoxStyle
        {
            get { return this.comboBoxStyle; }
            set { this.comboBoxStyle = value; }
        }

        public ComboBoxStyle DropDownStyle
        {
            get { return this.comboBoxStyle; }
            set { this.comboBoxStyle = value; }
        }

        public int ButtonWidth
        {
            get { return this.buttonWidth; }
            set
            {
                this.buttonWidth = value;
                this.ResizeTextBox();
                this.Invalidate();
            }
        }

        public SkinTextBox TextBox
        {
            get { return this.textBox; }
            set { this.textBox = value; }
        }

        #endregion

        public SkinComboBox2()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.skinDropDown.ListBox.ComboBox = this;

            if (this.fixedHeight)
            {
                this.Height = 20;
            }

            this.skinDropDown.Width = this.Width;
            this.skinDropDown.Height = 100;

            this.ResizeTextBox();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            this.skinDropDown.Width = this.ClientRectangle.Width;
            //this.ResizeTextBox();
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);

            this.ResizeTextBox();
        }

        private void ResizeTextBox()
        {
            if (this.ImageObject == null)
                return;

            if (this.comboBoxStyle == ComboBoxStyle.DropDown)
            {
                if (this.textBox == null)
                    this.textBox = new SkinTextBox();

                this.textPaanel.Visible = true;
                this.textPaanel.Location = new Point(1, 1);
                this.textPaanel.Size = new Size(this.ClientRectangle.Width - this.buttonWidth, this.Height - 2);
                this.textPaanel.BorderStyle = BorderStyle.None;

                int x = 0;
                this.textBox.AutoSize = false;
                int y = 0; //(this.textPaanel.Height - this.textBox.Height) / 2;
                this.textBox.Location = new Point(x, y);
                this.textBox.Width = this.textPaanel.Width;
                this.textBox.Height = this.textPaanel.Height;
                this.textBox.BackColor = this.BackColor;
                this.textBox.BorderStyle = BorderStyle.None;
                this.textBox.BorderColor = Color.Transparent;
            }
            else
            {
                this.textPaanel.Visible = false;
            }
        }

        public void ListBoxSelectedChanged(object selectedObj)
        {
            this.Text = selectedObj as string;
            this.skinDropDown.Close();

            if (this.SelecctedChangedEvent != null)
                this.SelecctedChangedEvent(this, new EventArgs());

            if (this.SelectedIndexChanged != null)
                this.SelectedIndexChanged(this, new EventArgs());
        }

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

            Point pt = Cursor.Position;
            pt = this.PointToClient(pt);

            if (this.Enabled)
            {
                if (this.skinDropDown != null && this.dropDown)
                    this.state = ControlState.Down;
                else
                {
                    this.state = ControlState.Normal;
                }
            }
            else
                this.state = ControlState.Disable;

            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (this.Enabled)
            {
                if (this.skinDropDown != null)
                {
                    this.state = ControlState.Down;
                    Point pt = this.PointToScreen(new Point(0, this.Height));
                    this.skinDropDown.Location = pt;
                    this.skinDropDown.Show(pt);
                    this.skinDropDown.BringToFront();
                    this.BringToFront();
                }
                else
                {
                    this.state = ControlState.Normal;
                }
            }
            else
            {
                this.state = ControlState.Disable;
            }

            this.Invalidate();
        }

        private void OnMenuVisibleChanged(object sender, EventArgs e)
        {
            if (!this.skinDropDown.Visible)
            {
                this.dropDown = false;
                this.state = ControlState.Normal;
                this.Invalidate();
            }
            else
            {
                this.dropDown = true;
            }
        }

        private void OnMenuLostFocus(object sender, EventArgs e)
        {
            this.skinDropDown.Visible = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.ResizeTextBox();

            this.DrawImage(e.Graphics, this.ClientRectangle);

            Rectangle textRect = this.ClientRectangle;
            textRect.Width -= this.buttonWidth;
            this.DrawString(e.Graphics, textRect);
        }

        protected void DrawImage(Graphics g, Rectangle rect)
        {
            DrawEngine.DrawRect2(g, this.imgObj, rect, state);

            // Fill the background with the edit box background color
            if (this.comboBoxStyle == ComboBoxStyle.DropDown)
            {
                Rectangle editRect = new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - this.buttonWidth - 2, rect.Height - 2);
                g.FillRectangle(new SolidBrush(this.textBox.BackColor), editRect);
            }
        }

        protected void DrawString(Graphics g, Rectangle rect)
        {
            StringFormat strFmt = new StringFormat();
            strFmt.Alignment = StringAlignment.Near;
            strFmt.LineAlignment = StringAlignment.Center;
            strFmt.Trimming = StringTrimming.EllipsisCharacter;
            strFmt.FormatFlags = StringFormatFlags.LineLimit;
            g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), rect, strFmt);
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_BackColorChanged(object sender, EventArgs e)
        {
        }
    }
}
