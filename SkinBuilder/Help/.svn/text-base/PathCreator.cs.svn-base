// ***************************************************************
//  PathCreator   version:  1.0   ? date: 02/12/2008
//  -------------------------------------------------------------
//  
//  -------------------------------------------------------------
//  Health121 Copyright (C) 2008 - All Rights Reserved
// ***************************************************************
//  
//  Author: Jeffery
//  
//  Purpurs: Create the special path
// ***************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public static class PathCreator
    {
        /// <summary>
        /// Creates a rounded rectangle from the specified rectangle and radius
        /// </summary>
        /// <param name="rectangle">Base rectangle</param>
        /// <param name="radius">Radius of the corners</param>
        /// <returns>Rounded rectangle as a GraphicsPath</returns>
        public static GraphicsPath CreateRoundRectangle(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            int l = rectangle.Left;
            int t = rectangle.Top;
            int w = rectangle.Width;
            int h = rectangle.Height;
            int d = radius << 1;

            path.AddArc(l, t, d, d, 180, 90); // topleft
            path.AddLine(l + radius, t, l + w - radius, t); // top
            path.AddArc(l + w - d, t, d, d, 270, 90); // topright
            path.AddLine(l + w, t + radius, l + w, t + h - radius); // right
            path.AddArc(l + w - d, t + h - d, d, d, 0, 90); // bottomright
            path.AddLine(l + w - radius, t + h, l + radius, t + h); // bottom
            path.AddArc(l, t + h - d, d, d, 90, 90); // bottomleft
            path.AddLine(l, t + h - radius, l, t + radius); // left
            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Creates a rectangle rounded on the top
        /// </summary>
        /// <param name="rectangle">Base rectangle</param>
        /// <param name="radius">Radius of the top corners</param>
        /// <returns>Rounded rectangle (on top) as a GraphicsPath object</returns>
        public static GraphicsPath CreateTopRoundRectangle(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            int l = rectangle.Left;
            int t = rectangle.Top;
            int w = rectangle.Width;
            int h = rectangle.Height;
            int d = radius << 1;

            path.AddArc(l, t, d, d, 180, 90); // topleft
            path.AddLine(l + radius, t, l + w - radius, t); // top
            path.AddArc(l + w - d, t, d, d, 270, 90); // topright
            path.AddLine(l + w, t + radius, l + w, t + h); // right
            path.AddLine(l + w, t + h, l, t + h); // bottom
            path.AddLine(l, t + h, l, t + radius); // left
            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Creates a rectangle rounded on the bottom
        /// </summary>
        /// <param name="rectangle">Base rectangle</param>
        /// <param name="radius">Radius of the bottom corners</param>
        /// <returns>Rounded rectangle (on bottom) as a GraphicsPath object</returns>
        public static GraphicsPath CreateBottomRoundRectangle(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            int l = rectangle.Left;
            int t = rectangle.Top;
            int w = rectangle.Width;
            int h = rectangle.Height;
            int d = radius << 1;

            path.AddLine(l + radius, t, l + w - radius, t); // top
            path.AddLine(l + w, t + radius, l + w, t + h - radius); // right
            path.AddArc(l + w - d, t + h - d, d, d, 0, 90); // bottomright
            path.AddLine(l + w - radius, t + h, l + radius, t + h); // bottom
            path.AddArc(l, t + h - d, d, d, 90, 90); // bottomleft
            path.AddLine(l, t + h - radius, l, t + radius); // left
            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Creates the glow of the buttons
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static GraphicsPath CreateBottomRadialPath(Rectangle rectangle)
        {
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = rectangle;
            rect.X -= rect.Width * .35f;
            rect.Y -= rect.Height * .15f;
            rect.Width *= 1.7f;
            rect.Height *= 2.3f;
            path.AddEllipse(rect);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// Creates the chevron for the overflow button
        /// </summary>
        /// <param name="overflowButtonSize"></param>
        /// <returns></returns>
        private static GraphicsPath CreateOverflowChevron(Size overflowButtonSize)
        {
            Rectangle r = new Rectangle(Point.Empty, overflowButtonSize);
            GraphicsPath path = new GraphicsPath();

            int segmentWidth = 3;
            int segmentHeight = 3;
            int segmentSeparation = 5;
            int chevronWidth = segmentWidth + segmentSeparation;
            int chevronHeight = segmentHeight * 2;
            int chevronLeft = (r.Width - chevronWidth) / 2;
            int chevronTop = (r.Height - chevronHeight) / 2;

            // Segment \
            path.AddLine(
                new Point(chevronLeft, chevronTop),
                new Point(chevronLeft + segmentWidth, chevronTop + segmentHeight));

            // Segment /
            path.AddLine(
                new Point(chevronLeft + segmentWidth, chevronTop + segmentHeight),
                new Point(chevronLeft, chevronTop + segmentHeight * 2));

            path.StartFigure();

            // Segment \
            path.AddLine(
                new Point(segmentSeparation + chevronLeft, chevronTop),
                new Point(segmentSeparation + chevronLeft + segmentWidth, chevronTop + segmentHeight));

            // Segment /
            path.AddLine(
                new Point(segmentSeparation + chevronLeft + segmentWidth, chevronTop + segmentHeight),
                new Point(segmentSeparation + chevronLeft, chevronTop + segmentHeight * 2));


            return path;
        }

        /// <summary>
        /// Gets a rounded rectangle representing the hole area of the toolstrip
        /// </summary>
        /// <param name="toolStrip"></param>
        /// <returns></returns>
        public static GraphicsPath GetToolStripRectangle(ToolStrip toolStrip, int radius)
        {
            return PathCreator.CreateRoundRectangle(
                new Rectangle(0, 0, toolStrip.Width - 1, toolStrip.Height - 1), radius);
        }
    }
}
