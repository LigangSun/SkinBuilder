using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public partial class SkinComboBox2DropDown : SkinPopup
    {
        public List<object> Items
        {
            get { return this.comboBox2ListBox.Items; }
        }

        public ComboBox2ListBox ListBox
        {
            get { return this.comboBox2ListBox; }
        }

        public SkinComboBox2DropDown()
        {
            InitializeComponent();
        }

        private void skinListBox1_Click(object sender, EventArgs e)
        {

        }

        public override void Show(Point screenLocation)
        {
            int height = 0;
            for (int i = 0; i<this.comboBox2ListBox.Items.Count; i++)
            {
                height += this.comboBox2ListBox.ItemHeight;
            }
            if (height == 0)
                height = 100;
            this.Width = this.comboBox2ListBox.MaxItemWidth;
            this.Height = height;
            base.Show(screenLocation);
        }
    }
}
