using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public partial class SkinTreeView : TreeView
    {
        private Color selectedColor = SystemColors.ActiveBorder;

        public Color SelectedColor
        {
            get { return this.selectedColor; }
            set { this.selectedColor = value; }
        }

        public SkinTreeView()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

         //   this.DrawMode = TreeViewDrawMode.OwnerDrawAll;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //base.OnPaintBackground(pevent);
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            Graphics g = e.Graphics;

            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                g.FillRectangle(new SolidBrush(this.selectedColor), e.Bounds);

                Rectangle imgRect = e.Node.Bounds;
                if (this.ImageList != null)
                {
                    Image image = this.ImageList.Images[e.Node.ImageIndex];

                    imgRect = new Rectangle(e.Node.Bounds.Left, e.Bounds.Top + (imgRect.Height - image.Height) / 2,
                                            image.Width, image.Height);
                    g.DrawImage(image, imgRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                }

                Rectangle textRect = new Rectangle(imgRect.Right, e.Bounds.Top, e.Bounds.Width - imgRect.Width, e.Bounds.Height);
                StringFormat strFmt = new StringFormat();
                strFmt.Alignment = StringAlignment.Near;
                strFmt.LineAlignment = StringAlignment.Center;
                strFmt.FormatFlags = StringFormatFlags.LineLimit;
                strFmt.Trimming = StringTrimming.EllipsisCharacter;
                g.DrawString(e.Node.Text, this.Font, new SolidBrush(this.ForeColor), textRect, strFmt);
            }
            else
            {
                e.DrawDefault = true;
            }
        }
    }
}
