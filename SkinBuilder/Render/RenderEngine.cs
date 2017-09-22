// ***************************************************************
//  RenderEngine   version:  1.0   ? date: 02/12/2008
//  -------------------------------------------------------------
//  
//  -------------------------------------------------------------
//  Health121 Copyright (C) 2008 - All Rights Reserved
// ***************************************************************
//  
//  Author: Jeffery
//  
//  Purpurs: Render the defined style
// ***************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace ZLIS.SkinBuilder
{
    public class RenderEngine : ToolStripRenderer, IRender
    {
        private ColorTable colorTable = new ColorTable();
        private ImageTable imageTable = new ImageTable();
        private int toolstripRadius = 2;
        private int buttonRadius = 4;

#region Properties

        protected ColorTable ColorTable
        {
            get { return this.colorTable; }
            set { this.colorTable = value; }
        }

        /// <summary>
        /// Gets or sets the buttons rectangle radius
        /// </summary>
        public int ButtonRadius
        {
            get { return this.buttonRadius; }
            set { this.buttonRadius = value; }
        }

        /// <summary>
        /// Gets or sets the radius of the rectangle of the hole ToolStrip
        /// </summary>
        public int ToolStripRadius
        {
            get { return this.toolstripRadius; }
            set { this.toolstripRadius = value; }
        }

        /// <summary>
        /// Gets ors sets if background glow should be rendered
        /// </summary>
        public bool BackgroundGlow
        {
            get { return this.colorTable.EnalbeGlow; }
        }

        /// <summary>
        /// Gets or sets if glossy effect should be rendered
        /// </summary>
        public bool GlossyEffect
        {
            get { return this.colorTable.GlossyEffect; }
        }
#endregion

#region Implement IRender
        public ColorTable GetColorTable()
        {
            return this.colorTable;
        }

        public void SetColorTable(ColorTable table)
        {
            this.colorTable = table;
        }

        public void SetImageTable(ImageTable table)
        {
            this.imageTable = table;
        }

        public void DrawButton(Object sender, Graphics g, ImageObject obj,
            Rectangle rect, ToolStripTextDirection textDirection)
        {
            ISkinBase skinBase = (ISkinBase)sender;
            ButtonBase btn = (ButtonBase)sender;

            ImageObject imgObject = skinBase.GetImageObject();
            if (imgObject != null && !imgObject.IsEmpty)
            {
                DrawEngine.DrawRect2(g, imgObject, rect, skinBase.State());
            }
            else if (this.imageTable.GeneralButtonImageObject != null)
            {
                DrawEngine.DrawRect2(g, this.imageTable.GeneralButtonImageObject, rect, skinBase.State());
            }
            else // Draw the button use color
            {
                this.FillButton(sender, g, rect, skinBase.State());
            }

            Size txts = Size.Empty;

            StringFormat format1;
            using (format1 = new StringFormat())
            {
                format1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
                format1.FormatFlags |= StringFormatFlags.LineLimit;
                format1.FormatFlags |= StringFormatFlags.NoWrap;
                SizeF ef1 = g.MeasureString(btn.Text, btn.Font, new SizeF((float)rect.Width, (float)rect.Height), format1);
                txts = Size.Ceiling(ef1);
            }

            Rectangle txtr = rect;
            txtr = DrawEngine.HAlignWithin(txts, txtr, btn.TextAlign);
            txtr = DrawEngine.VAlignWithin(txts, txtr, btn.TextAlign);

            Brush brushText;
            StringFormat strFmt = new StringFormat();
            strFmt.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
            strFmt.FormatFlags |= StringFormatFlags.LineLimit;
            strFmt.FormatFlags |= StringFormatFlags.NoWrap;
            if (textDirection == ToolStripTextDirection.Vertical90 ||
                textDirection == ToolStripTextDirection.Vertical270)
            {
                strFmt.FormatFlags |= StringFormatFlags.DirectionVertical;
                txtr = Rectangle.FromLTRB(txtr.Left, 0, txtr.Right, btn.Height);
                strFmt.Alignment = StringAlignment.Center;
            }

            if (btn.RightToLeft == RightToLeft.Yes)
            {
                strFmt.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            }
            brushText = new SolidBrush(btn.ForeColor);
            if (skinBase.State() == ControlState.Disable)
                g.DrawString(btn.Text, btn.Font, new SolidBrush(SystemColors.GrayText), txtr, strFmt);
            else
                g.DrawString(btn.Text, btn.Font, brushText, (RectangleF)txtr, strFmt);
            brushText.Dispose();
        }

        public void DrawCheckBox(Object sender, Graphics g, Rectangle rect, bool check)
        {
            ISkinBase skinBase = (ISkinBase)sender;
            ButtonBase btn = (ButtonBase)sender;

            Bitmap bitmap = skinBase.GetBitmap(skinBase.State());
            if (bitmap == null)
                bitmap = this.imageTable.GeneralCheckBoxImageObject[skinBase.State()];
            if (bitmap != null)
            {
                int width = bitmap.Width;
                int height = bitmap.Height;
                if (width > 22)
                    width = 22;
                if (height > 22)
                    height = 22;
                Int32 top = (rect.Height - height) / 2;
                Rectangle rcCheck = new Rectangle(0, top, width, height);
                g.DrawImage(bitmap, rcCheck, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);

                Bitmap checkedBitmap = skinBase.GetBitmap(ControlState.Checked);
                if (checkedBitmap == null)
                    checkedBitmap = this.imageTable.GeneralCheckBoxImageObject[ControlState.Checked];
                if (checkedBitmap != null && check)
                {
                    float opacity = 1.0f;
                    if (skinBase.State() != ControlState.Normal && skinBase.State() != ControlState.Disable)
                        opacity = 0.5f;
                    float[][] colorMatrixElements = { 
                            new float[] {1.0f, 0, 0, 0, 0},
                            new float[] {0, 1.0f, 0, 0, 0},
                            new float[] {0, 0, 1.0f, 0, 0},
                            new float[] {0, 0, 0, opacity, 0},
                            new float[] {0, 0, 0, 0, 1.0f}};

                    ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
                    ImageAttributes imageAttr = new ImageAttributes();
                    imageAttr.SetColorMatrix(colorMatrix);

                    g.DrawImage(checkedBitmap, rcCheck, 0, 0, checkedBitmap.Width, checkedBitmap.Height, GraphicsUnit.Pixel, imageAttr);
                }

                StringFormat strFmt = new StringFormat();
                strFmt.Alignment = StringAlignment.Near;
                strFmt.LineAlignment = StringAlignment.Center;
                SizeF strSize = g.MeasureString(btn.Text, btn.Font);
                Rectangle rcText = new Rectangle(rect.Left + rcCheck.Width + 1, (rect.Height - (int)strSize.Height) / 2, (int)strSize.Width + 1, (int)strSize.Height + 1);
                if (skinBase.State() == ControlState.Disable)
                    g.DrawString(btn.Text, btn.Font, new SolidBrush(SystemColors.GrayText), rcText, strFmt);
                else
                    g.DrawString(btn.Text, btn.Font, new SolidBrush(btn.ForeColor), rcText, strFmt);

                if (btn.Focused)
                {
                    ControlPaint.DrawFocusRectangle(g, rcText);
                }
            }
            else
            {
                bool selected = (skinBase.State() == ControlState.Highlight) ? true : false;
                bool pressed = (skinBase.State() == ControlState.Down) ? true : false;
                DrawVistaButtonBackground(g, rect, selected, pressed, check);
            }
        }

        public void DrawRadioButton(Object sender, Graphics g, Rectangle rect, bool check)
        {
            ISkinBase skinBase = (ISkinBase)sender;
            ButtonBase btn = (ButtonBase)sender;

            Bitmap bitmap = skinBase.GetBitmap(skinBase.State());
            if (bitmap == null)
                bitmap = this.imageTable.GeneralRadioButtonImageObject[skinBase.State()];
            if (bitmap != null)
            {
                int width = SystemInformation.MenuCheckSize.Width;
                int height = SystemInformation.MenuCheckSize.Height;
                if (width > 24)
                    width = 24;
                if (height > 24)
                    height = 24;
                Int32 top = (rect.Height - height) / 2;
                Rectangle rcCheck = new Rectangle(0, top, width, height);
                g.DrawImage(bitmap, rcCheck, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);

                Bitmap checkedBitmap = skinBase.GetBitmap(ControlState.Checked);
                if (checkedBitmap == null)
                    checkedBitmap = this.imageTable.GeneralRadioButtonImageObject[ControlState.Checked];
                if (checkedBitmap != null && check)
                {
                    float opacity = 1.0f;
                    if (skinBase.State() != ControlState.Normal && skinBase.State() != ControlState.Disable)
                        opacity = 0.5f;
                    float[][] colorMatrixElements = { 
                            new float[] {1.0f, 0, 0, 0, 0},
                            new float[] {0, 1.0f, 0, 0, 0},
                            new float[] {0, 0, 1.0f, 0, 0},
                            new float[] {0, 0, 0, opacity, 0},
                            new float[] {0, 0, 0, 0, 1.0f}};

                    ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
                    ImageAttributes imageAttr = new ImageAttributes();
                    imageAttr.SetColorMatrix(colorMatrix);

                    g.DrawImage(checkedBitmap, rcCheck, 0, 0, checkedBitmap.Width, checkedBitmap.Height, GraphicsUnit.Pixel, imageAttr);
                }

                StringFormat strFmt = new StringFormat();
                strFmt.Alignment = StringAlignment.Near;
                strFmt.LineAlignment = StringAlignment.Center;
                SizeF strSize = g.MeasureString(btn.Text, btn.Font);
                Rectangle rcText = new Rectangle(rect.Left + rcCheck.Width + 1, (rect.Height - (int)strSize.Height) / 2, (int)strSize.Width + 1, (int)strSize.Height + 1);
                if (skinBase.State() == ControlState.Disable)
                    g.DrawString(btn.Text, btn.Font, new SolidBrush(SystemColors.GrayText), rcText, strFmt);
                else
                    g.DrawString(btn.Text, btn.Font, new SolidBrush(btn.ForeColor), rcText, strFmt);

                if (btn.Focused)
                {
                    ControlPaint.DrawFocusRectangle(g, rcText);
                }
            }
            else
            {
                bool selected = (skinBase.State() == ControlState.Highlight) ? true : false;
                bool pressed = (skinBase.State() == ControlState.Down) ? true : false;
                DrawVistaButtonBackground(g, rect, selected, pressed, check);
            }
        }

        public void DrawComboBoxBorder(Object sender, Graphics g, Rectangle rect)
        {
            ISkinBase skinBase = (ISkinBase)sender;
            ComboBox cbx = (ComboBox)sender;

            Rectangle drawRect = new Rectangle(0, 0, cbx.Width - 1, cbx.Height - 1);
            Color clr = this.colorTable.ComboBoxBorder;
            if (skinBase.State() == ControlState.Highlight)
                clr = this.colorTable.ComboBoxBorderHot;
            g.DrawRectangle(new Pen(clr), drawRect);
        }

        public void DrawComboBoxButton(Object sender, Graphics g, Rectangle rect)
        {
            ISkinBase skinBase = (ISkinBase)sender;
            ComboBox cbx = (ComboBox)sender;

            Bitmap bitmap = skinBase.GetBitmap(skinBase.State());
            if (bitmap != null)
            {
                g.DrawImage(bitmap, rect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);
            }
            else
            {
                FillComboBoxButton(sender, g, rect, skinBase.State());
            }
        }

#endregion

        #region Methods

        /// <summary>
        /// Initializes properties for ToolStripMenuItem objects
        /// </summary>
        /// <param name="item"></param>
        protected virtual void InitializeToolStripMenuItem(ToolStripMenuItem item)
        {
            item.AutoSize = false;
            item.Height = ColorTable.MenuItemHeight;
            item.TextAlign = ContentAlignment.MiddleLeft;

            foreach (ToolStripItem subitem in item.DropDownItems)
            {
                if (subitem is ToolStripMenuItem)
                {
                    InitializeToolStripMenuItem(subitem as ToolStripMenuItem);
                }
            }
        }

        /// <summary>
        /// Draws the glossy effect on the toolbar
        /// </summary>
        /// <param name="g"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private void DrawGlossyEffect(Graphics g, ToolStrip t)
        {
            DrawGlossyEffect(g, t, 0);
        }

        /// <summary>
        /// Draws the glossy effect on the toolbar
        /// </summary>
        /// <param name="g"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private void DrawGlossyEffect(Graphics g, ToolStrip t, int offset)
        {
            Rectangle glossyRect = new Rectangle(0, offset,
                    t.Width - 1,
                    (t.Height - 1) / 2);

            using (LinearGradientBrush b = new LinearGradientBrush(
                glossyRect.Location, new PointF(0, glossyRect.Bottom),
                ColorTable.GlossyEffectStart,
                ColorTable.GlossyEffectEnd))
            {
                using (GraphicsPath border =
                    PathCreator.CreateTopRoundRectangle(glossyRect, ToolStripRadius))
                {
                    g.FillPath(b, border);
                }
            }
        }

        /// <summary>
        /// Renders the background of a button
        /// </summary>
        /// <param name="e"></param>
        private void DrawVistaButtonBackground(ToolStripItemRenderEventArgs e)
        {
            bool chk = false;

            if (e.Item is ToolStripButton)
            {
                chk = (e.Item as ToolStripButton).Checked;
            }

            DrawVistaButtonBackground(e.Graphics,
                new Rectangle(Point.Empty, e.Item.Size),
                e.Item.Selected,
                e.Item.Pressed,
                chk);
        }

        /// <summary>
        /// Renders the background of a button on the specified rectangle using the specified device
        /// </summary>
        /// <param name="e"></param>
        private void DrawVistaButtonBackground(Graphics g, Rectangle r, bool selected, bool pressed, bool checkd)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle outerBorder = new Rectangle(r.Left, r.Top, r.Width - 1, r.Height - 1);
            Rectangle border = outerBorder;// border.Inflate(-1, -1);
            Rectangle innerBorder = border; innerBorder.Inflate(-1, -1);
            Rectangle glossy = outerBorder; glossy.Height /= 2;
            Rectangle fill = innerBorder; 
            if (this.GlossyEffect || this.BackgroundGlow)
                fill.Height /= 2;
            Rectangle glow = Rectangle.FromLTRB(outerBorder.Left,
                outerBorder.Top + Convert.ToInt32(Convert.ToSingle(outerBorder.Height) * .5f),
                outerBorder.Right, outerBorder.Bottom);

            if (selected || pressed || checkd)
            {
                #region Layers

             /*   //Outer border
            //    using (GraphicsPath path =
            //        PathCreator.CreateRoundRectangle(outerBorder, ButtonRadius))
                {
                    using (Pen p = new Pen(ColorTable.ToolButtonOuterBorder))
                    {
                        g.DrawRectangle(p, outerBorder);
                    }
                }*/

                //Checked fill
                if (checkd)
                {
               //     using (GraphicsPath path = PathCreator.CreateRoundRectangle(innerBorder, 2))
                    {
                        using (Brush b = new SolidBrush(selected ? ColorTable.CheckedToolButtonFillHot : ColorTable.CheckedToolButtonFill))
                        {
                            g.FillRectangle(b, border);
                        }
                    }
                }

                //Glossy effefct
                if (this.GlossyEffect)
                {
                //    using (GraphicsPath path = PathCreator.CreateTopRoundRectangle(glossy, ButtonRadius))
                    {
                        using (Brush b = new LinearGradientBrush(
                            new Point(0, glossy.Top),
                            new Point(0, glossy.Bottom),
                            ColorTable.GlossyEffectStart,
                            ColorTable.GlossyEffectEnd))
                        {
                            g.FillRectangle(b, glossy);
                        }
                    }
                }

                //Border
            //    using (GraphicsPath path =
            //        PathCreator.CreateRoundRectangle(border, ButtonRadius))
                {
                    using (Pen p = new Pen(ColorTable.ToolButtonBorder))
                    {
                        g.DrawRectangle(p, border);
                    }
                }

                Color fillStart = pressed ? ColorTable.ToolButtonFillStartPressed : ColorTable.ToolButtonFillStart;
                Color fillEnd = pressed ? ColorTable.ToolButtonFillEndPressed : ColorTable.ToolButtonFillEnd;

                Trace.WriteLine(string.Format("Pressed {0}", pressed));
                Trace.WriteLine(string.Format("Selected {0}", selected));

                //Fill
           //     using (GraphicsPath path = PathCreator.CreateTopRoundRectangle(fill, ButtonRadius))
                {
                    using (Brush b = new LinearGradientBrush(
                        new Point(0, fill.Top),
                        new Point(0, fill.Bottom),
                        fillStart, fillEnd))
                    {
                        g.FillRectangle(b, fill);
                    }
                }

            /*    Color innerBorderColor = pressed || checkd ? ColorTable.ToolButtonInnerBorderPressed : ColorTable.ToolButtonInnerBorder;

                //Inner border
            //    using (GraphicsPath path =
             //       PathCreator.CreateRoundRectangle(innerBorder, ButtonRadius))
                {
                    using (Pen p = new Pen(innerBorderColor))
                    {
                        g.DrawRectangle(p, innerBorder);
                    }
                }*/

                //Glow
                if (this.BackgroundGlow)
                {
                    using (GraphicsPath clip = PathCreator.CreateRoundRectangle(glow, 2))
                    {
                        g.SetClip(clip, CombineMode.Intersect);

                        Color glowColor = ColorTable.ToolGlow;

                        if (checkd)
                        {
                            if (selected)
                            {
                                glowColor = ColorTable.CheckedGlowHot;
                            }
                            else
                            {
                                glowColor = ColorTable.CheckedGlow;
                            }
                        }

                        using (GraphicsPath brad = PathCreator.CreateBottomRadialPath(glow))
                        {
                            using (PathGradientBrush pgr = new PathGradientBrush(brad))
                            {
                                unchecked
                                {
                                    int opacity = 255;
                                    RectangleF bounds = brad.GetBounds();
                                    pgr.CenterPoint = new PointF((bounds.Left + bounds.Right) / 2f, (bounds.Top + bounds.Bottom) / 2f);
                                    pgr.CenterColor = Color.FromArgb(opacity, glowColor);
                                    pgr.SurroundColors = new Color[] { Color.FromArgb(0, glowColor) };
                                }
                                g.FillPath(pgr, brad);
                            }
                        }
                        g.ResetClip();
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// Draws the background of a menu, vista style
        /// </summary>
        /// <param name="e"></param>
        private void DrawVistaMenuBackground(ToolStripItemRenderEventArgs e)
        {
            DrawVistaMenuBackground(e.Graphics,
                                    new Rectangle(Point.Empty, e.Item.Size),
                                    e.Item.Selected, e.Item.Owner is MenuStrip);   
        }

        /// <summary>
        /// Draws the background of a menu, vista style
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        /// <param name="highlighted"></param>
        private void DrawVistaMenuBackground(Graphics g, Rectangle r, bool highlighted, bool isMainMenu)
        {
            int margin = 2;
            int left = 22;

            #region IconSeparator

            if (!isMainMenu)
            {
                using (Pen p = new Pen(ColorTable.MenuDark))
                {
                    g.DrawLine(p,
                                new Point(r.Left + left, r.Top),
                                new Point(r.Left + left, r.Height - margin));
                }

                using (Pen p = new Pen(ColorTable.MenuLight))
                {
                    g.DrawLine(p,
                                new Point(r.Left + left + 1, r.Top),
                                new Point(r.Left + left + 1, r.Height - margin));
                } 
            }

            #endregion

            if (highlighted)
            {
                #region Draw Rectangle

                Rectangle itemRect = new Rectangle(r.X + margin, r.Y + margin, r.Width - margin * 2, r.Height - margin * 2);
                {

                    using (Brush b = new LinearGradientBrush(
                        new Point(0, 2), new Point(0, r.Height - 2),
                        ColorTable.MenuHighlightStart,
                        ColorTable.MenuHighlightEnd))
                    {
                        g.FillRectangle(b, itemRect);
                    }

                    using (Pen p = new Pen(ColorTable.MenuItemHighlightBorder))
                    {
                        g.DrawRectangle(p, itemRect);
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// Draws the border of the vista menu window
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        private void DrawVistaMenuBorder(Graphics g, Rectangle r)
        {
            using (Pen p = new Pen(ColorTable.BackgroundBorder))
            {
                g.DrawRectangle(p,
                    new Rectangle(r.Left, r.Top, r.Width - 1, r.Height - 1));
            }
        }

        #endregion

        protected override void Initialize(ToolStrip toolStrip)
        {
            toolStrip.ForeColor = ColorTable.Text;
            base.Initialize(toolStrip);
        }

        protected override void InitializeItem(ToolStripItem item)
        {
            base.InitializeItem(item);

            //Don't Affect ForeColor of TextBoxes and ComboBoxes
            if ( !((item is ToolStripTextBox) || (item is ToolStripComboBox)) )
            {
                item.ForeColor = ColorTable.Text;
            }
            
            item.Padding = new Padding(1, 4, 1, 4);

            if (item is ToolStripSplitButton)
            {
                ToolStripSplitButton btn = item as ToolStripSplitButton;
                btn.DropDownButtonWidth = ColorTable.MenuItemHeight;

                foreach (ToolStripItem subitem in btn.DropDownItems)
                {
                    if (subitem is ToolStripMenuItem)
                    {
                        InitializeToolStripMenuItem(subitem as ToolStripMenuItem);
                    }
                }
            }
            
            if (item is ToolStripDropDownButton)
            {
                ToolStripDropDownButton btn = item as ToolStripDropDownButton;
                btn.ShowDropDownArrow = false;

                foreach (ToolStripItem subitem in btn.DropDownItems)
                {
                    if (subitem is ToolStripMenuItem)
                    {
                        InitializeToolStripMenuItem(subitem as ToolStripMenuItem);
                    }
                }
            }

            if (item is MenuItem || item is ToolStripMenuItem)
            {
                item.Height = ColorTable.MenuItemHeight;
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem menuItem = item as ToolStripMenuItem;
                    if (menuItem.GetCurrentParent() is ToolStrip)
                    {
                        foreach (ToolStripItem subItem in menuItem.DropDownItems)
                        {
                            this.InitializeItem(subItem);
                        }
                    }
                    else
                    {
                        foreach (ToolStripItem subItem in menuItem.DropDownItems)
                        {
                            this.InitializeItem(subItem);
                        }
                        item.Height = ColorTable.MenuItemHeight;
                    }
                }
            }   
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is ToolStripDropDownMenu)
            {
                #region Draw Rectangled Border

                DrawVistaMenuBorder(e.Graphics,
                    new Rectangle(Point.Empty, e.ToolStrip.Size));

                #endregion
            }
            else if (e.ToolStrip is ToolStrip)
            {
                #region Draw Rounded Border
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

             //   using (GraphicsPath path = PathCreator.GetToolStripRectangle(e.ToolStrip, this.ToolStripRadius))
                {
                    using (Pen p = new Pen(ColorTable.BackgroundBorder))
                    {
                    //    e.Graphics.DrawRectangle(p, new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height-1));
                        e.Graphics.DrawLine(p, new Point(0, e.ToolStrip.Height - 1), new Point(e.ToolStrip.Width, e.ToolStrip.Height - 1));
                    }
                } 
                #endregion
            }   
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is ToolStripDropDownMenu)
            {
                using (LinearGradientBrush b = new LinearGradientBrush(new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height),
                    ColorTable.MenuPageBackground.Start,
                    ColorTable.MenuPageBackground.End,
                    ColorTable.MenuPageBackground.Angle))
                {
                    e.Graphics.FillRectangle(b, new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height));
                }

                /*
                using (LinearGradientBrush b = new LinearGradientBrush(new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height),
                    ColorTable.MenuPageBorder.Start,
                    ColorTable.MenuPageBorder.End,
                    ColorTable.MenuPageBorder.Angle))
                {
                    Pen borderPen = new Pen(b);
                    borderPen.Width = ColorTable.MenuPageBorderWidth;
                    int borderWidth = ColorTable.MenuPageBorderWidth;
                    e.Graphics.DrawRectangle(borderPen, new Rectangle(borderWidth / 2, borderWidth / 2, e.ToolStrip.Width - borderWidth, e.ToolStrip.Height - borderWidth));
                }
                */

                return;
            }

            #region Background

            if (e.ToolStrip is MenuStrip)
            {
                using (SolidBrush b = new SolidBrush(ColorTable.MenuBackground))
                {
                    e.Graphics.FillRectangle(b, new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height));
                }
            }
            else
            {
                using (LinearGradientBrush b = new LinearGradientBrush(
                    Point.Empty, new PointF(0, e.ToolStrip.Height),
                    ColorTable.BackgroundStart,
                    ColorTable.BackgroundEnd))
                {
                 //   using (GraphicsPath border = PathCreator.GetToolStripRectangle(e.ToolStrip, this.ToolStripRadius))
                    {
                        e.Graphics.FillRectangle(b, new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height));
                    }
                }
            }

            #endregion

            if (GlossyEffect)
            {
                #region Glossy Effect

                DrawGlossyEffect(e.Graphics, e.ToolStrip, 1);

                #endregion 
            }

            if (BackgroundGlow)
            {
                #region BackroundGlow

                int glowSize = Convert.ToInt32(Convert.ToSingle(e.ToolStrip.Height) * 0.15f);
                Rectangle glow = new Rectangle(0,
                    e.ToolStrip.Height - glowSize - 1,
                    e.ToolStrip.Width - 1,
                    glowSize);
                
                using (LinearGradientBrush b = new LinearGradientBrush(
                    new Point(0, glow.Top -1 ), new PointF(0, glow.Bottom),
                    Color.FromArgb(0, ColorTable.BackgroundGlow),
                    ColorTable.BackgroundGlow))
                {
                    using (GraphicsPath border =
                        PathCreator.CreateBottomRoundRectangle(glow, ToolStripRadius))
                    {
                        e.Graphics.FillPath(b, border);
                    }
                }

                #endregion
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item is ToolStripButton)
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            }

            if (e.Item is ToolStripMenuItem
                && !(e.Item.Owner is MenuStrip))
            {
                Rectangle r = new Rectangle(e.TextRectangle.Location, new Size(e.TextRectangle.Width, 24));
                e.TextRectangle = r;
                e.TextColor = ColorTable.MenuText;
            }

            base.OnRenderItemText(e);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            DrawVistaButtonBackground(e);
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            DrawVistaButtonBackground(e);

            ToolStripDropDownButton item = e.Item as ToolStripDropDownButton; if (item == null) return;

            Rectangle arrowBounds = new Rectangle(item.Width - 18, 0, 18, item.Height);
            
            DrawArrow(new ToolStripArrowRenderEventArgs(
                e.Graphics, e.Item,
                arrowBounds,
                ColorTable.DropDownArrow, ArrowDirection.Down));
        }

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {
            DrawVistaButtonBackground(e);
            
            ToolStripSplitButton item = e.Item as ToolStripSplitButton; if(item==null) return;

            Rectangle arrowBounds = item.DropDownButtonBounds;
            Rectangle buttonBounds = new Rectangle(item.ButtonBounds.Location, new Size(item.ButtonBounds.Width + 2, item.ButtonBounds.Height));
            Rectangle dropDownBounds = item.DropDownButtonBounds;

            DrawVistaButtonBackground(e.Graphics, buttonBounds, item.ButtonSelected,
                item.ButtonPressed, false);

            DrawVistaButtonBackground(e.Graphics, dropDownBounds, item.DropDownButtonSelected,
                item.DropDownButtonPressed, false);

            DrawArrow(new ToolStripArrowRenderEventArgs(
                e.Graphics, e.Item,
                arrowBounds,
                ColorTable.DropDownArrow, ArrowDirection.Down));
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            if (!e.Item.Enabled)
            {
                base.OnRenderItemImage(e);
            }
            else
            {
                if (e.Image != null)
                {
                    e.Graphics.DrawImage(e.Image, e.ImageRectangle);
                }
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Owner is MenuStrip)
            {
                DrawVistaButtonBackground(e);
            }
            else
            {
                DrawVistaMenuBackground(e.Graphics,
                   new Rectangle(Point.Empty, e.Item.Size),
                   e.Item.Selected, e.Item.Owner is MenuStrip);
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            if ((e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                // Create the light and dark line pens
                using (Pen lightPen = new Pen(ColorTable.SeparatorStart),
                            darkPen = new Pen(ColorTable.SeparatorEnd))
                {
                    DrawEngine.DrawSeparator(e.Graphics, e.Vertical, e.Item.Bounds,
                                  lightPen, darkPen, 31,
                                  (e.ToolStrip.RightToLeft == RightToLeft.Yes));
                }
            }
            else if (e.ToolStrip is StatusStrip)
            {
                // Create the light and dark line pens
                using (Pen lightPen = new Pen(ColorTable.SeparatorStart),
                            darkPen = new Pen(ColorTable.SeparatorEnd))
                {
                    DrawEngine.DrawSeparator(e.Graphics, e.Vertical, e.Item.Bounds,
                                  lightPen, darkPen, 0, false);
                }
            }
            else
            {
                base.OnRenderSeparator(e);
            }
        }

        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        {
            DrawVistaButtonBackground(e);

            //Chevron is obtained from the character: ?(Alt+0187)
            using (Brush b = new SolidBrush(e.Item.ForeColor))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                Font f = new Font(e.Item.Font.FontFamily, 15);

                e.Graphics.DrawString("?", f, b, new RectangleF(Point.Empty, e.Item.Size), sf); 
            }

        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (e.Item is ToolStripMenuItem && e.Item.Selected)
            {
                e.ArrowColor = ColorTable.MenuText;
            }

            base.OnRenderArrow(e);
        }

        protected virtual void FillButton(Object sender, Graphics g, Rectangle rect, ControlState state)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(ColorTable.ButtonBase);

            Button btn = (Button)sender;

            Rectangle outerBorder = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
            Rectangle border = new Rectangle(rect.Left, rect.Top, rect.Width - 2, rect.Height - 2);// border.Inflate(-1, -1);
            Rectangle innerBorder = border; innerBorder.Inflate(-1, -1);
            Rectangle glossy = outerBorder; glossy.Height /= 2;
            Rectangle fill = innerBorder;// fill.Height /= 2;
            Rectangle glow = Rectangle.FromLTRB(outerBorder.Left,
                outerBorder.Top + Convert.ToInt32(Convert.ToSingle(outerBorder.Height) * .5f),
                outerBorder.Right, outerBorder.Bottom);

            //Outer border
            using (GraphicsPath path =
                PathCreator.CreateRoundRectangle(outerBorder, ButtonRadius))
            {
                using (Pen p = new Pen(ColorTable.ButtonOuterBorder))
                {
                    g.DrawPath(p, path);
                    btn.Region = new Region(path);
                }
            }

            //Glossy effefct
            if (state == ControlState.Highlight ||
                state == ControlState.Down)
            {
                using (GraphicsPath path = PathCreator.CreateTopRoundRectangle(glossy, ButtonRadius))
                {
                    using (Brush b = new LinearGradientBrush(
                        new Point(0, glossy.Top),
                        new Point(0, glossy.Bottom),
                        ColorTable.GlossyEffectStart,
                        ColorTable.GlossyEffectEnd))
                    {
                        g.FillPath(b, path);
                    }
                }
            }

            //Border
            using (GraphicsPath path =
                PathCreator.CreateRoundRectangle(border, ButtonRadius))
            {
                using (Pen p = new Pen(ColorTable.ButtonBorder))
                {
                    g.DrawPath(p, path);
                }
            }

            LinearGradientBrush lineBrush = null;
            Color innerBorderColor = ColorTable.ButtonInnerBorder;
            Color text = ColorTable.Text;
            switch (state)
            {
                case ControlState.Normal:
                    {
                        lineBrush = new LinearGradientBrush(fill,
                            ColorTable.ButtonNormalColorData.Start,
                            ColorTable.ButtonNormalColorData.End,
                            ColorTable.ButtonNormalColorData.Angle);

                        text = ColorTable.ButtonNormalColorData.Text;
                    }
                    break;
                case ControlState.Highlight:
                    {
                        lineBrush = new LinearGradientBrush(fill,
                            ColorTable.ButtonHighlightColorData.Start,
                            ColorTable.ButtonHighlightColorData.End,
                            ColorTable.ButtonHighlightColorData.Angle);

                        text = ColorTable.ButtonHighlightColorData.Text;
                    }
                    break;
                case ControlState.Down:
                    {
                        lineBrush = new LinearGradientBrush(fill,
                            ColorTable.ButtonDownColorData.Start,
                            ColorTable.ButtonDownColorData.End,
                            ColorTable.ButtonDownColorData.Angle);

                        innerBorderColor = ColorTable.ButtonInnerBorderPressed;
                        text = ColorTable.ButtonDownColorData.Text;
                    }
                    break;
                case ControlState.Disable:
                    {
                        lineBrush = new LinearGradientBrush(fill,
                            ColorTable.ButtonDisableColorData.Start,
                            ColorTable.ButtonDisableColorData.End,
                            ColorTable.ButtonDisableColorData.Angle);

                        text = ColorTable.ButtonDisableColorData.Text;
                    }
                    break;
            }

            using (GraphicsPath path = PathCreator.CreateTopRoundRectangle(fill, ButtonRadius))
            {
                 g.FillPath(lineBrush, path);
            }

            //Inner border
            using (GraphicsPath path =
                PathCreator.CreateRoundRectangle(innerBorder, ButtonRadius))
            {
                using (Pen p = new Pen(innerBorderColor))
                {
                    g.DrawPath(p, path);
                }
            }

            //Glow
            if (state == ControlState.Highlight ||
                state == ControlState.Down)
            {
                using (GraphicsPath clip = PathCreator.CreateRoundRectangle(glow, 2))
                {
                    g.SetClip(clip, CombineMode.Intersect);

                    Color glowColor = ColorTable.ButtonGlow;

                    using (GraphicsPath brad = PathCreator.CreateBottomRadialPath(glow))
                    {
                        using (PathGradientBrush pgr = new PathGradientBrush(brad))
                        {
                            unchecked
                            {
                                int opacity = 255;
                                RectangleF bounds = brad.GetBounds();
                                pgr.CenterPoint = new PointF((bounds.Left + bounds.Right) / 2f, (bounds.Top + bounds.Bottom) / 2f);
                                pgr.CenterColor = Color.FromArgb(opacity, glowColor);
                                pgr.SurroundColors = new Color[] { Color.FromArgb(0, glowColor) };
                            }
                            g.FillPath(pgr, brad);
                        }
                    }
                    g.ResetClip();
                }
            }

            // Text
            StringFormat strFmt = new StringFormat();
            strFmt.Alignment = StringAlignment.Center;
            strFmt.LineAlignment = StringAlignment.Center;
            g.DrawString(btn.Text, btn.Font, new SolidBrush(text), outerBorder, strFmt);
        }

        protected virtual void FillComboBoxButton(Object sender, Graphics g, Rectangle rect, ControlState state)
        {
            Rectangle glossy = rect;
            glossy.Height /= 2;

            //Glossy effefct
            if (state == ControlState.Highlight ||
                state == ControlState.Down)
            {
                using (GraphicsPath path = PathCreator.CreateTopRoundRectangle(glossy, ButtonRadius))
                {
                    using (Brush linearBrush = new LinearGradientBrush(
                        new Point(0, glossy.Top),
                        new Point(0, glossy.Bottom),
                        ColorTable.GlossyEffectStart,
                        ColorTable.GlossyEffectEnd))
                    {
                        g.FillPath(linearBrush, path);
                    }
                }
            }

            LinearGradientBrush lgb = null;
            if (state == ControlState.Normal)
                lgb = new LinearGradientBrush(rect, ColorTable.ComboBoxButtonBgStart, ColorTable.ComboBoxButtonBgEnd, ColorTable.ComboBoxButtonBgAngle);
            else if (state == ControlState.Highlight)
                lgb = new LinearGradientBrush(rect, ColorTable.ComboBoxButtonBgHotStart, ColorTable.ComboBoxButtonBgHotEnd, ColorTable.ComboBoxButtonBgHotAngle);
            else if (state == ControlState.Disable)
                lgb = new LinearGradientBrush(rect, ColorTable.ComboBoxButtonBgDisabledStart, ColorTable.ComboBoxButtonBgDisabledEnd, ColorTable.ComboBoxButtonBgDisabledAngle);
            else if (state == ControlState.Down)
                lgb = new LinearGradientBrush(rect, ColorTable.ComboBoxButtonBgPressedStart, ColorTable.ComboBoxButtonBgPressedEnd, ColorTable.ComboBoxButtonBgPressedAngle);
            g.FillRectangle(lgb, rect);
            Bitmap b = ZLIS.SkinBuilder.Properties.Resource.ComboArrow;
            b.MakeTransparent();
            g.DrawImage(b, rect);
            lgb.Dispose();
        }
    }
}
