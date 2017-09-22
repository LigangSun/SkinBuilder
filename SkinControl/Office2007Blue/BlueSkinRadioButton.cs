using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZLIS.SkinBuilder;
using ZLIS.SkinControl.Properties;

namespace ZLIS.SkinControl
{
    public partial class BlueSkinRadioButton : SkinRadioButton
    {
        public BlueSkinRadioButton()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (this.DesignMode)
                return;

            base.NormalBitmap = Resources.radiobtn;
            base.HighlightBitmap = Resources.radiobtn_highlight;
            base.DisableBitmap = Resources.radiobtn_disable;
            base.DownBitmap = Resources.press;
            base.CheckedBitmap = Resources.Radio;

            this.Height = 16;
        }
    }
}
