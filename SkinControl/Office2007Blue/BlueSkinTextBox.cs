using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZLIS.SkinBuilder;

namespace ZLIS.SkinControl.Office2007Blue
{
    public partial class BlueSkinTextBox : SkinTextBox
    {
        public BlueSkinTextBox()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (this.DesignMode)
                return;

            this.ActiveColor = Color.White;
            this.BackColor = Color.White;// Color.FromArgb(234, 242, 251);
            this.BorderColor = Color.FromArgb(179, 199, 225);
            this.BorderStyle = BorderStyle.None;

            if (!this.Multiline)
            {
            //    this.Height = 24;
                this.Multiline = false;
                this.TextBox.ScrollBars = ScrollBars.None;
            }
            else
            {
                this.TextBox.ScrollBars = ScrollBars.Both;
                this.TextBox.WordWrap = true;
            }

            if (this.DesignMode)
                return;

            this.ResizeControl();
        }
    }
}
