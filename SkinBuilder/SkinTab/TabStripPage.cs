using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    [ToolboxItem(false)] // dont show up in the toolbox, this will be created by the Add TabStripPage verb on the TabPageSwitcherDesigner
    [System.ComponentModel.DesignerCategory("Code")] // dont bring up the component designer when opened
    public class TabStripPage : RibbonPanel 
    {
        public TabStripPage() 
        {
        }


        /// <summary>
        /// Bring this TabStripPage to the front of the switcher.
        /// </summary>
        public void Activate() 
        {
            TabPageSwitcher tabPageSwitcher = this.Parent as TabPageSwitcher;
            if (tabPageSwitcher != null) {
                tabPageSwitcher.SelectedTabStripPage = this;

                try
                {
                    int x0 = tabPageSwitcher.TabStrip.SelectedTab.Bounds.Location.X;
                    int xf = tabPageSwitcher.TabStrip.SelectedTab.Bounds.Right;
                 //   tabPageSwitcher.SelectedTabStripPage.LinePos(x0, xf, true);
                }
                catch { }
            }
            
        }
    }
}
