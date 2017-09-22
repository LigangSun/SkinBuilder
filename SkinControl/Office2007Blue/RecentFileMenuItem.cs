using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinControl.Office2007Blue
{
    public partial class RecentFileMenuItem : MainMenuItem
    {
        #region Menu Item Colors

        private Color buttonBaseColor = Color.FromArgb(197, 197, 197);

        private Color buttonBaseStroke = Color.FromArgb(197, 197, 197);
        private Color buttonColorStroke = Color.FromArgb(197, 197, 197);
        private Color buttonOnColor = Color.Orange;
        private Color buttonOnStroke = Color.FromArgb(100, 153, 99, 0);
        private Color buttonPressColor = Color.Tomato;
        private Color buttonPressStroke = Color.FromArgb(100, 76, 29, 21);

        #endregion

        public RecentFileMenuItem()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (this.DesignMode)
                return;

            this.BackColor = this.buttonBaseColor;
            this.ColorBase = this.buttonBaseStroke;
            this.ColorBaseStroke = this.buttonColorStroke;
            this.ColorOn = this.buttonOnColor;
            this.ColorOnStroke = this.buttonOnStroke;
            this.ColorPress = this.buttonPressColor;
            this.ColorPressStroke = this.buttonPressStroke;

            this.ImageLocation = MainMenuItem.e_imagelocation.Left;// = ContentAlignment.MiddleLeft;
            this.UseVisualStyleBackColor = true;
        }
    }
}
