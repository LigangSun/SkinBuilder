using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public partial class SkinPopup : SkinControl
    {
        #region Fields
        private ToolStripDropDown _toolStripDropDown;

        #endregion

        #region Events

        /// <summary>
        /// Raised when the popup is closed
        /// </summary>
        public event EventHandler Closed;

        #endregion

        #region Constructor
        public SkinPopup()
        {
            InitializeComponent();
        }
        #endregion

        #region Props

        /// <summary>
        /// Gets the related ToolStripDropDown
        /// </summary>
        public ToolStripDropDown ToolStripDropDown
        {
            get { return _toolStripDropDown; }
            set { _toolStripDropDown = value; }
        }


        #endregion

        #region Methods

        public virtual void Show(Point screenLocation)
        {
            int width = this.Width;
            int height = this.Height;

            ToolStripControlHost host = new ToolStripControlHost(this);
            ToolStripDropDown = new ToolStripDropDown();

            ToolStripDropDown.AutoSize = false;
            host.AutoSize = false;

            ToolStripDropDown.Width = width;
            ToolStripDropDown.Height = height;
            host.Width = width;
            host.Height = height;

            ToolStripDropDown.Items.Clear();
            ToolStripDropDown.Items.Add(host);

            ToolStripDropDown.Padding = Padding.Empty;
            ToolStripDropDown.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            host.Margin = Padding.Empty;

            ToolStripDropDown.Closed += new ToolStripDropDownClosedEventHandler(ToolStripDropDown_Closed);

            ToolStripDropDown.Show(screenLocation);
        }

        void ToolStripDropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            OnClosed(EventArgs.Empty);
        }

        public void Close()
        {
            if (ToolStripDropDown != null)
            {
                ToolStripDropDown.Close();
            }
        }

        protected virtual void OnClosed(EventArgs e)
        {
            if (Closed != null)
            {
                Closed(this, e);
            }
        }

        #endregion

        #region Shadow

        // Define the CS_DROPSHADOW constant
        private const int CS_DROPSHADOW = 0x00020000;

        // Override the CreateParams property
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }


        #endregion

    }
}
