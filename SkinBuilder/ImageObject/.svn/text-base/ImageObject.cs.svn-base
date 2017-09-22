using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace ZLIS.SkinBuilder
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ImageObject
    {
        private Padding splitMargin;
        private Dictionary<ControlState, Bitmap> bitmapDictionary = new Dictionary<ControlState, Bitmap>();
        private Dictionary<ControlState, string> bitmapFileNameDictionary = new Dictionary<ControlState, string>();

        public ImageObject()
        {

        }

        public ImageObject(Padding margin)
        {
            splitMargin = margin;
        }

        #region Properties

        public bool IsEmpty
        {
            get
            {
                if (this.bitmapDictionary.Count > 0)
                {
                    Dictionary<ControlState, Bitmap>.KeyCollection keys = this.bitmapDictionary.Keys;
                    foreach (ControlState state in keys)
                    {
                        if (this.bitmapDictionary[state] != null)
                            return false;
                    }
                }

                return true;
            }
        }

        public Bitmap this[ControlState index]
        {
            get 
            {
                if (!this.bitmapDictionary.ContainsKey(index))
                    return null;

                return this.bitmapDictionary[index]; 
            }

            set
            {
                this.bitmapDictionary[index] = value;
            }
        }

        public int Width 
        {
            get 
            {
                int width = 0;
                if (this[ControlState.Normal] != null)
                    width = this[ControlState.Normal].Width;
                else if (this[ControlState.Highlight] != null)
                    width = this[ControlState.Highlight].Width;
                else if (this[ControlState.Down] != null)
                    width = this[ControlState.Down].Width;

                return width;
            }
        }

        public int Height
        {
            get 
            {
                int height = 0;
                if (this[ControlState.Normal] != null)
                    height = this[ControlState.Normal].Height;
                else if (this[ControlState.Highlight] != null)
                    height = this[ControlState.Highlight].Height;
                else if (this[ControlState.Down] != null)
                    height = this[ControlState.Down].Height;

                return height;
            }
        }

        public Padding SplitMargin 
        {
            get { return this.splitMargin; }
            set { this.splitMargin = value; }
        }

        [DefaultValue(null)]
        public Bitmap NormalBitmap
        {
            set
            {
                this.bitmapDictionary[ControlState.Normal] = value;
            }
            get
            {
                if (!this.bitmapDictionary.ContainsKey(ControlState.Normal))
                    return null;
                return this.bitmapDictionary[ControlState.Normal];
            }
        }

        [DefaultValue(null)]
        public Bitmap HighlightBitmap
        {
            set { this.bitmapDictionary[ControlState.Highlight] = value; }
            get
            {
                if (!this.bitmapDictionary.ContainsKey(ControlState.Highlight))
                    return null;
                return this.bitmapDictionary[ControlState.Highlight];
            }
        }

        [DefaultValue(null)]
        public Bitmap DownBitmap
        {
            set { this.bitmapDictionary[ControlState.Down] = value; }
            get
            {
                if (!this.bitmapDictionary.ContainsKey(ControlState.Down))
                    return null;
                return this.bitmapDictionary[ControlState.Down];
            }
        }

        [DefaultValue(null)]
        public Bitmap DisableBitmap
        {
            set { this.bitmapDictionary[ControlState.Disable] = value; }
            get
            {
                if (!this.bitmapDictionary.ContainsKey(ControlState.Disable))
                    return null;
                return this.bitmapDictionary[ControlState.Disable];
            }
        }

        public Bitmap CheckedBitmap
        {
            set { this.bitmapDictionary[ControlState.Checked] = value; }
            get
            {
                if (!this.bitmapDictionary.ContainsKey(ControlState.Checked))
                    return null;
                return this.bitmapDictionary[ControlState.Checked];
            }
        }
        #endregion

        public void Load(XmlReader reader, string path)
        {
            if (reader == null)
                return;

            this.bitmapDictionary.Add(ControlState.Normal, this.LoadBitmap(reader, path, ControlState.Normal));
            this.bitmapDictionary.Add(ControlState.Highlight, this.LoadBitmap(reader, path, ControlState.Highlight));
            this.bitmapDictionary.Add(ControlState.Down, this.LoadBitmap(reader, path, ControlState.Down));
            this.bitmapDictionary.Add(ControlState.Disable, this.LoadBitmap(reader, path, ControlState.Disable));
            this.bitmapDictionary.Add(ControlState.Checked, this.LoadBitmap(reader, path, ControlState.Checked));

        //    this.splitMargin = this.RectangleFromString(reader["SplitRect"]);
        }

        public void Save(XmlWriter writer)
        {
        //    writer.WriteValue()
        }

        private Bitmap LoadBitmap(XmlReader reader, string path, ControlState state)
        {
            string fullName = path = @"\" + reader[state.ToString()];
            if (File.Exists(fullName))
            {
                return (Bitmap)Bitmap.FromFile(fullName);
            }

            return null;
        }

        private Rectangle RectangleFromString(string str)
        {
            if (str == null || str.Length == 0)
                return Rectangle.Empty;

            string[] rectPara = str.Split(',');
            rectPara[0] = rectPara[0].Remove(0);
            rectPara[3] = rectPara[3].Remove(rectPara[3].Length - 1);

            Rectangle rect = new Rectangle(Convert.ToInt32(rectPara[0]),
                Convert.ToInt32(rectPara[1]),
                Convert.ToInt32(rectPara[2]),
                Convert.ToInt32(rectPara[3]));

            return rect;
        }
    }
}
