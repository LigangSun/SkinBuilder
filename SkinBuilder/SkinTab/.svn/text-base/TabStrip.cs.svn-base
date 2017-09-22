using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace ZLIS.SkinBuilder
{

    [ToolboxItem(typeof(TabStripToolboxItem))]
    

    public partial class TabStrip : ToolStrip
    {
        Font boldFont = new Font(SystemFonts.MenuFont, FontStyle.Bold);
        private const int EXTRA_PADDING = 0;
        
        public TabStrip()
        {
            Renderer = new TabStripProfessionalRenderer();
            this.Padding = new Padding(60, 3, 30, 0);
            this.AutoSize = false;
            this.Size = new Size(this.Width, 26);
            this.BackColor = Color.FromArgb(191, 219, 255);
            this.GripStyle = ToolStripGripStyle.Hidden;

            this.ShowItemToolTips = false;
        }
        protected override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
        {
            return new Tab(text, image, onClick);
        }


        protected override Padding DefaultPadding
        {
            get
            {
                Padding padding = base.DefaultPadding;
                padding.Top += EXTRA_PADDING;
                padding.Bottom += EXTRA_PADDING;

                return padding;
            }
        }

        private Tab currentSelection;

        public Tab SelectedTab
        {
            get { return currentSelection; }
            set
            {
                if (currentSelection != value)
                {
                    currentSelection = value;

                    if (currentSelection != null)
                    {
                        this.SetItemStatus(currentSelection);

                        PerformLayout();
                        if (currentSelection.TabStripPage != null)
                        {
                            currentSelection.TabStripPage.Activate();
                        }
                    }
                }
            }
        }

        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            this.SelectedTab = e.ClickedItem as Tab;

            base.OnItemClicked(e);
        }

        private void SetItemStatus(Tab activeItem)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Tab currentTab = Items[i] as Tab;
                SuspendLayout();
                if (currentTab != null)
                {
                    if (currentTab != activeItem)
                    {
                        currentTab.Checked = false;
                        currentTab.Font = this.Font;
                        currentTab.b_active = false;
                    }
                    else
                    {
                        // currentTab.Font = boldFont;
                        currentTab.b_active = true;
                        currentTab.Checked = true;
                    }
                }
                ResumeLayout();
            }
        }

        protected override void SetDisplayedItems()
        {
            base.SetDisplayedItems();
            for (int i = 0; i < DisplayedItems.Count; i++)
            {
                if (DisplayedItems[i] == SelectedTab)
                {
                    DisplayedItems.Add(SelectedTab);
                    break;
                }
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                Size size = base.DefaultSize;
                // size.Height += EXTRA_PADDING*2;
                return size;
            }
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize = Size.Empty;
            proposedSize -= this.Padding.Size;

            foreach (ToolStripItem item in this.Items)
            {
                preferredSize = LayoutUtils.UnionSizes(preferredSize, item.GetPreferredSize(proposedSize) + item.Padding.Size);
            }
            return preferredSize + this.Padding.Size;
        }

        private int tabOverlap = 0;
        [DefaultValue(10)]
        public int TabOverlap
        {
            get { return tabOverlap; }
            set
            {
                if (tabOverlap != value)
                {
                    tabOverlap = value;
                    // call perform layout so we 
                    PerformLayout();
                }
            }
        }
    }
}
