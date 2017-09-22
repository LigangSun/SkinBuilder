using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public partial class SkinComboBox2Button : SkinDropDownButton
    {
        public SkinComboBox2Button()
        {
            InitializeComponent();
        }

        protected override void DrawString(Graphics g, Rectangle rect)
        {
            rect = new Rectangle(rect.Left + 2, rect.Top, rect.Width - 18, rect.Height);
            base.DrawString(g, rect);
        }
    }
}
