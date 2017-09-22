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
    public partial class BlueRibbonButton : RibbonButton
    {
        public BlueRibbonButton()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (this.DesignMode)
                return;

            this.img_on = Resources.btn_h;
            this.img_click = Resources.btn_d;
            this.BackColor = Color.Transparent;

            this.ImageObject.SplitMargin = new Padding(10, 24, 10, 34);

            if (this.Img != null)
            {
                this.Img_H = this.Img;
                this.Img_D = this.Img;
                this.Img_G = ToolStripRenderer.CreateDisabledImage(this.Img);
            }
        }
    }
}
