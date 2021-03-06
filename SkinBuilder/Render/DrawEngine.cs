using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace ZLIS.SkinBuilder
{
    public class DrawEngine
    {
        public static ContentAlignment anyRight = ContentAlignment.BottomRight | (ContentAlignment.MiddleRight | ContentAlignment.TopRight);
        public static ContentAlignment anyTop = ContentAlignment.TopRight | (ContentAlignment.TopCenter | ContentAlignment.TopLeft);
        public static ContentAlignment anyBottom = ContentAlignment.BottomRight | (ContentAlignment.BottomCenter | ContentAlignment.BottomLeft);
        public static ContentAlignment anyCenter = ContentAlignment.BottomCenter | (ContentAlignment.MiddleCenter | ContentAlignment.TopCenter);
        public static ContentAlignment anyMiddle = ContentAlignment.MiddleRight | (ContentAlignment.MiddleCenter | ContentAlignment.MiddleLeft);

        public static void DrawRect1(Graphics g, ImageObject obj, Rectangle rect, ControlState state)
        {
            if (obj[state] == null)
                return;

            Rectangle r1, r2;
            int x = 0;
            int y = 0;
            int x1 = rect.Left;
            int y1 = rect.Top;
            r1 = new Rectangle(x, y, obj.Width, obj.Height);
            r2 = new Rectangle(x1, y1, rect.Width, rect.Height);
            g.DrawImage(obj[state], r2, r1, GraphicsUnit.Pixel);
        }

        public static void DrawRect2(Graphics g, ImageObject obj, Rectangle rect, ControlState state)
        {
            DrawRect2(g, obj, rect, state, 1.0f);
        }
        public static void DrawRect2(Graphics g, ImageObject obj, Rectangle rect, ControlState state, float alpha)
        {
            if (obj == null || obj[state] == null) 
                return;

            g.SetClip(rect);

            Rectangle r1, r2;
            int x = 0;
            int y = 0;
            int x1 = rect.Left;
            int y1 = rect.Top;

           /* if (rect.Height > obj.Height && rect.Width <= obj.Width)
            {
                r1 = new Rectangle(x, y, obj.Width, obj.SplitMargin.Top);
                r2 = new Rectangle(x1, y1, rect.Width, obj.SplitMargin.Top);
                g.DrawImage(obj[state], r2, r1, GraphicsUnit.Pixel);

                r1 = new Rectangle(x, y + obj.SplitMargin.Top, obj.Width, obj.Height - obj.SplitMargin.Top - obj.SplitMargin.Bottom);
                r2 = new Rectangle(x1, y1 + obj.SplitMargin.Top, rect.Width, rect.Height - obj.SplitMargin.Top - obj.SplitMargin.Bottom);
                if ((obj.SplitMargin.Top + obj.SplitMargin.Bottom) == 0) r1.Height = r1.Height - 1;
                g.DrawImage(obj[state], r2, r1, GraphicsUnit.Pixel);

                r1 = new Rectangle(x, y + obj.Height - obj.SplitMargin.Bottom, obj.Width, obj.SplitMargin.Bottom);
                r2 = new Rectangle(x1, y1 + rect.Height - obj.SplitMargin.Bottom, rect.Width, obj.SplitMargin.Bottom);
                g.DrawImage(obj[state], r2, r1, GraphicsUnit.Pixel);
            }
            else if (rect.Height <= obj.Height && rect.Width > obj.Width)
            {
                r1 = new Rectangle(x, y, obj.SplitMargin.Left, obj.Height);
                r2 = new Rectangle(x1, y1, obj.SplitMargin.Left, rect.Height);
                g.DrawImage(obj[state], r2, r1, GraphicsUnit.Pixel);
                r1 = new Rectangle(x + obj.SplitMargin.Left, y, obj.Width - obj.SplitMargin.Left - obj.SplitMargin.Right, obj.Height);
                r2 = new Rectangle(x1 + obj.SplitMargin.Left, y1, rect.Width - obj.SplitMargin.Left - obj.SplitMargin.Right, rect.Height);
                g.DrawImage(obj[state], r2, r1, GraphicsUnit.Pixel);
                r1 = new Rectangle(x + obj.Width - obj.SplitMargin.Right, y, obj.SplitMargin.Right, obj.Height);
                r2 = new Rectangle(x1 + rect.Width - obj.SplitMargin.Right, y1, obj.SplitMargin.Right, rect.Height);
                g.DrawImage(obj[state], r2, r1, GraphicsUnit.Pixel);
            }
            else if (rect.Height <= obj.Height && rect.Width <= obj.Width)
            {
                r1 = new Rectangle(0, 0, obj.Width, obj.Height);
                //r1.Offset(obj.r.Left,obj.r.Top);
                g.DrawImage(obj[state], new Rectangle(x1, y1, rect.Width, rect.Height), r1, GraphicsUnit.Pixel);
            }
            else*/// if (rect.Height > obj.Height && rect.Width > obj.Width)
            {
                //top-left
                r1 = new Rectangle(x, y, obj.SplitMargin.Left, obj.SplitMargin.Top);
                r2 = new Rectangle(x1, y1, obj.SplitMargin.Left, obj.SplitMargin.Top);
                DrawImage(g, obj[state], r2, r1, alpha);

                //top-bottom
                r1 = new Rectangle(x, y + obj.Height - obj.SplitMargin.Bottom, obj.SplitMargin.Left, obj.SplitMargin.Bottom);
                r2 = new Rectangle(x1, y1 + rect.Height - obj.SplitMargin.Bottom, obj.SplitMargin.Left, obj.SplitMargin.Bottom);
                DrawImage(g, obj[state], r2, r1, alpha);

                //left
                r1 = new Rectangle(x, y + obj.SplitMargin.Top, obj.SplitMargin.Left, obj.Height - obj.SplitMargin.Top - obj.SplitMargin.Bottom);
                r2 = new Rectangle(x1, y1 + obj.SplitMargin.Top, obj.SplitMargin.Left, rect.Height - obj.SplitMargin.Top - obj.SplitMargin.Bottom);
                DrawImage(g, obj[state], r2, r1, alpha);

                //top
                r1 = new Rectangle(x + obj.SplitMargin.Left, y,
                    obj.Width - obj.SplitMargin.Left - obj.SplitMargin.Right, obj.SplitMargin.Top);
                r2 = new Rectangle(x1 + obj.SplitMargin.Left, y1,
                    rect.Width - obj.SplitMargin.Left - obj.SplitMargin.Right, obj.SplitMargin.Top);
                DrawImage(g, obj[state], r2, r1, alpha);

                //right-top
                r1 = new Rectangle(x + obj.Width - obj.SplitMargin.Right, y, obj.SplitMargin.Right, obj.SplitMargin.Top);
                r2 = new Rectangle(x1 + rect.Width - obj.SplitMargin.Right, y1, obj.SplitMargin.Right, obj.SplitMargin.Top);
                DrawImage(g, obj[state], r2, r1, alpha);

                //Right
                r1 = new Rectangle(x + obj.Width - obj.SplitMargin.Right, y + obj.SplitMargin.Top,
                    obj.SplitMargin.Right, obj.Height - obj.SplitMargin.Top - obj.SplitMargin.Bottom);
                r2 = new Rectangle(x1 + rect.Width - obj.SplitMargin.Right, y1 + obj.SplitMargin.Top,
                    obj.SplitMargin.Right, rect.Height - obj.SplitMargin.Top - obj.SplitMargin.Bottom);
                DrawImage(g, obj[state], r2, r1, alpha);

                //right-bottom
                r1 = new Rectangle(x + obj.Width - obj.SplitMargin.Right, y + obj.Height - obj.SplitMargin.Bottom,
                    obj.SplitMargin.Right, obj.SplitMargin.Bottom);
                r2 = new Rectangle(x1 + rect.Width - obj.SplitMargin.Right, y1 + rect.Height - obj.SplitMargin.Bottom,
                    obj.SplitMargin.Right, obj.SplitMargin.Bottom);
                DrawImage(g, obj[state], r2, r1, alpha);

                //bottom
                r1 = new Rectangle(x + obj.SplitMargin.Left, y + obj.Height - obj.SplitMargin.Bottom,
                    obj.Width - obj.SplitMargin.Left - obj.SplitMargin.Right, obj.SplitMargin.Bottom);
                r2 = new Rectangle(x1 + obj.SplitMargin.Left, y1 + rect.Height - obj.SplitMargin.Bottom,
                    rect.Width - obj.SplitMargin.Left - obj.SplitMargin.Right, obj.SplitMargin.Bottom);
                DrawImage(g, obj[state], r2, r1, alpha);

                //Center
                r1 = new Rectangle(x + obj.SplitMargin.Left, y + obj.SplitMargin.Top,
                    obj.Width - obj.SplitMargin.Left - obj.SplitMargin.Right, obj.Height - obj.SplitMargin.Top - obj.SplitMargin.Bottom);
                //r1 = Rectangle.FromLTRB(x+obj.SplitMargin.Left,y+obj.SplitMargin.Top,
                //	x+obj.width-obj.SplitMargin.Right,y+obj.height-obj.SplitMargin.Bottom);
                r2 = new Rectangle(x1 + obj.SplitMargin.Left, y1 + obj.SplitMargin.Top,
                    rect.Width - obj.SplitMargin.Left - obj.SplitMargin.Right, rect.Height - obj.SplitMargin.Top - obj.SplitMargin.Bottom);
                DrawImage(g, obj[state], r2, r1, alpha);
            }

            g.ResetClip();
        }

        private static void DrawImage(Graphics g, Image img, Rectangle dest, Rectangle source, float alpha)
        {
            if (alpha > 0.9999f)
            {
                g.DrawImage(img, dest, source, GraphicsUnit.Pixel);
            }
            else
            {
                ColorMatrix colorMatrix = new ColorMatrix();
                colorMatrix.Matrix00 = 1.0f;
                colorMatrix.Matrix11 = 1.0f;
                colorMatrix.Matrix22 = 1.0f;
                colorMatrix.Matrix33 = alpha;

                ImageAttributes imgAttr = new ImageAttributes();
                imgAttr.SetColorMatrix(colorMatrix);

                g.DrawImage(img, dest, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, imgAttr);
            }
        }

        public static Rectangle HAlignWithin(Size alignThis, Rectangle withinThis, ContentAlignment align)
        {
            if ((align & anyRight) != (ContentAlignment)0)
            {
                withinThis.X += (withinThis.Width - alignThis.Width);
            }
            else if ((align & anyCenter) != ((ContentAlignment)0))
            {
                withinThis.X += ((withinThis.Width - alignThis.Width + 1) / 2);
            }
            withinThis.Width = alignThis.Width;
            return withinThis;
        }

        public static Rectangle VAlignWithin(Size alignThis, Rectangle withinThis, ContentAlignment align)
        {
            if ((align & anyBottom) != ((ContentAlignment)0))
            {
                withinThis.Y += (withinThis.Height - alignThis.Height);
            }
            else if ((align & anyMiddle) != ((ContentAlignment)0))
            {
                withinThis.Y += ((withinThis.Height - alignThis.Height + 1) / 2);
            }
            withinThis.Height = alignThis.Height;
            return withinThis;
        }

        public static void DrawSeparator(Graphics g,
                                   bool vertical,
                                   Rectangle rect,
                                   Pen lightPen,
                                   Pen darkPen,
                                   int horizontalInset,
                                   bool rtl)
        {
            if (vertical)
            {
                int l = rect.Width / 2;
                int t = rect.Y;
                int b = rect.Bottom;

                // Draw vertical lines centered
                g.DrawLine(darkPen, l, t, l, b);
                g.DrawLine(lightPen, l + 1, t, l + 1, b);
            }
            else
            {
                int y = rect.Height / 2;
                int l = rect.X + (rtl ? 0 : horizontalInset);
                int r = rect.Right - (rtl ? horizontalInset : 0);

                // Draw horizontal lines centered
                g.DrawLine(darkPen, l, y, r, y);
                g.DrawLine(lightPen, l, y + 1, r, y + 1);
            }
        }
    }
}
