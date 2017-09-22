using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;

namespace ZLIS.SkinBuilder
{

    /// <summary>
    /// Tab is a specialized ToolStripButton with extra padding
    /// </summary>
    [System.ComponentModel.DesignerCategory("code")]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.None)] // turn off the ability to add this in the DT, the TabPageSwitcher designer will provide this.
    public class Tab : ToolStripButton {
        
        private TabStripPage tabStripPage;

        public bool b_on = false;
        public bool b_selected = false;
        public bool b_active = false;
        public bool b_fading = true;
        public int o_opacity = 180;
        public int e_opacity = 40;
        public int i_opacity;
        private Timer timer = new Timer();
        
        /// <summary>
        /// Constructor for tab - support all overloads.
        /// </summary>
        public Tab() {
            Initialize();
        }
        public Tab(string text):base(text,null,null) {
            Initialize();
        }
        public Tab(Image image):base(null,image,null) {
            Initialize();
        }
        public Tab(string text, Image image):base(text,image,null) {
            Initialize();
        }
        public Tab(string text, Image image, EventHandler onClick):base(text,image,onClick) {
            Initialize();            
        }
        public Tab(string text, Image image, EventHandler onClick, string name):base(text,image,onClick,name) {
            Initialize();
        }

        /// <summary>
        /// Common initialization code between all CTORs.
        /// </summary>
        private void Initialize() {
            this.AutoSize = false;
            this.Width = 60;
            CheckOnClick = true;  // Tab will use the "checked" property in order to represent the "selected tab".
            this.ForeColor = Color.FromArgb(44, 90, 154);
            this.Font = new Font("Segoe UI", 9);
            this.Margin = new Padding(6, this.Margin.Top, this.Margin.Right, this.Margin.Bottom);
            i_opacity = o_opacity;
            timer.Interval = 1;
            timer.Tick += new EventHandler(timer_Tick);
        }

        /// <summary>
        /// Hide the CheckOnClick from the Property Grid so that we can use it for our own purposes. 
        /// </summary>
        [DefaultValue(true)]
        public new bool CheckOnClick {
            get { return base.CheckOnClick; }
            set { base.CheckOnClick = value; }
        }

        /// <summary>
        /// Specify the default display style to be ImageAndText
        /// </summary>
        protected override ToolStripItemDisplayStyle DefaultDisplayStyle {
            get {
                return ToolStripItemDisplayStyle.ImageAndText;
            }
        }
     
        /// <summary>
        /// Add extra internal spacing so we have enough room for our curve.
        /// </summary>
        protected override Padding DefaultPadding {
            get {
                return new Padding(35, 0, 6, 0);
            }
        }

        /// <summary>
        /// The associated TabStripPage - when Tab is clicked, this TabPage will be selected.
        /// </summary>
        [DefaultValue("null")]
        public TabStripPage TabStripPage {
            get {
                return tabStripPage;
            }
            set {
                tabStripPage = value;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            b_on = true; b_fading = true; b_selected = true;
            timer.Start();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {            
            b_on = false; b_fading = true; 
            timer.Start();
            base.OnMouseLeave(e);
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;

                Bitmap bmpdummy = new Bitmap(100,100);
                Graphics g = Graphics.FromImage(bmpdummy);
                float textwidth = g.MeasureString(this.Text, this.Font).Width;
                this.Width = Convert.ToInt16(textwidth) + 26;
            }
        }
        

        private void timer_Tick(object sender, EventArgs e)
        {
            if (b_on)
            {
                if (i_opacity > e_opacity)
                {
                    i_opacity -= 20;
                    this.Invalidate();
                }
                else
                {
                    i_opacity = e_opacity;
                    this.Invalidate();
                    timer.Stop();
                }
            }
            if (!b_on)
            {
                if (i_opacity < o_opacity)
                {
                    i_opacity += 8;
                    this.Invalidate();
                }
                else
                {
                    i_opacity = o_opacity;
                    b_fading = false;
                    this.Invalidate();
                    b_selected = false;
                    timer.Stop();
                }

            }
        }
    }
}
