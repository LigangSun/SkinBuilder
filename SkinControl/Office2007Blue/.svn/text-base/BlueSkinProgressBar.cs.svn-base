using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SkinBuilder.SkinProgressBar;
using ZLIS.SkinControl.Properties;

namespace ZLIS.SkinControl.Office2007Blue
{
    public partial class BlueSkinProgressBar : SkinProgressBar
    {
        public BlueSkinProgressBar()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (this.DesignMode)
                return;

            this.BackgroundImage = Resources.BlueProgressBar_BK;
            this.ForegroundImage = Resources.BlueProgressBar;
        }
    }
}
