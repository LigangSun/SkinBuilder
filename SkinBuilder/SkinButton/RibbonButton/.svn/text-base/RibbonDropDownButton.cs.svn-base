using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public partial class RibbonDropDownButton : RibbonButton
    {
        public RibbonDropDownButton()
            : base()
        {
            InitializeComponent();
        }

        #region Members
        private ContextMenuStrip dropDownMenu = null;
        private bool dropDown = false;
        #endregion

        #region Properties
        public ContextMenuStrip DropDownMenu 
        {
            set 
            { 
                if (this.dropDownMenu != null)
                {
                    this.dropDownMenu.Opening -= OnMenuOpening;
                    this.dropDownMenu.Closed -= OnMenuClosed;
                }

                this.dropDownMenu = value;
                if (value != null)
                {
                    this.dropDownMenu.Opening += OnMenuOpening;
                    this.dropDownMenu.Closed += OnMenuClosed;
                }
            }
            get { return this.dropDownMenu; }
        }

        #endregion

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            if (this.Enabled)
            {
                if (this.dropDownMenu != null && !this.dropDown)
                {
                    this.dropDown = true;
                    this.state = ControlState.Down;
                    this.dropDownMenu.Show(this, 0, this.Height);
                }
                else
                {
                    this.state = ControlState.Normal;
                }
            }
            else
            {
                this.state = ControlState.Disable;
            }

            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (this.dropDown)
                return;

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (this.Enabled)
            {
                if (this.dropDownMenu != null && this.dropDown)
                    this.state = ControlState.Down;
                else
                    base.OnMouseLeave(e);
            }
            else
                this.state = ControlState.Disable;

            this.Invalidate();
        }

        private void OnMenuOpening(object sender, CancelEventArgs e)
        {
            this.dropDown = true;
        }

        private void OnMenuClosed(object sender, EventArgs e)
        {
            this.dropDown = false;
            OnMouseLeave(e);
        }
    }
}
