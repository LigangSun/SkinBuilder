using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace ZLIS.SkinBuilder
{
    /// <summary>
    /// This is just the start of what you would do if you wanted to draw using 
    /// the theme APIs.
    /// </summary>
    class TabStripSystemRenderer : ToolStripSystemRenderer {

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e) {
            TabStrip tabStrip = e.ToolStrip as TabStrip;
            Tab tab = e.Item as Tab;
            Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);

            if (tab != null && tabStrip != null) {
                System.Windows.Forms.VisualStyles.TabItemState tabState = System.Windows.Forms.VisualStyles.TabItemState.Normal;
                if (tab.Checked) {
                    tabState |= System.Windows.Forms.VisualStyles.TabItemState.Selected;
                }
                if (tab.Selected) {
                    tabState |= System.Windows.Forms.VisualStyles.TabItemState.Hot;
                }
                TabRenderer.DrawTabItem(e.Graphics, bounds, tabState);
            }
            else {
                base.OnRenderButtonBackground(e);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
            base.OnRenderItemText(e);
            Tab tab = e.Item as Tab;
            if (tab != null && tab.Checked) {                
                Rectangle rect = e.TextRectangle;
                ControlPaint.DrawFocusRectangle(e.Graphics, rect);
            }
        }


        
        
    }
}
