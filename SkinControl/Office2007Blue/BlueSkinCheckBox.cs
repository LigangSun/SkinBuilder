using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZLIS.SkinBuilder;
using ZLIS.SkinControl.Properties;

namespace ZLIS.SkinControl.Office2007Blue
{
    public partial class BlueSkinCheckBox : SkinCheckBox
    {
        public BlueSkinCheckBox()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (this.DesignMode)
                return;

            this.ForeColor = Color.Black;
            base.NormalBitmap = Resources.checkbox;
            base.HighlightBitmap = Resources.checkbox_highlight1;
            base.DisableBitmap = Resources.checkbox_disable1;
            base.DownBitmap = Resources.checkbox_press1;
            base.CheckedBitmap = Resources.Check;

            this.Height = 16;
        }
    }
}
