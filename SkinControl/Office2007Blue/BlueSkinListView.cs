using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZLIS.SkinBuilder;
using System.Drawing.Drawing2D;
using ZLIS.SkinControl.Properties;

namespace ZLIS.SkinControl.Office2007Blue
{
    public partial class BlueSkinListView : SkinListView
    {
        protected Font boldFont = null;

        protected Color startColor = Color.FromArgb(255, 255, 251);
        protected Color middleColor = Color.FromArgb(255, 216, 114);
        protected Color endColor = Color.FromArgb(255, 248, 181);
        protected Color borderColor = Color.FromArgb(219, 206, 153);

        public BlueSkinListView()
        {
            InitializeComponent();

            this.theme.GroupHeaderHeight = 28;
            this.theme.ItemSize = new Size(0, 28);
            this.theme.BackgroundSplitMargin = new Padding(35);
            this.theme.ArrowFoldImage = Resources.plus;
            this.theme.ArrowUnFoldImage = Resources.minus;
            this.theme.ItemBKHighlightImage = Resources.item_hover;
            this.theme.ItemBKSelectedImage = Resources.item_selected;
            this.theme.ItemBKSplitMargin = new Padding(5, 11, 5, 13);
            this.theme.GroupHeaderHighlightImage = Resources.item_hover;
            this.theme.GroupHeaderSelectedImage = Resources.item_selected;
            this.theme.GroupHeaderSplitMargin = new Padding(5, 11, 5, 13);

            this.ShowSelectAlways = true;
        }

        protected override void DrawItemBackground(Graphics g, Rectangle rect, SkinListViewItem item)
        {
            base.DrawItemBackground(g, rect, item);
            return;
            ControlState state = item.State;
            if (item == this.SelectedItem)
                state = ControlState.Checked;
            if (item == this.HighlightItem)
                state = ControlState.Highlight;

            Rectangle itemRect = rect;
            itemRect.X = 0;
            itemRect.Width = this.ClientRectangle.Width - this.GetVScrollBarWidth();

            Rectangle rectTop = new Rectangle(itemRect.Left, itemRect.Top, itemRect.Width, itemRect.Height / 2);
            Rectangle rectBottom = new Rectangle(itemRect.Left, itemRect.Top + itemRect.Height / 2 - 1, itemRect.Width, itemRect.Height / 2 + 1);
            if (state == ControlState.Highlight)
            {
                using (LinearGradientBrush linearBrush1 = new LinearGradientBrush(rectTop, startColor, middleColor, 90),
                                           linearBrush2 = new LinearGradientBrush(rectBottom, middleColor, endColor, 90))
                {
                    g.FillRectangle(linearBrush1, rectTop);
                    g.FillRectangle(linearBrush2, rectBottom);

                    g.DrawRectangle(new Pen(borderColor), itemRect);
                }
            }
            else if (state == ControlState.Checked)
            {
                using (LinearGradientBrush linearBrush1 = new LinearGradientBrush(rectTop, ControlPaint.Dark(startColor), ControlPaint.Dark(middleColor), 90),
                                          linearBrush2 = new LinearGradientBrush(rectBottom, ControlPaint.Dark(middleColor), ControlPaint.Dark(endColor), 90))
                {
                    g.FillRectangle(linearBrush1, rectTop);
                    g.FillRectangle(linearBrush2, rectBottom);

                    g.DrawRectangle(new Pen(borderColor), itemRect);
                }
            }
            else
            {

            }
        }

        protected override void DrawGroupBackground(Graphics g, Rectangle rect, SkinListViewGroup group)
        {
            base.DrawGroupBackground(g, rect, group);
            return;

            ControlState state = group.State;
            if (group == this.SelectedItem)
                state = ControlState.Checked;
            if (group == this.HighlightItem)
                state = ControlState.Highlight;

            Rectangle itemRect = rect;
            itemRect.X = 0;
            itemRect.Width = this.ClientRectangle.Width - this.GetVScrollBarWidth();

            Rectangle rectTop = new Rectangle(itemRect.Left, itemRect.Top, itemRect.Width, itemRect.Height / 2);
            Rectangle rectBottom = new Rectangle(itemRect.Left, itemRect.Top + itemRect.Height / 2 - 1, itemRect.Width, itemRect.Height / 2 + 1);
            if (state == ControlState.Highlight)
            {
                using (LinearGradientBrush linearBrush1 = new LinearGradientBrush(rectTop, startColor, middleColor, 90),
                                           linearBrush2 = new LinearGradientBrush(rectBottom, middleColor, endColor, 90))
                {
                    g.FillRectangle(linearBrush1, rectTop);
                    g.FillRectangle(linearBrush2, rectBottom);

                    g.DrawRectangle(new Pen(borderColor), itemRect);
                }
            }
            else if (state == ControlState.Checked)
            {
                using (LinearGradientBrush linearBrush1 = new LinearGradientBrush(rectTop, ControlPaint.Dark(startColor), ControlPaint.Dark(middleColor), 90),
                                          linearBrush2 = new LinearGradientBrush(rectBottom, ControlPaint.Dark(middleColor), ControlPaint.Dark(endColor), 90))
                {
                    g.FillRectangle(linearBrush1, rectTop);
                    g.FillRectangle(linearBrush2, rectBottom);

                    g.DrawRectangle(new Pen(borderColor), itemRect);
                }
            }
            else
            {

            }
        }

        protected override void DrawItemContent(Graphics g, Rectangle rect, SkinListViewItem item)
        {
            // Draw Item Image
            Rectangle imgRect = this.DrawItemImage(g, rect, item);

            Rectangle itemRect = rect;
            itemRect.X = imgRect.Right + 5;
            if (imgRect == Rectangle.Empty)
                itemRect.X = rect.Left;
            itemRect.Width = rect.Width - imgRect.Width - 5;

            StringFormat strFmt = new StringFormat();
            strFmt.Alignment = StringAlignment.Near;
            strFmt.LineAlignment = StringAlignment.Center;
            strFmt.Trimming = StringTrimming.EllipsisWord;
            strFmt.FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap;

            Color textColor = item.TextColor;

            Font textFont = item.TextFont;
            if (textFont == null)
                textFont = this.Font;
            g.DrawString(item.Text, textFont, new SolidBrush(textColor), itemRect, strFmt);
        }

        protected virtual Rectangle DrawItemImage(Graphics g, Rectangle rect, SkinListViewItem item)
        {
            // Draw Item Image
            Rectangle imgRect = Rectangle.Empty;
            if (item.Image != null)
            {
                Image itemImage = item.Image[item.State];
                if (itemImage == null)
                {
                    itemImage = item.Image[ControlState.Normal];
                }

                if (itemImage != null)
                {
                    imgRect = rect;
                    imgRect.X += this.GetArrowWidth();
                    if (itemImage.Height < imgRect.Height)
                    {
                        imgRect.Y += (imgRect.Height - itemImage.Height) / 2;
                        imgRect.Height = itemImage.Height;
                    }
                    else
                    {
                        imgRect.Y += 1;
                        imgRect.Height -= 2;
                    }

                    imgRect.Width = imgRect.Height;

                    g.DrawImage(itemImage, imgRect, 0, 0, itemImage.Width, itemImage.Height, GraphicsUnit.Pixel);
                }
            }

            return imgRect;
        }
    }
}
