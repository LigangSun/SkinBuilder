using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Reflection;

namespace ZLIS.SkinBuilder
{
    public partial class ComboBox2ListBox : UserControl
    {
        private SkinComboBox2 comboBox = null;

        private Color startColor = Color.FromArgb(255, 255, 251);
        private Color middleColor = Color.FromArgb(255, 216, 114);
        private Color endColor = Color.FromArgb(255, 248, 181);
        private Color borderColor = Color.FromArgb(219, 206, 153);

        private int highlightIndex = -1;
        private int selectedIndex = -1;

        private List<object> items = new List<object>();

        private int itemHeight = 24;

        private string displayMember = string.Empty;

        public string DisplayMember
        {
            set { this.displayMember = value; }
            get { return this.displayMember; }
        }

        public SkinComboBox2 ComboBox
        {
            set { this.comboBox = value; }
        }

        public List<object> Items
        {
            get { return this.items; }
        }

        public int ItemHeight
        {
            get { return this.itemHeight; }
            set { this.itemHeight = value; }
        }

        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            set
            { 
                this.selectedIndex = value;
                if (this.comboBox != null)
                    this.comboBox.ListBoxSelectedChanged(this.SelectedText);
            }
        }

        public object SelectedItem
        {
            get 
            {
                if (this.selectedIndex == -1)
                    return null;

                if (this.items.Count == 0)
                    return null;

                return this.items[this.selectedIndex];
            }
            set
            {
                for (int i = 0; i < this.items.Count; i++)
                {
                    if (this.items[i].Equals(value))
                    {
                        this.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        public string SelectedText
        {
            get
            {
                object selectedObject = this.SelectedItem;
                if (selectedObject == null)
                    return string.Empty;

                return GetString(selectedObject);
            }
        }

        public int MaxItemWidth
        {
            get
            {
                Graphics g = Graphics.FromHwnd(this.Handle);
                int width = this.Width;
                foreach (object item in this.items)
                {
                    int temp = (int)Math.Round(g.MeasureString(this.GetString(item), this.comboBox.Font).Width + 5.0f);
                    if (temp > width)
                        width = temp;
                }

                return width;
            }
        }

        public ComboBox2ListBox()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        private string GetString(Object item)
        {
            if (item is string)
                return item as string;

            Type type = item.GetType();
            PropertyInfo propertyInfo = type.GetProperty(this.displayMember);
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(item, null).ToString();
            }

            return item.ToString();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Point pt = new Point(e.X, e.Y);
            int top = 0;
            for (int i = 0; i<this.Items.Count; i++)
            {
                Rectangle rect = new Rectangle(0, top, this.Width, this.ItemHeight);
                if (rect.Contains(pt))
                {
                    this.highlightIndex = i;
                    this.Invalidate();
                    break;
                }
                top += this.itemHeight;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this.highlightIndex = -1;
            this.Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            Point pt = new Point(e.X, e.Y);
            int top = 0;
            int oldSelectedIndex = this.selectedIndex;
            for (int i = 0; i < this.Items.Count; i++)
            {
                Rectangle rect = new Rectangle(0, top, this.Width, this.ItemHeight);
                if (rect.Contains(pt))
                {
                    this.selectedIndex = i;
                    this.Invalidate();
                    break;
                }
                top += this.itemHeight;
            }

            if (this.comboBox != null)
                this.comboBox.ListBoxSelectedChanged(this.SelectedText);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            StringFormat strFmt = new StringFormat();
            strFmt.Alignment = StringAlignment.Near;
            strFmt.LineAlignment = StringAlignment.Center;
            strFmt.FormatFlags = StringFormatFlags.LineLimit;
            int top = 0;
            for (int i = 0; i < this.items.Count; i++)
            {
                Rectangle itemRect = new Rectangle(2, top, this.Width - 4, this.itemHeight);
                if (this.highlightIndex == i ||
                    this.SelectedIndex == i)
                {
                    Rectangle rectTop = new Rectangle(itemRect.Left, itemRect.Top, itemRect.Width, itemRect.Height / 2 + 1);
                    Rectangle rectBottom = new Rectangle(itemRect.Left, itemRect.Top + itemRect.Height / 2, itemRect.Width, itemRect.Height / 2 + 1);
                    using (LinearGradientBrush linearBrush1 = new LinearGradientBrush(rectTop, startColor, middleColor, 90),
                                               linearBrush2 = new LinearGradientBrush(rectBottom, middleColor, endColor, 90))
                    {
                        e.Graphics.FillRectangle(linearBrush1, rectTop);
                        e.Graphics.FillRectangle(linearBrush2, rectBottom);

                        e.Graphics.DrawString(this.GetString(this.Items[i]), this.comboBox.Font, new SolidBrush(this.ForeColor), itemRect, strFmt);

                        e.Graphics.DrawRectangle(new Pen(borderColor), itemRect);
                    }
                }
                else
                {
                    Brush textBrush = Brushes.Black;

                    // Draw the current item text based on the current Font and the custom brush settings.
                    e.Graphics.DrawString(this.GetString(this.Items[i]), this.comboBox.Font, textBrush, itemRect, strFmt);
                }

                top += itemHeight;
            }

            Rectangle borderRect = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
            e.Graphics.DrawRectangle(new Pen(this.borderColor), borderRect);
        }
    }
}
