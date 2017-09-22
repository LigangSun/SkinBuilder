using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Health121.SkinBuilder;

namespace SkinEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            SkinManager.Init(null);

            this.testMenuStrip.Renderer = (ToolStripRenderer)SkinManager.Render;
            this.testToolStrip.Renderer = (ToolStripRenderer)SkinManager.Render;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            this.testMenuStrip.Invalidate();
            this.testToolStrip.Invalidate();
            this.defaultControlPanel.Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SkinManager.ColorTable.Save(this.saveFileDialog.FileName);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SkinManager.ColorTable.Load(this.openFileDialog.FileName);

                this.testMenuStrip.Renderer = (ToolStripRenderer)SkinManager.Render;
                this.testToolStrip.Renderer = (ToolStripRenderer)SkinManager.Render;

                foreach (Control ctrl in this.defaultControlPanel.Controls)
                {
                    ctrl.ForeColor = SkinManager.ColorTable.Text;
                    ctrl.Invalidate();
                }
                this.testMenuStrip.Invalidate();
                this.testToolStrip.Invalidate();
                this.defaultControlPanel.Invalidate();
            }
        }

        private void controlTypeTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name == "controlDefaultColorNode" || e.Node.Name == "ribbonButton")
            {
                this.controlPropertyGrid.SelectedObject = SkinManager.ColorTable;
                this.defaultControlPanel.Show();
            }
        }

        private void addFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode skinFormNode = this.controlTypeTreeView.Nodes["skinFormNode"];
            skinFormNode.Nodes.Add("SkinForm1");
        }

        private void addPanelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode skinFormNode = this.controlTypeTreeView.Nodes["skinPanelNode"];
        }

        private void controlPropertyGrid_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void controlPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.testMenuStrip.Renderer = (ToolStripRenderer)SkinManager.Render;
            this.testToolStrip.Renderer = (ToolStripRenderer)SkinManager.Render;

            foreach (Control ctrl in this.defaultControlPanel.Controls)
            {
                ctrl.ForeColor = SkinManager.ColorTable.Text;
                ctrl.Invalidate();
            }
            this.testMenuStrip.Invalidate();
            this.testToolStrip.Invalidate();
            this.defaultControlPanel.Invalidate();
        }

        private void propertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controlPropertyGrid.SelectedObject = this.ribbonButton1;
        }
    }
}