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
    public partial class BlueSkinComboBox : SkinComboBox2
    {
        public BlueSkinComboBox()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (this.DesignMode)
                return;

            base.NormalBitmap = Resources.BlueComboBoxBtn_N;
            base.HighlightBitmap = Resources.BlueComboBoxBtn_H;
            base.DownBitmap = Resources.BlueComboBoxBtn_D;
            base.DisableBitmap = new Bitmap(ToolStripRenderer.CreateDisabledImage(Resources.BlueComboBoxBtn_N));

            this.ImageObject.SplitMargin = new Padding(5, 5, 18, 5);
            this.Height = 26;
            this.ButtonWidth = 16;
        }
    }
}
