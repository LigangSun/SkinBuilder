// ***************************************************************
//  ColorTable   version:  1.0   ? date: 02/12/2008
//  -------------------------------------------------------------
//  
//  -------------------------------------------------------------
//  Health121 Copyright (C) 2008 - All Rights Reserved
// ***************************************************************
//  
//  Author: Jeffery
//  
//  Purpurs: The colors for all the controls
// ***************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Xml;
using System.Data;
using System.Globalization;

namespace ZLIS.SkinBuilder
{
    public class ColorTable
    {
#region Names

        private string[] propertyNames = new string[]
            {
                "BackgroundStart",
                "BackgroundEnd",
                "GlossyEffectStart",
                "GlossyEffectEnd",
                "BackgroundBorder",
                "BackgroundGlow",
                "Text",
                "ToolButtonOuterBorder",
                "ToolButtonInnerBorder",
                "ToolButtonInnerBorderPressed",
                "ToolButtonBorder",
                "ToolButtonFillStart",
                "ToolButtonFillEnd",
                "ToolButtonFillStartPressed",
                "ToolButtonFillEndPressed",
                "ToolGlow",
                "DropDownArrow",
                "MenuHighlight",
                "MenuHighlightStart",
                "MenuHighlightEnd",
                "MenuBackground",
                "MenuDark",
                "MenuLight",
                "MenuItemHeight",
                "SeparatorStart",
                "SeparatorEnd",
                "MenuText",
                "MenuItemHighlightBorder",
                "CheckedGlow",
                "CheckedGlowHot",
                "CheckedToolButtonFill",
                "CheckedToolButtonFillHot",
                "ComboBoxBorder",
                "ComboBoxBorderHot",
                "ComboBoxButtonBgStart",
                "ComboBoxButtonBgEnd",
                "ComboBoxButtonBgAngle",
                "ComboBoxButtonBgHotStart",
                "ComboBoxButtonBgHotEnd",
                "ComboBoxButtonBgHotAngle",
                "ComboBoxButtonBgPressedStart",
                "ComboBoxButtonBgPressedEnd",
                "ComboBoxButtonBgPressedAngle",
                "ComboBoxButtonBgDisabledStart",
                "ComboBoxButtonBgDisabledEnd",
                "comboBoxButtonBgDisabledAngle",

                "ButtonBase",
                "ButtonNormalColorDataStart",
                "ButtonNormalColorDataEnd",
                "ButtonNormalColorDataText",
                "ButtonNormalColorDataAngle",

                "ButtonHighlightColorDataStart",
                "ButtonHighlightColorDataEnd",
                "ButtonHighlightColorDataText",
                "ButtonHighlightColorDataAngle",

                "ButtonDownColorDataStart",
                "ButtonDownColorDataEnd",
                "ButtonDownColorDataText",
                "ButtonDownColorDataAngle",

                "ButtonDisableColorDataStart",
                "ButtonDisableColorDataEnd",
                "ButtonDisableColorDataText",
                "ButtonDisableColorDataAngle",

                "ButtonGlow",

                "ButtonOuterBorder",
                "ButtonInnerBorder",
                "ButtonInnerBorderPressed",
                "ButtonBorder",

                "MenuPageBackgroundStart",
                "MenuPageBackgroundEnd",
                "MenuPageBackgroundText",
                "MenuPageBackgroundAngle",

                "MenuPageBorder",
                "MenuPageBorderWidth",

                "GlossyEffect",
                "EnableGlow"
            };

#endregion

#region Fields
        protected Color bgStart;
        protected Color bgEnd;

        protected Color glossyStart;
        protected Color glossyEnd;

        protected Color bgBorder;

        protected Color bgGlow;

        protected Color text;

        protected Color toolButtonInnerBorder;
        protected Color toolButtonBorder;
        protected Color toolButtonOuterBorder;
        protected Color toolButtonFillStart;
        protected Color toolButtonFillStartPressed;
        protected Color toolGlow;
        protected Color toolButtonInnerBorderPressed;
        protected Color toolButtonFillEnd;
        protected Color toolButtonFillEndPressed;

        protected Color dropDownArrow;

        protected Color menuHighlight;
        protected Color menuItemHighlightBorder;
        protected Color menuHighlightStart;
        protected Color menuHighlightEnd;
        protected Color menuBackground;
        protected Color menuDark;
        protected Color menuLight;
        protected Color seperatorStart;
        protected Color seperatorEnd;
        protected Color menuText;

        protected int menuItemHeight;

        // Color page background
        protected ColorData menuPageBackground;
        protected ColorData menuPageBorder;
        protected int menuPageBorderWidth;

        protected Color checkedGlow;
        protected Color checkedToolButtonFill;
        protected Color checkedToolButtonFillHot;
        protected Color checkedGlowHot;

        protected Color comboBoxBorder;
        protected Color comboBoxBorderHot;

        protected Color comboBoxButtonBgStart;
        protected Color comboBoxButtonBgEnd;
        protected int comboBoxButtonBgAngle;
        protected Color comboBoxButtonBgHotStart;
        protected Color comboBoxButtonBgHotEnd;
        protected int comboBoxButtonBgHotAngle;
        protected Color comboBoxButtonBgPressedStart;
        protected Color comboBoxButtonBgPressedEnd;
        protected int comboBoxButtonBgPressedAngle;
        protected Color comboBoxButtonBgDisabledStart;
        protected Color comboBoxButtonBgDisabledEnd;
        protected int comboBoxButtonBgDisabledAngle;

        protected Color buttonInnerBorder;
        protected Color buttonInnerBorderPressed;
        protected Color buttonBorder;
        protected Color buttonOuterBorder;
        protected Color buttonGlow;
        protected Color buttonBase;
        protected Dictionary<ControlState, ColorData> buttonColorDictionary = new Dictionary<ControlState, ColorData>();

        protected bool glossyEffect = true;
        protected bool enalbeGlow = true;

#endregion

#region Contructor

        public ColorTable()
        {
            this.BackgroundStart = Color.Black;
            this.BackgroundEnd = Color.Black;

            this.GlossyEffectStart = Color.FromArgb(217, 0x68, 0x7C, 0xAC);
            this.GlossyEffectEnd = Color.FromArgb(74, 0xAA, 0xB5, 0xD0);

            this.BackgroundBorder = Color.FromArgb(0x85, 0x85, 0x87);
            this.BackgroundGlow = Color.FromArgb(0x43, 0x53, 0x7A);

            this.Text = Color.FromArgb(21, 66, 139);

            this.ToolButtonOuterBorder = Color.FromArgb(0x75, 0x7D, 0x95);
            this.ToolButtonInnerBorder = Color.FromArgb(0xBF, 0xC4, 0xCE);
            this.ToolButtonInnerBorderPressed = Color.FromArgb(0x4b, 0x4b, 0x4b);
            this.ToolButtonBorder = Color.FromArgb(0x03, 0x07, 0x0D);
            this.ToolButtonFillStart = Color.FromArgb(85, Color.White);
            this.ToolButtonFillEnd = Color.FromArgb(1, Color.White);
            this.ToolButtonFillStartPressed = Color.FromArgb(150, Color.Black);
            this.ToolButtonFillEndPressed = Color.FromArgb(100, Color.Black);

            this.ToolGlow = Color.FromArgb(0x30, 0x73, 0xCE);
            this.DropDownArrow = Color.White;

            this.MenuHighlight = Color.FromArgb(0xA8, 0xD8, 0xEB);
            this.MenuHighlightStart = Color.FromArgb(25, MenuHighlight);
            this.MenuHighlightEnd = Color.FromArgb(102, MenuHighlight);
            this.MenuBackground = Color.FromArgb(0xF1, 0xF1, 0xF1);
            this.MenuDark = Color.FromArgb(0xE2, 0xE3, 0xE3);
            this.MenuLight = Color.White;

            this.MenuItemHeight = 36;

            this.SeparatorStart = this.BackgroundEnd;
            this.SeparatorEnd = this.GlossyEffectStart;

            this.MenuText = Color.Black;

            this.CheckedGlow = Color.FromArgb(0x57, 0xC6, 0xEF);
            this.CheckedGlowHot = Color.FromArgb(0x70, 0xD4, 0xFF);
            this.CheckedToolButtonFill = Color.FromArgb(0x18, 0x38, 0x9E);
            this.CheckedToolButtonFillHot = Color.FromArgb(0x0F, 0x3A, 0xBF);

            this.ComboBoxBorder = Color.Black;
            this.ComboBoxBorderHot = Color.Gray;

            this.ComboBoxButtonBgStart = Color.White;
            this.ComboBoxButtonBgEnd = Color.White;
            this.comboBoxButtonBgAngle = 90;
            this.ComboBoxButtonBgHotStart = Color.FromArgb(255, 247, 209, 117);
            this.ComboBoxButtonBgHotEnd = Color.FromArgb(255, 250, 235, 165);
            this.comboBoxButtonBgHotAngle = 90;
            this.ComboBoxButtonBgPressedStart = Color.FromArgb(255, 246, 172, 81);
            this.ComboBoxButtonBgPressedEnd = Color.FromArgb(255, 250, 233, 179);
            this.comboBoxButtonBgPressedAngle = 90;
            this.ComboBoxButtonBgDisabledStart = Color.White;
            this.ComboBoxButtonBgDisabledEnd = Color.White;
            this.comboBoxButtonBgDisabledAngle = 90;

            this.ButtonBase = Color.FromArgb(0x30, 0x73, 0xCE);
            this.ButtonNormalColorData = new ColorData(Color.FromArgb(85, Color.White), Color.FromArgb(1, Color.White), Color.White, 90);
            this.ButtonHighlightColorData = new ColorData(Color.FromArgb(85, Color.White), Color.FromArgb(100, Color.Black), Color.White, 90);
            this.ButtonDownColorData = new ColorData(Color.FromArgb(150, Color.Black), Color.FromArgb(1, Color.White), Color.White, 90);
            this.ButtonDisableColorData = new ColorData(Color.FromArgb(100, Color.Black), Color.FromArgb(1, Color.White), Color.Gray, 90);

            this.ButtonGlow = Color.FromArgb(0x30, 0x73, 0xCE);

            this.ButtonOuterBorder = Color.FromArgb(0x75, 0x7D, 0x95);
            this.ButtonInnerBorder = Color.FromArgb(0xBF, 0xC4, 0xCE);
            this.ButtonInnerBorderPressed = Color.FromArgb(0x4b, 0x4b, 0x4b);
            this.ButtonBorder = Color.FromArgb(0x03, 0x07, 0x0D);

            this.MenuPageBackground = new ColorData(Color.FromArgb(245, 247, 249), Color.FromArgb(245, 247, 249), Color.Black, 90);
            this.MenuPageBorder = new ColorData(Color.FromArgb(202, 222, 245), Color.FromArgb(202, 222, 245), Color.Black, 90);
            this.MenuPageBorderWidth = 6;
        }

#endregion

#region Properties

        [Category("Checked Button(Not check box)")]
        public virtual Color CheckedGlowHot
        {
            get { return this.checkedGlowHot; }
            set { this.checkedGlowHot = value; }
        }

        [Category("Checked Button(Not check box)")]
        public virtual Color CheckedToolButtonFillHot
        {
            get { return this.checkedToolButtonFillHot; }
            set { this.checkedToolButtonFillHot = value; }
        }

        [Category("Checked Button(Not check box)")]
        public virtual Color CheckedToolButtonFill
        {
            get { return this.checkedToolButtonFill; }
            set { this.checkedToolButtonFill = value; }
        }

        [Category("Checked Button(Not check box)")]
        public virtual Color CheckedGlow
        {
            get { return this.checkedGlow; }
            set { this.checkedGlow = value; }
        }

        [Category("Menu Color")]
        public virtual Color MenuText
        {
            get { return this.menuText; }
            set { this.menuText = value; }
        }

        [Category("Menu Color")]
        public virtual Color SeparatorStart
        {
            get { return this.seperatorStart; }
            set { this.seperatorStart = value; }
        }

        [Category("Menu Color")]
        public virtual Color SeparatorEnd
        {
            get { return this.seperatorEnd; }
            set { this.seperatorEnd = value; }
        }

        [Category("Menu Color")]
        public virtual Color MenuLight
        {
            get { return this.menuLight; }
            set { this.menuLight = value; }
        }

        [Category("Menu Color")]
        public virtual Color MenuDark
        {
            get { return this.menuDark; }
            set { this.menuDark = value; }
        }

        [Category("Menu Color")]
        public virtual Color MenuBackground
        {
            get { return this.menuBackground; }
            set { this.menuBackground = value; }
        }

        [Category("Menu Color")]
        public virtual Color MenuItemHighlightBorder
        {
            get { return this.menuItemHighlightBorder; }
            set { this.menuItemHighlightBorder = value; }
        }

        [Category("Menu Color")]
        public virtual Color MenuHighlightEnd
        {
            get { return this.menuHighlightEnd; }
            set { this.menuHighlightEnd = value; }
        }

        [Category("Menu Color")]
        public virtual Color MenuHighlightStart
        {
            get { return this.menuHighlightStart; }
            set { this.menuHighlightStart = value; }
        }

        [Category("Menu Color")]
        public virtual Color MenuHighlight
        {
            get { return this.menuHighlight; }
            set { this.menuHighlight = value; }
        }

        [DefaultValue(20)]
        [Category("Menu Color")]
        public virtual int MenuItemHeight
        {
            get { return this.menuItemHeight; }
            set { this.menuItemHeight = value; }
        }

        /// <summary>
        /// Gets or sets the color for the dropwown arrow
        /// </summary>
        public virtual Color DropDownArrow
        {
            get { return this.dropDownArrow; }
            set { this.dropDownArrow = value; }
        }

        /// <summary>
        /// Gets or sets the south color of the toolButton fill when pressed
        /// </summary>
        [Category("Tool Button and Menu button(The button on Toolbar and menu)")]
        public virtual Color ToolButtonFillStartPressed
        {
            get { return this.toolButtonFillStartPressed; }
            set { this.toolButtonFillStartPressed = value; }
        }

        /// <summary>
        /// Gets or sets the south color of the toolButton fill
        /// </summary>
        [Category("Tool Button and Menu button(The button on Toolbar and menu)")]
        public virtual Color ToolButtonFillStart
        {
            get { return this.toolButtonFillStart; }
            set { this.toolButtonFillStart = value; }
        }

        /// <summary>
        /// Gets or sets the color of the inner border when pressed
        /// </summary>
        [Category("Tool Button and Menu button(The button on Toolbar and menu)")]
        public virtual Color ToolButtonInnerBorderPressed
        {
            get { return this.toolButtonInnerBorderPressed; }
            set { this.toolButtonInnerBorderPressed = value; }
        }

        /// <summary>
        /// Gets or sets the glow color
        /// </summary>
        [Category("Tool Button and Menu button(The button on Toolbar and menu)")]
        public virtual Color ToolGlow
        {
            get { return this.toolGlow; }
            set { this.toolGlow = value; }
        }

        /// <summary>
        /// Gets or sets the toolButtons fill color
        /// </summary>
        [Category("Tool Button and Menu button(The button on Toolbar and menu)")]
        public virtual Color ToolButtonFillEnd
        {
            get { return this.toolButtonFillEnd; }
            set { this.toolButtonFillEnd = value; }
        }

        /// <summary>
        /// Gets or sets the toolButtons fill color when pressed
        /// </summary>
        [Category("Tool Button and Menu button(The button on Toolbar and menu)")]
        public virtual Color ToolButtonFillEndPressed
        {
            get { return this.toolButtonFillEndPressed; }
            set { this.toolButtonFillEndPressed = value; }
        }

        /// <summary>
        /// Gets or sets the toolButtons inner border color
        /// </summary>
        [Category("Tool Button and Menu button(The button on Toolbar and menu)")]
        public virtual Color ToolButtonInnerBorder
        {
            get { return this.toolButtonInnerBorder; }
            set { this.toolButtonInnerBorder = value; }
        }

        /// <summary>
        /// Gets or sets the toolButtons border color
        /// </summary>
        [Category("Tool Button and Menu button(The button on Toolbar and menu)")]
        public virtual Color ToolButtonBorder
        {
            get { return this.toolButtonBorder; }
            set { this.toolButtonBorder = value; }
        }

        /// <summary>
        /// Gets or sets the toolButtons outer border color
        /// </summary>
        [Category("Tool Button and Menu button(The button on Toolbar and menu)")]
        public virtual Color ToolButtonOuterBorder
        {
            get { return this.toolButtonOuterBorder; }
            set { this.toolButtonOuterBorder = value; }
        }

        /// <summary>
        /// Gets or sets the color of the text
        /// </summary>
        public virtual Color Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        /// <summary>
        /// Gets or sets the background glow color
        /// </summary>
        [Category("Toolbar and Menubar")]
        public virtual Color BackgroundGlow
        {
            get { return this.bgGlow; }
            set { this.bgGlow = value; }
        }

        /// <summary>
        /// Gets or sets the color of the background border
        /// </summary>
        [Category("Toolbar and Menubar")]
        public virtual Color BackgroundBorder
        {
            get { return this.bgBorder; }
            set { this.bgBorder = value; }
        }

        /// <summary>
        /// Background north part
        /// </summary>
        [Category("Toolbar and Menubar")]
        public virtual Color BackgroundStart
        {
            get { return this.bgStart; }
            set { this.bgStart = value; }
        }

        /// <summary>
        /// Background south color
        /// </summary>
        [Category("Toolbar and Menubar")]
        public virtual Color BackgroundEnd
        {
            get { return this.bgEnd; }
            set { this.bgEnd = value; }
        }

        /// <summary>
        /// Gets ors sets the glossy effect north color
        /// </summary>
        [Category("Glossy effect for all controls")]
        public virtual Color GlossyEffectStart
        {
            get { return this.glossyStart; }
            set { this.glossyStart = value; }
        }

        /// <summary>
        /// Gets or sets the glossy effect south color
        /// </summary>
        [Category("Glossy effect for all controls")]
        public virtual Color GlossyEffectEnd
        {
            get { return this.glossyEnd; }
            set { this.glossyEnd = value; }
        }

        [Category("Glossy effect for all controls")]
        public virtual bool GlossyEffect
        {
            get { return this.glossyEffect; }
            set { this.glossyEffect = value; }
        }

        #region Glow State
        [Category("Enable/Disable Glow")]
        public virtual bool EnalbeGlow
        {
            get { return this.enalbeGlow; }
            set { this.enalbeGlow = value; }
        }
        #endregion

        #region  ComboBox color config

        [Category("ComboBox")]
        public virtual Color ComboBoxBorder
        {
            get { return this.comboBoxBorder; }
            set { this.comboBoxBorder = value; }
        }

        [Category("ComboBox")]
        public virtual Color ComboBoxBorderHot
        {
            get { return this.comboBoxBorderHot; }
            set { this.comboBoxBorderHot = value; }
        }

        [Category("ComboBox")]
        public virtual Color ComboBoxButtonBgStart
        {
            get { return this.comboBoxButtonBgStart; }
            set { this.comboBoxButtonBgStart = value; }
        }

        [Category("ComboBox")]
        public virtual Color ComboBoxButtonBgEnd
        {
            get { return this.comboBoxButtonBgEnd; }
            set { this.comboBoxButtonBgEnd = value; }
        }

        [Category("ComboBox")]
        public virtual int ComboBoxButtonBgAngle
        {
            get { return this.comboBoxButtonBgAngle; }
            set { this.comboBoxButtonBgAngle = value; }
        }

        [Category("ComboBox")]
        public virtual Color ComboBoxButtonBgHotStart
        {
            get { return this.comboBoxButtonBgHotStart; }
            set { this.comboBoxButtonBgHotStart = value; }
        }

        [Category("ComboBox")]
        public virtual Color ComboBoxButtonBgHotEnd
        {
            get { return this.comboBoxButtonBgHotEnd; }
            set { this.comboBoxButtonBgHotEnd = value; }
        }

        [Category("ComboBox")]
        public virtual int ComboBoxButtonBgHotAngle
        {
            get { return this.comboBoxButtonBgHotAngle; }
            set { this.comboBoxButtonBgHotAngle = value; }
        }

        [Category("ComboBox")]
        public virtual Color ComboBoxButtonBgPressedStart
        {
            get { return this.comboBoxButtonBgPressedStart; }
            set { this.comboBoxButtonBgPressedStart = value; }
        }

        [Category("ComboBox")]
        public virtual Color ComboBoxButtonBgPressedEnd
        {
            get { return this.comboBoxButtonBgPressedEnd; }
            set { this.comboBoxButtonBgPressedEnd = value; }
        }

        [Category("ComboBox")]
        public virtual int ComboBoxButtonBgPressedAngle
        {
            get { return this.comboBoxButtonBgPressedAngle; }
            set { this.comboBoxButtonBgPressedAngle = value; }
        }

        [Category("ComboBox")]
        public virtual Color ComboBoxButtonBgDisabledStart
        {
            get { return this.comboBoxButtonBgDisabledStart; }
            set { this.comboBoxButtonBgDisabledStart = value; }
        }

        [Category("ComboBox")]
        public virtual Color ComboBoxButtonBgDisabledEnd
        {
            get { return this.comboBoxButtonBgDisabledEnd; }
            set { this.comboBoxButtonBgDisabledEnd = value; }
        }

        [Category("ComboBox")]
        public virtual int ComboBoxButtonBgDisabledAngle
        {
            get { return this.comboBoxButtonBgDisabledAngle; }
            set { this.comboBoxButtonBgDisabledAngle = value; }
        }
#endregion

#region Button color config

        [Category("Button")]
        public virtual Color ButtonBase
        {
            get { return this.buttonBase; }
            set { this.buttonBase = value; }
        }

        [Category("Button")]
        public virtual ColorData ButtonNormalColorData
        {
            get { return this.buttonColorDictionary[ControlState.Normal]; }
            set { this.buttonColorDictionary[ControlState.Normal] = value; }
        }

        [Category("Button")]
        public virtual ColorData ButtonHighlightColorData
        {
            get { return this.buttonColorDictionary[ControlState.Highlight]; }
            set { this.buttonColorDictionary[ControlState.Highlight] = value; }
        }

        [Category("Button")]
        public virtual ColorData ButtonDownColorData
        {
            get { return this.buttonColorDictionary[ControlState.Down]; }
            set { this.buttonColorDictionary[ControlState.Down] = value; }
        }

        [Category("Button")]
        public virtual ColorData ButtonDisableColorData
        {
            get { return this.buttonColorDictionary[ControlState.Disable]; }
            set { this.buttonColorDictionary[ControlState.Disable] = value; }
        }

        [Category("Button")]
        public virtual Color ButtonInnerBorder
        {
            get { return this.buttonInnerBorder; }
            set { this.buttonInnerBorder = value; }
        }

        [Category("Button")]
        public virtual Color ButtonInnerBorderPressed
        {
            get { return this.buttonInnerBorderPressed; }
            set { this.buttonInnerBorderPressed = value; }
        }

        [Category("Button")]
        public virtual Color ButtonOuterBorder
        {
            get { return this.buttonOuterBorder; }
            set { this.buttonOuterBorder = value; }
        }

        [Category("Button")]
        public virtual Color ButtonBorder
        {
            get { return this.buttonBorder; }
            set { this.buttonBorder = value; }
        }

        [Category("Button")]
        public virtual Color ButtonGlow
        {
            get { return this.buttonGlow; }
            set { this.buttonGlow = value; }
        }

#endregion

#region Color page background

        [Category("Menu Color")]
        public virtual ColorData MenuPageBackground
        {
            get { return this.menuPageBackground; }
            set { this.menuPageBackground = value; }
        }

        [Category("Menu Color")]
        public virtual ColorData MenuPageBorder 
        {
            get { return this.menuPageBorder; }
            set { this.menuPageBorder = value; }
        }

        [DefaultValue(6)]
        [Category("Menu Color")]
        public virtual int MenuPageBorderWidth
        {
            get { return this.menuPageBorderWidth; }
            set { this.menuPageBorderWidth = value; }
        }
#endregion

#endregion

        protected Color String2Color(Object obj)
        {
            string str = (string)obj;

            int pos = str.IndexOf('[');
            str = str.Substring(pos + 1, str.Length - pos - 2);

            string[] colorPart = str.Split(',');
            if (colorPart.Length == 1)
            {
                return Color.FromName(colorPart[0]);
            }
            else
            {
                string A = colorPart[0].Substring(colorPart[0].IndexOf('=') + 1);
                string R = colorPart[1].Substring(colorPart[1].IndexOf('=') + 1);
                string G = colorPart[2].Substring(colorPart[2].IndexOf('=') + 1);
                string B = colorPart[3].Substring(colorPart[3].IndexOf('=') + 1);
                return Color.FromArgb(Convert.ToInt32(A),
                    Convert.ToInt32(R),
                    Convert.ToInt32(G),
                    Convert.ToInt32(B));
            }
        }

        public virtual void Load(string file)
        {
            using (DataSet ds = new DataSet())
            {
                ds.ReadXml(file);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this.BackgroundStart = String2Color(dr["BackgroundStart"]);
                    this.BackgroundEnd = String2Color(dr["BackgroundEnd"]);

                    this.GlossyEffectStart = String2Color(dr["GlossyEffectStart"]);
                    this.GlossyEffectEnd = String2Color(dr["GlossyEffectEnd"]);

                    this.BackgroundBorder = String2Color(dr["BackgroundBorder"]);
                    this.BackgroundGlow = String2Color(dr["BackgroundGlow"]);

                    this.Text = String2Color(dr["Text"]);

                    this.ToolButtonOuterBorder = String2Color(dr["ToolButtonOuterBorder"]);
                    this.ToolButtonInnerBorder = String2Color(dr["ToolButtonInnerBorder"]);
                    this.ToolButtonInnerBorderPressed = String2Color(dr["ToolButtonInnerBorderPressed"]);
                    this.ToolButtonBorder = String2Color(dr["ToolButtonBorder"]);
                    this.ToolButtonFillStart = String2Color(dr["ToolButtonFillStart"]);
                    this.ToolButtonFillEnd = String2Color(dr["ToolButtonFillEnd"]);
                    this.ToolButtonFillStartPressed = String2Color(dr["ToolButtonFillStartPressed"]);
                    this.ToolButtonFillEndPressed = String2Color(dr["ToolButtonFillEndPressed"]);

                    this.ToolGlow = String2Color(dr["ToolGlow"]);
                    this.DropDownArrow = String2Color(dr["DropDownArrow"]);

                    this.MenuHighlight = String2Color(dr["MenuHighlight"]);
                    this.MenuHighlightStart = String2Color(dr["MenuHighlightStart"]);
                    this.MenuHighlightEnd = String2Color(dr["MenuHighlightEnd"]);
                    this.MenuBackground = String2Color(dr["MenuBackground"]);
                    this.MenuDark = String2Color(dr["MenuDark"]);
                    this.MenuLight = String2Color(dr["MenuLight"]);

                    this.MenuItemHeight = Convert.ToInt32((String)dr["MenuItemHeight"]);

                    this.SeparatorStart = String2Color(dr["SeparatorStart"]);
                    this.SeparatorEnd = String2Color(dr["SeparatorEnd"]);

                    this.MenuText = String2Color(dr["MenuText"]);

                    try
                    {
                        this.MenuItemHighlightBorder = String2Color(dr["MenuItemHighlightBorder"]);
                    }
                    catch (Exception)
                    {

                    }

                    this.CheckedGlow = String2Color(dr["CheckedGlow"]);
                    this.CheckedGlowHot = String2Color(dr["CheckedGlowHot"]);
                    this.CheckedToolButtonFill = String2Color(dr["CheckedToolButtonFill"]);
                    this.CheckedToolButtonFillHot = String2Color(dr["CheckedToolButtonFillHot"]);

                    this.ComboBoxBorder = String2Color(dr["ComboBoxBorder"]);
                    this.ComboBoxBorderHot = String2Color(dr["ComboBoxBorderHot"]);

                    this.ComboBoxButtonBgStart = String2Color(dr["ComboBoxButtonBgStart"]);
                    this.ComboBoxButtonBgEnd = String2Color(dr["ComboBoxButtonBgEnd"]);
                    this.comboBoxButtonBgAngle = Convert.ToInt32((String)dr["comboBoxButtonBgAngle"]);
                    this.ComboBoxButtonBgHotStart = String2Color(dr["ComboBoxButtonBgHotStart"]);
                    this.ComboBoxButtonBgHotEnd = String2Color(dr["ComboBoxButtonBgHotEnd"]);
                    this.comboBoxButtonBgHotAngle = Convert.ToInt32((String)dr["comboBoxButtonBgHotAngle"]);
                    this.ComboBoxButtonBgPressedStart = String2Color(dr["ComboBoxButtonBgPressedStart"]);
                    this.ComboBoxButtonBgPressedEnd = String2Color(dr["ComboBoxButtonBgPressedEnd"]);
                    this.comboBoxButtonBgPressedAngle = Convert.ToInt32((String)dr["comboBoxButtonBgPressedAngle"]);
                    this.ComboBoxButtonBgDisabledStart = String2Color(dr["ComboBoxButtonBgDisabledStart"]);
                    this.ComboBoxButtonBgDisabledEnd = String2Color(dr["ComboBoxButtonBgDisabledEnd"]);
                    this.comboBoxButtonBgDisabledAngle = Convert.ToInt32((String)dr["comboBoxButtonBgDisabledAngle"]);

                    this.ButtonBase = String2Color(dr["ButtonBase"]);
                    this.ButtonNormalColorData = new ColorData(String2Color(dr["ButtonNormalColorDataStart"]), String2Color(dr["ButtonNormalColorDataEnd"]), String2Color(dr["ButtonNormalColorDataText"]), Convert.ToInt32((String)dr["ButtonNormalColorDataAngle"]));
                    this.ButtonHighlightColorData = new ColorData(String2Color(dr["ButtonHighlightColorDataStart"]), String2Color(dr["ButtonHighlightColorDataEnd"]), String2Color(dr["ButtonHighlightColorDataText"]), Convert.ToInt32((String)dr["ButtonHighlightColorDataAngle"]));
                    this.ButtonDownColorData = new ColorData(String2Color(dr["ButtonDownColorDataStart"]), String2Color(dr["ButtonDownColorDataEnd"]), String2Color(dr["ButtonDownColorDataText"]), Convert.ToInt32((String)dr["ButtonDownColorDataAngle"]));
                    this.ButtonDisableColorData = new ColorData(String2Color(dr["ButtonDisableColorDataStart"]), String2Color(dr["ButtonDisableColorDataEnd"]), String2Color(dr["ButtonDisableColorDataText"]), Convert.ToInt32((String)dr["ButtonDisableColorDataAngle"]));

                    this.ButtonGlow = String2Color(dr["ButtonGlow"]);

                    this.ButtonOuterBorder = String2Color(dr["ButtonOuterBorder"]);
                    this.ButtonInnerBorder = String2Color(dr["ButtonInnerBorder"]);
                    this.ButtonInnerBorderPressed = String2Color(dr["ButtonInnerBorderPressed"]);
                    this.ButtonBorder = String2Color(dr["ButtonBorder"]);

                    this.MenuPageBackground = new ColorData(String2Color(dr["MenuPageBackgroundStart"]), String2Color(dr["MenuPageBackgroundEnd"]), String2Color(dr["MenuPageBackgroundText"]), Convert.ToInt32((String)dr["MenuPageBackgroundAngle"]));
               //     this.MenuPageBorder = new ColorData((Color)dr["MenuPageBorderStart"], (Color)dr["MenuPageBorderEnd"], (Color)dr["MenuPageBorderText"], (int)dr["MenuPageBorderAngle"]);
                    this.MenuPageBorderWidth = Convert.ToInt32((String)dr["MenuPageBorderWidth"]);

                    try
                    {
                        this.glossyEffect = bool.Parse((string)dr["GlossyEffect"]);
                        this.enalbeGlow = bool.Parse((string)dr["EnableGlow"]);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        public virtual void Save(string file)
        {
            using (DataSet ds = new DataSet())
            {
                DataTable dt = ds.Tables.Add("ColorTable");

                foreach (string name in this.propertyNames)
                {
                    dt.Columns.Add(name);
                }

                ds.AcceptChanges();

                DataRow dr = ds.Tables[0].NewRow();

                dr["BackgroundStart"] = this.BackgroundStart;
                dr["BackgroundEnd"] = this.BackgroundEnd;
                dr["GlossyEffectStart"] = this.GlossyEffectStart;
                dr["GlossyEffectEnd"] = this.GlossyEffectEnd;
                dr["BackgroundBorder"] = this.BackgroundBorder;
                dr["BackgroundGlow"] = this.BackgroundGlow;
                dr["Text"] = this.Text;
                dr["ToolButtonOuterBorder"] = this.ToolButtonOuterBorder;
                dr["ToolButtonInnerBorder"] = this.ToolButtonInnerBorder;
                dr["ToolButtonInnerBorderPressed"] = this.ToolButtonInnerBorderPressed;
                dr["ToolButtonBorder"] = this.ToolButtonBorder;
                dr["ToolButtonFillStart"] = this.ToolButtonFillStart;
                dr["ToolButtonFillEnd"] = this.ToolButtonFillEnd;
                dr["ToolButtonFillStartPressed"] = this.ToolButtonFillStartPressed;
                dr["ToolButtonFillEndPressed"] = this.ToolButtonFillEndPressed;
                dr["ToolGlow"] = this.ToolGlow;
                dr["DropDownArrow"] = this.DropDownArrow;
                dr["MenuHighlight"] = this.MenuHighlight;
                dr["MenuHighlightStart"] = this.MenuHighlightStart;
                dr["MenuHighlightEnd"] = this.MenuHighlightEnd;
                dr["MenuBackground"] = this.MenuBackground;
                dr["MenuDark"] = this.MenuDark;
                dr["MenuLight"] = this.MenuLight;
                dr["MenuItemHeight"] = this.MenuItemHeight;
                dr["SeparatorStart"] = this.SeparatorStart;
                dr["SeparatorEnd"] = this.SeparatorEnd;
                dr["MenuText"] = this.MenuText;
                dr["MenuItemHighlightBorder"] = this.MenuItemHighlightBorder;
                dr["CheckedGlow"] = this.CheckedGlow;
                dr["CheckedGlowHot"] = this.CheckedGlowHot;
                dr["CheckedToolButtonFill"] = this.CheckedToolButtonFill;
                dr["CheckedToolButtonFillHot"] = this.CheckedToolButtonFillHot;
                dr["ComboBoxBorder"] = this.ComboBoxBorder;
                dr["ComboBoxBorderHot"] = this.ComboBoxBorderHot;
                dr["ComboBoxButtonBgStart"] = this.ComboBoxButtonBgStart;
                dr["ComboBoxButtonBgEnd"] = this.ComboBoxButtonBgEnd;
                dr["comboBoxButtonBgAngle"] = this.comboBoxButtonBgAngle;
                dr["ComboBoxButtonBgHotStart"] = this.ComboBoxButtonBgHotStart;
                dr["ComboBoxButtonBgHotEnd"] = this.ComboBoxButtonBgEnd;
                dr["ComboBoxButtonBgHotAngle"] = this.ComboBoxButtonBgHotAngle;
                dr["ComboBoxButtonBgPressedStart"] = this.ComboBoxButtonBgPressedStart;
                dr["ComboBoxButtonBgPressedEnd"] = this.ComboBoxButtonBgPressedEnd;
                dr["ComboBoxButtonBgPressedAngle"] = this.ComboBoxButtonBgPressedAngle;
                dr["ComboBoxButtonBgDisabledStart"] = this.ComboBoxButtonBgDisabledStart;
                dr["ComboBoxButtonBgDisabledEnd"] = this.ComboBoxButtonBgDisabledEnd;
                dr["ComboBoxButtonBgDisabledAngle"] = this.ComboBoxButtonBgDisabledAngle;

                dr["ButtonBase"] = this.ButtonBase;
                dr["ButtonNormalColorDataStart"] = this.ButtonNormalColorData.Start;
                dr["ButtonNormalColorDataEnd"] = this.ButtonNormalColorData.End;
                dr["ButtonNormalColorDataText"] = this.ButtonNormalColorData.Text;
                dr["ButtonNormalColorDataAngle"] = this.ButtonNormalColorData.Angle;

                dr["ButtonHighlightColorDataStart"] = this.ButtonHighlightColorData.Start;
                dr["ButtonHighlightColorDataEnd"] = this.ButtonHighlightColorData.End;
                dr["ButtonHighlightColorDataText"] = this.ButtonHighlightColorData.Text;
                dr["ButtonHighlightColorDataAngle"] = this.ButtonHighlightColorData.Angle;

                dr["ButtonDownColorDataStart"] = this.ButtonDownColorData.Start;
                dr["ButtonDownColorDataEnd"] = this.ButtonDownColorData.End;
                dr["ButtonDownColorDataText"] = this.ButtonDownColorData.Text;
                dr["ButtonDownColorDataAngle"] = this.ButtonDownColorData.Angle;

                dr["ButtonDisableColorDataStart"] = this.ButtonDisableColorData.Start;
                dr["ButtonDisableColorDataEnd"] = this.ButtonDisableColorData.End;
                dr["ButtonDisableColorDataText"] = this.ButtonDisableColorData.Text;
                dr["ButtonDisableColorDataAngle"] = this.ButtonDisableColorData.Angle;

                dr["ButtonGlow"] = this.ButtonGlow;

                dr["ButtonOuterBorder"] = this.ButtonOuterBorder;
                dr["ButtonInnerBorder"] = this.ButtonInnerBorder;
                dr["ButtonInnerBorderPressed"] = this.ButtonInnerBorderPressed;
                dr["ButtonBorder"] = this.buttonBorder;

                dr["MenuPageBackgroundStart"] = this.MenuPageBackground.Start;
                dr["MenuPageBackgroundEnd"] = this.MenuPageBackground.End;
                dr["MenuPageBackgroundText"] = this.MenuPageBackground.Text;
                dr["MenuPageBackgroundAngle"] = this.MenuPageBackground.Angle;

                dr["MenuPageBorder"] = this.MenuPageBorder;
                dr["MenuPageBorderWidth"] = this.MenuPageBorderWidth;

                dr["GlossyEffect"] = this.glossyEffect;
                dr["EnableGlow"] = this.enalbeGlow;

                ds.Tables[0].Rows.Add(dr);

                ds.AcceptChanges();

                ds.WriteXml(file);
            }
        }

        [TypeConverter(typeof(ColorTableConverter))]
        public class ColorData
        {
            private Color start = Color.DarkBlue;
            private Color end = Color.LightBlue;
            private Color text = Color.Black;
            private int angle = 90;

            public ColorData()
            {

            }

            public ColorData(Color start, Color end, Color text, int angle)
            {
                this.start = start;
                this.end = end;
                this.text = text;
                this.angle = angle;
            }

            [DefaultValue(typeof(Color), "DrakBlue")]
            public Color Start
            {
                get { return this.start; }
                set { this.start = value; }
            }

            [DefaultValue(typeof(Color), "LightBlue")]
            public Color End
            {
                get { return this.end; }
                set { this.end = value; }
            }

            [DefaultValue(typeof(Color), "Black")]
            public Color Text
            {
                get { return this.text; }
                set { this.text = value; }
            }

            [DefaultValue(90)]
            public int Angle
            {
                get { return this.angle; }
                set { this.angle = value; }
            }
        }

        internal class ColorTableConverter : ExpandableObjectConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type t)
            {
                if (t == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, t);
            }

            public override object ConvertFrom(ITypeDescriptorContext context,  CultureInfo info, object value) 
            {
                if (value is string)
                {
                    try
                    {
                        string s = (string)value;
                    }
                    catch (Exception)
                    {

                    }
                }
                return base.ConvertFrom(context, info, value);
           }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
            {
                return base.ConvertTo(context, culture, value, destType);
            }
        }
    }
}
