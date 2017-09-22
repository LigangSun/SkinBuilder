// ***************************************************************
//  IRender   version:  1.0   ? date: 02/12/2008
//  -------------------------------------------------------------
//  
//  -------------------------------------------------------------
//  Health121 Copyright (C) 2008 - All Rights Reserved
// ***************************************************************
//  
//  Author: Jeffery
//  
//  Purpurs: Define the render functions for controls
// ***************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public interface IRender
    {
        /*
         * For color table
         */ 
        ColorTable GetColorTable();
        void SetColorTable(ColorTable table);
        void SetImageTable(ImageTable table);

        /*
         * For button
         */
        void DrawButton(Object sender, Graphics g, ImageObject obj, Rectangle rect, ToolStripTextDirection textDirection);

        /*
         * For check box
         */
        void DrawCheckBox(Object sender, Graphics g, Rectangle rect, bool check);

        /*
         * For radio button 
         */
        void DrawRadioButton(Object sender, Graphics g, Rectangle rect, bool check);

        /*
         * For ComboBox
         */ 
        void DrawComboBoxBorder(Object sender, Graphics g, Rectangle rect);
        void DrawComboBoxButton(Object sender, Graphics g, Rectangle rect);
    }
}
