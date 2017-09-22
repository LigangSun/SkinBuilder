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
    public partial class BlueHeader : SkinPanel
    {
        public string Header
        {
            get
            {
                return this.textLabel.Text;
            }
            set
            {
                this.textLabel.Text = value;
            }
        }

        public BlueHeader()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (this.DesignMode)
                return;

            this.BackgroundImage = Resources.ToolTitleBK;
            this.ImageObject.SplitMargin = new Padding(2);
            this.Height = 24;
            this.ForeColor = Color.Black;
            this.Font = new Font("TohomaI", 10f, FontStyle.Regular);

            this.textLabel.Parent = this;
            this.textLabel.BackColor = Color.Transparent;
            this.textLabel.Location = new Point(2, (this.Height - this.textLabel.Height) / 2);
            this.textLabel.Visible = true;
        }
    }
}
