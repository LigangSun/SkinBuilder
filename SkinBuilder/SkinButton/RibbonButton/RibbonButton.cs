// ***************************************************************
//  RibbonButton   version:  1.0   ? date: 02/22/2008
//  -------------------------------------------------------------
//  
//  -------------------------------------------------------------
//  Copyright (C) 2008 - All Rights Reserved
// ***************************************************************
// 
//  Author: Jeffery
//
// ***************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ZLIS.SkinBuilder
{
    public partial class RibbonButton : SkinButton
    {
        //Timer
        private System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();

        //Images
        private Image _img;
        private Image _img_H;
        private Image _img_D;
        private Image _img_G;

        private String s_folder;
        private String s_filename;
        private String _infotitle = "";
        private String _infocomment = "";
        private String _infoimage = "";
        private Color _infocolor = Color.FromArgb(201, 217, 239);
        private Color _TextColor = Color.Black;

        private Image _toshow;

        //Fading
        bool b_fad = false;
        int i_fad = 0; //0 nothing, 1 entering, 2 leaving
        int i_value = 255; //Level of transparency

        //InfoForm
        private SkinToolTip toolTip = new SkinToolTip();

        //Constructor
        public RibbonButton()
            : base()
        {
            this.BackColor = Color.Transparent;
            this.EnableFade = true;
        }

        //Properties
        public Bitmap img_on
        {
            get { return base.HighlightBitmap; }
            set { base.HighlightBitmap = value; }
        }

        public Bitmap img_click
        {
            get { return base.DownBitmap; }
            set { base.DownBitmap = value; }
        }
        public Bitmap img_back
        {
            get { return base.NormalBitmap; }
            set
            {
                base.NormalBitmap = value;
                this.Invalidate();
            }
        }

        public Image Img
        {
            get { return _img; }
            set
            {
                _img = value;
                this.Image = _img;
                this.Invalidate();
            }
        }

        public Image Img_H
        {
            get { return this._img_H; }
            set 
            {
                this._img_H = value;
                this.Invalidate();
            }
        }

        public Image Img_D
        {
            get { return this._img_D; }
            set
            {
                this._img_D = value;
                this.Invalidate();
            }
        }

        public Image Img_G
        {
            get { return this._img_G; }
            set
            {
                this._img_G = value;
                this.Invalidate();
            }
        }

        protected new bool DesignMode
        {
            get
            {
                try
                {
                    if (base.DesignMode)
                        return true;

                    Control parent = this.Parent;
                    while (parent != null)
                    {
                        if (parent.Site != null)
                            return parent.Site.DesignMode;

                        parent = parent.Parent;
                    }

                    return false;
                }
                catch (System.Exception ex)
                {

                }

                return false;
            }
        }

        protected override void InitEvents()
        {
        }

        protected override void DrawImage(Graphics g, Rectangle rect)
        {
            ImageObject imgObject = this.GetImageObject();
            if (imgObject != null && !imgObject.IsEmpty)
            {
                if (this.EnableFade)
                {
                    ControlState state = this.State();
                    if (state == ControlState.Normal)
                    {
                        if (alpha > 0)
                            DrawEngine.DrawRect2(g, imgObject, rect, ControlState.Highlight, (float)alpha / 255f);
                        DrawEngine.DrawRect2(g, imgObject, rect, ControlState.Normal, 1.0f - (float)alpha / 255f);
                    }
                    else
                        DrawEngine.DrawRect2(g, imgObject, rect, this.State());
                }
                else
                    DrawEngine.DrawRect2(g, imgObject, rect, this.State());
            }

            Image img2Draw = null;
            switch (this.State())
            {
                case ControlState.Normal:
                    img2Draw = this._img;
                    break;
                case ControlState.Highlight:
                    img2Draw = this._img_H;
                    break;
                case ControlState.Down:
                    img2Draw = this._img_D;
                    break;
                case ControlState.Disable:
                    img2Draw = this._img_G;
                    break;
            }

            if (img2Draw != null)
            {
                Rectangle imgRect = new Rectangle((this.ClientRectangle.Width - img2Draw.Width) / 2,
                    0,
                    img2Draw.Width,
                    img2Draw.Height);

                ColorMatrix colorMatrix = new ColorMatrix();
                colorMatrix.Matrix00 = 1.0f;
                colorMatrix.Matrix11 = 1.0f;
                colorMatrix.Matrix22 = 1.0f;
                colorMatrix.Matrix33 = 1.0f - (float)alpha / 255f;
                if (colorMatrix.Matrix33 < 0.5f)
                    colorMatrix.Matrix33 = 0.5f;

                ImageAttributes imgAttr = new ImageAttributes();
                imgAttr.SetColorMatrix(colorMatrix);

                g.DrawImage(img2Draw, imgRect, 0, 0, img2Draw.Width, img2Draw.Height, GraphicsUnit.Pixel, imgAttr);
            }

            if (this.Text.Length > 0)
            {
                StringFormat strFmt = new StringFormat();
                strFmt.Alignment = StringAlignment.Center;
                strFmt.LineAlignment = StringAlignment.Near;
                strFmt.Trimming = StringTrimming.EllipsisWord;

                SizeF strSize = g.MeasureString(this.Text, this.Font);
                float strHeight = strSize.Height + 1f;

                if (this._img != null)
                {
                    strHeight = (float)this.Height - this._img.Height;
                }

                RectangleF textRect = new RectangleF(0f, (float)this.Height - strHeight - 2f, (float)this.Width, strHeight);
                
                SolidBrush textBrush = null;
                if (this.Enabled)
                {
                    textBrush = new SolidBrush(this.ForeColor);
                    g.DrawString(this.Text, this.Font, textBrush, textRect, strFmt);
                }
                else
                {
                    ControlPaint.DrawStringDisabled(g, this.Text, this.Font, this.BackColor, textRect, strFmt);
                }
            }
        }

        protected override void DrawString(Graphics g, Rectangle rect)
        {
            //base.DrawString(g, rect);
        }
    }
}

