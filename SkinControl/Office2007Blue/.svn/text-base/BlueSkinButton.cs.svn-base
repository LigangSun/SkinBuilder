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
    public partial class BlueSkinButton : SkinButton
    {
        public BlueSkinButton()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (this.DesignMode)
                return;

            this.BackColor = Color.FromArgb(191, 219, 255);
            base.EnableFade = true;
            base.NormalBitmap = Resources.BlueCommonBtn_N;
            base.HighlightBitmap = Resources.BlueCommonBtn_H;
            base.DownBitmap = Resources.BlueCommonBtn_D;
            base.DisableBitmap = new Bitmap(ToolStripRenderer.CreateDisabledImage(Resources.BlueCommonBtn_N));

            base.ImageObject.SplitMargin = new Padding(11);
        }
    }
}
