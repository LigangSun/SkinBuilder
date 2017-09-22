// ***************************************************************
//  SkinManager   version:  1.0   ? date: 02/13/2008
//  -------------------------------------------------------------
//  
//  -------------------------------------------------------------
//  Health121 Copyright (C) 2008 - All Rights Reserved
// ***************************************************************
//  
//  Author: Jeffery
//  
//  Purpurs:
// ***************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    public static class SkinManager
    {
        private static IRender render = new RenderEngine();
        private static ColorTable colorTable = new ColorTable();
        private static ImageTable imageTable = new ImageTable();

        private static ToolStripProfessionalRenderer professionalRenderer = new Office2007Renderer();

        static SkinManager()
        {
            render.SetColorTable(colorTable);
        }

        public static IRender Render
        {
            set { SkinManager.render = value; }
            get { return SkinManager.render; }
        }

        public static ColorTable ColorTable
        {
            get { return SkinManager.colorTable; }
            set { SkinManager.colorTable = value; }
        }

        public static ImageTable ImageTable
        {
            get { return SkinManager.imageTable; }
            set { SkinManager.imageTable = value; }
        }

        public static ToolStripProfessionalRenderer ProfessionalRenderer
        {
            get { return SkinManager.professionalRenderer; }
            set { SkinManager.professionalRenderer = value; }
        }

        public static void Init(IRender render)
        {
            if (render == null)
            {
                SkinManager.render = new RenderEngine();
                SkinManager.render.SetColorTable(colorTable);
                SkinManager.render.SetImageTable(imageTable);
            }
            else
            {
                SkinManager.render = render;
            }
        }

        public static void Init(IRender render, string colorTableFile)
        {
            if (render == null)
                SkinManager.render = new RenderEngine();
            else
                SkinManager.render = render;

            if (File.Exists(colorTableFile))
                colorTable.Load(colorTableFile);
            SkinManager.render.SetColorTable(colorTable);
            SkinManager.render.SetImageTable(imageTable);
        }
    }
}
