using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public partial class SkinListBox : ListBox
    {
        public SkinListBox()
        {
            InitializeComponent();

            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (this.Items.Count == 0)
                return;

            e.DrawBackground();
            Brush textBrush = Brushes.Black;

            // Draw the current item text based on the current Font and the custom brush settings.
            e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }
    }
}
