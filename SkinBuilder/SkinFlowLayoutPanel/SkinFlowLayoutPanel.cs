using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public partial class SkinFlowLayoutPanel : FlowLayoutPanel
    {
        public SkinFlowLayoutPanel()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }
    }
}
